using Progress.Extensions;
using Progress.Messages;
using Progress.Models;
using Service;
using Service.Contracts;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Progress.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }
        
        public ActionResult Index()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> Index(SendEmail sendEmail, ModelVM modelVM)
        {
            if (ModelState.IsValid)
            {
                var client = modelVM.GetClient();
                var postAsync = await sendEmail.SendEmailAsync(client);
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
            return RedirectToAction("Index", "Home");

        }
    }
}
