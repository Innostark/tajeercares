using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Web.Script.Serialization;
using APIInterface.Models;
using System.Web.Mvc;
using APIInterface.WebApiInterfaces;
using APIInterface.WebApis;

namespace APIInterface.Controllers
{
    public class RentalController : Controller
    {

        #region Private
        private readonly IRentalApiService rentalApiService;
        public RentalController()
        {
            rentalApiService = new RentalApiService();
        }

        private string ParseExceptionMessgeFromResponse(string response)
        {
            dynamic dymaincResponse = System.Web.Helpers.Json.Decode(response);
            return dymaincResponse.Message;
        }
        #endregion
        #region Public
        //
        // GET: /Rental/
        public ActionResult Index()
        {
            if (Request.Url != null)
            {
                string[] parms = Request.Url.LocalPath.Split('/');
                var response = rentalApiService.GetSitecontent(parms[1].ToUpper());
                if (response != "null")
                {
                    var jss = new JavaScriptSerializer();
                    var data = jss.Deserialize<SiteContentResponseModel>(response);
                    if (data.SiteContent == null)
                    {
                        Session["siteName"] = "404 Error";
                        return RedirectToAction("Index", "ErrorHandler", new { area = "" });
                    }
                    Session["siteName"] = parms[1].ToUpper();
                    Session["siteTitle"] = data.SiteContent.Slogan.ToUpper();

                    var model = new HomeModel
                    {
                        ReservationForm = new ReservationForm
                        {
                            PickupDateTime = DateTime.Now,
                            DropoffDateTime = DateTime.Now.AddDays(1)
                        },
                        Sitecontent = data.SiteContent,
                        OperationsWorkPlaces = data.OperationsWorkPlaces
                    };
                    Session["WPS"] = data.OperationsWorkPlaces;
                    return View(model);
                }

            }
            return View();

        }


        //
        // GET: /Rental/
        [HttpPost]
        public ActionResult SelectCar(HomeModel Model)
        {
            var operationWorkPlaces = Session["WPS"];
            var list = Session["WPS"] as List<WebApiOperationWorkplace>;
            if (list != null)
                foreach (var obj in list)
                {
                    if (obj.OperationWorkplaceId.ToString() == Model.ReservationForm.PickupLocation)
                    {
                        Session["pickupId"] = obj.LocationName;
                        Session["pickupName"] = obj.LocationName;
                    }
                    if (obj.OperationWorkplaceId.ToString() == Model.ReservationForm.DropoffLocation)
                    {
                        Session["dropoffId"] = obj.LocationName;
                        Session["dropoffName"] = obj.LocationName;
                    }
                }
            Session["pickupDate"] = Model.ReservationForm.PickupDateTime;
            Session["dropoffDate"] = Model.ReservationForm.DropoffDateTime;
            var thisii = Session["dropoffName"];
            return View(Model.ReservationForm);
        }

        //
        // GET: /Rental/
        public ActionResult SelectExtras()
        {
            return View();
        }

        public ActionResult Checkout()
        {
            return View();
        }


        public void GetLocations()
        {
            
        }
        #endregion
    }
}