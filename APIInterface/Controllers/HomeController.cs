﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Net;
using System.Net.Mail;
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
            var id = 1;
            var model = new RegisterViewModel { CountryList = CountryList.Countries.ToList(), AccountType = (int)id };
            
            return View(model);
        }

        /// <summary>
        /// Register User [Page posting ]
        /// </summary>
        [HttpPost]
        public ActionResult Index(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                string registerUserResponse = _webApiService.RegisterUser(model);
                if (registerUserResponse.Contains("Success"))
                {
                    RedirectToAction("Home", "RegistrationSuccess");
                    return View("RegistrationSuccess");
                }
                if (registerUserResponse.Contains("CaresGeneralException"))
                {
                    string errorMessage = ParseExceptionMessgeFromResponse(registerUserResponse);
                    errorMessage = errorMessage.Replace("Name ", "Email ");
                    errorMessage = errorMessage.Replace("taken", "registered with us");
                    ModelState.AddModelError("", errorMessage);
                    ViewBag.signupError = true;
                }
                else if (registerUserResponse.Contains("Runtime Error"))
                {
                    RedirectToAction("Home", "RegistrationSuccess");
                    return View("RegistrationSuccess");
                    // ModelState.AddModelError("", "This is response"+registerUserResponse); //ApiResources.registerUserError
                }
            }
            ViewBag.signupError = true;
            model = new RegisterViewModel { CountryList = CountryList.Countries.ToList() };
            return View(model);
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
                else if (registerUserResponse.Contains("Runtime Error"))
                {
                    RedirectToAction("Home", "RegistrationSuccess");
                    return View("RegistrationSuccess");
                   // ModelState.AddModelError("", "This is response"+registerUserResponse); //ApiResources.registerUserError
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

        /// <summary>
        /// Send Email 
        /// </summary>
        [HttpPost]
        public JsonResult SendEmail(EmailModel email)
        {
            // email weill be sending to Tajeercare.com admin 
            string adminAddress = ConfigurationManager.AppSettings["ToAdmin"];
            var emailContent = email;
            if (emailContent != null)
            {
                try
                {
                    string body = emailContent.EmailBody + " \n From :" + emailContent.SenderName + " " +
                                  emailContent.SenderEmail + " \n Phone:" + emailContent.Phone;
                    SendEmailTo(adminAddress, emailContent.EmailSubject, body, emailContent.SenderName);
                    return Json(new { status = "ok" });
                }
                catch (Exception excp)
                {
                    return Json(new { excp.StackTrace });
                }

            }
            return Json(new { status = "error" });
        }
        /// <summary>
        /// Send Email 
        /// </summary>
        public static void SendEmailTo(string email, string subject, string body, string fromDisplayName)
        {

            string fromAddress = ConfigurationManager.AppSettings["FromAddress"];
            string fromPwd = ConfigurationManager.AppSettings["FromPassword"];

            //Getting the file from config, to send
            var oEmail = new MailMessage
            {
                From = new MailAddress(fromAddress, fromDisplayName),
                Subject = subject,
                IsBodyHtml = true,
                Body = body,
                Priority = MailPriority.High
            };
            oEmail.To.Add(email);
            string smtpServer = ConfigurationManager.AppSettings["SMTPServer"];
            string smtpPort = ConfigurationManager.AppSettings["SMTPPort"];
            string enableSsl =ConfigurationManager.AppSettings["EnableSSL"];
            var client = new SmtpClient(smtpServer, Convert.ToInt32(smtpPort))
            {
                EnableSsl = enableSsl == "1",
                Credentials = new NetworkCredential(fromAddress, fromPwd)
            };
            client.Send(oEmail);
        }

        public ActionResult ChangeCulture(string lang)
        {
            Session["Culture"] = new CultureInfo(lang);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult KeepAlive()
        {
            return Json(new {});
        }

        #endregion
    }
}