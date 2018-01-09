using System.Web.Mvc;

namespace Stackoverflow.MVC.Controllers
{
    public class SoruController : Controller
    {
        // GET: Soru
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddQuestion()
        {
            return View();
        }

       public ActionResult DeleteQuesion()
        {
            return View();
        }

        public ActionResult ListAnswer()
        {
            return View();
        }

        public ActionResult ListQuestion()
        {
            return View();
        }

        public ActionResult AddAnswer()
        {
            return View();  
        }

    }
}