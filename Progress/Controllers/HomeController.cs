using Newtonsoft.Json;
using Polly;
using Progress.Messages;
using Progress.Models;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

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
                    {
                        TempData["Success"] = SuccessAndErrorMessages.SuccsessMessage;
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        TempData["Error"] = SuccessAndErrorMessages.ErrorMessage;
                    }
                }
            }
            return RedirectToAction("Index", "Home");

        }
    }
}
