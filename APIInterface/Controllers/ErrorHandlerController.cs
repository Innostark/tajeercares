using System.Web.Mvc;

namespace APIInterface.Controllers
{
    /// <summary>
    /// Handls Bad Requests
    /// </summary>
    public class ErrorHandlerController : Controller
    {
        //
        // GET: /ErrorHandler/
        public ActionResult Index()
        {
            return View();
        }
	}
}