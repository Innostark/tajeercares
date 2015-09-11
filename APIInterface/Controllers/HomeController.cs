using System.Web.Helpers;
using APIInterface.Models;
using APIInterface.Models.RequestModels;
using APIInterface.Resources;
using APIInterface.WebApiInterfaces;
using APIInterface.WebApis;
using System.Linq;
using System.Web.Mvc;

namespace APIInterface.Controllers
{
    public class HomeController : Controller
    {
        #region Private
        private readonly IWebApiService _webApiService;
        public HomeController()
        {
            _webApiService = new WebApiService();
        }

        private string ParseExceptionMessgeFromResponse(string response)
        {
            dynamic dymaincResponse = System.Web.Helpers.Json.Decode(response);
            return dymaincResponse.Message;
        }
        #endregion
        #region Public
        /// <summary>
       /// welcome page
       /// </summary>
        public ActionResult Index(string customRoute)
        {
            return View();
        }
        /// <summary>
        /// Register user [page rendring]
        /// </summary>
        [HttpGet]
        public ActionResult RegisterUser(int? id)
        {
            if (id == null)
                id = 1;
            var model = new RegisterViewModel {CountryList = CountryList.Countries.ToList(),AccountType = (int) id};
            return View(model);
        }
        /// <summary>
        /// Register User [Page posting ]
        /// </summary>
        [HttpPost]
        public ActionResult RegisterUser(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                string registerUserResponse = _webApiService.RegisterUser(model);
                if (registerUserResponse.Contains("Success"))
                {
                    RedirectToAction("Home","RegistrationSuccess");
                    return View("RegistrationSuccess");
                }
                if (registerUserResponse.Contains("CaresGeneralException"))
                {
                    string errorMessage = ParseExceptionMessgeFromResponse(registerUserResponse);
                    errorMessage = errorMessage.Replace("Name ", "Email ");
                    errorMessage = errorMessage.Replace("taken", "registered with us");
                    ModelState.AddModelError("", errorMessage);
                }
                else
                {
                    ModelState.AddModelError("", ApiResources.registerUserError);
                }
            }
            model = new RegisterViewModel { CountryList = CountryList.Countries.ToList() };
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
        public ActionResult RegistrationSuccess()
        {
            return View();
        }

        /// <summary>
        /// Gives Overview of system
        /// </summary>
        public ActionResult Overview()
        {
            return View();
        }

        [HttpPost]
        public JsonResult CompanyURLAvailability(GeneralRequest request)
        {

            if (ModelState.IsValid)
            {
                string registerUserResponse = _webApiService.CheckCompanyUrlAvailability(request.URL);
                if (registerUserResponse.Contains("true"))
                {
                    return Json(new { status = true });

                }
               
            }
            return Json(new { status = false });
        }
        #endregion
    }
}