using System.Web.Mvc;
using APIInterface.Models;
using APIInterface.WebApiInterfaces;
using APIInterface.WebApis;

namespace APIInterface.Controllers
{
    public class HomeController : Controller
    {
        #region Private
        private readonly IWebApiService webApiService;
        public HomeController()
        {
            this.webApiService = new WebApiService();
        }

        #endregion

       /// <summary>
       /// welcome page
       /// </summary>
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Register user [page rendring]
        /// </summary>
        [HttpGet]
        public ActionResult RegisterUserByView(int? id)
        {
            if (id == null)
                id = 1;
            ViewBag.registrationTypeId = id + "";
            return View();
        }
        /// <summary>
        /// Register User [Page posting ]
        /// </summary>
        [HttpPost]
        public ActionResult RegisterUserByView(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool registerUser = webApiService.RegisterUser(model);
            }
            return View(model);
        }

        /// <summary>
        /// Features page
        /// </summary>
        public ActionResult Features()
        {
            return View();
        }
    }
}