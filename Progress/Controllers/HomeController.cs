using Magnum.FileSystem;
using Newtonsoft.Json;
using Polly;
using Progress.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Progress.Controllers
{
    public class HomeController : Controller
    {
        private const string url = "https://us-central1-randomfails.cloudfunctions.net/submitEmail";
        public ActionResult Index()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> Index(Model model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var json = model.Email;
                    var converted = JsonConvert.SerializeObject(model, Formatting.Indented);
                    var policy = Policy
                                    .Handle<Exception>()
                                    .OrResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
                                    .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                             + TimeSpan.FromMilliseconds(1000));


                    using (var httpClient = new HttpClient())
                    {
                        var postAsync = await policy.ExecuteAsync(async () => await httpClient.PostAsync(url,
                   new StringContent(converted, Encoding.UTF8, "application/json")));
                        if (postAsync.IsSuccessStatusCode)
                            TempData["Success"]= "Your email was successfully submitted!";

                    }
                    return RedirectToAction("Index", "Home");

                }
            }
            catch (Exception)
            {
                TempData["Error"]= "The email was not sent. Please try again!";
            }
            return RedirectToAction("Index", "Home");

        }
    }
}
