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

        private string ParseExceptionMessgeFromResponse(string response)
        {
            dynamic dymaincResponse = System.Web.Helpers.Json.Decode(response);
            return dymaincResponse.Message;
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
                string registerUserResponse = webApiService.RegisterUser(model);
                if (registerUserResponse.Contains("Success"))
                {
                    RedirectToAction("RegistrationSuccess");
                }
                else if (registerUserResponse.Contains("CaresGeneralException"))
                {
                    string errorMessage = ParseExceptionMessgeFromResponse(registerUserResponse);
                    errorMessage = errorMessage.Replace("Name ", "Email ");
                    errorMessage = errorMessage.Replace("taken", "registered with us");
                    ModelState.AddModelError("", errorMessage);
                }
                else
                {
                    ModelState.AddModelError("",
                        "Something bad happend. Please try again later. We have recorded your action, and you will be contacted on the email you provided. We apologize for inconvenience!");
                }
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
        /// <summary>
        /// Registration Confirm
        /// </summary>
        /// <returns></returns>
        public ActionResult RegistrationSuccess()
        {
            return View();
        }
    }
}