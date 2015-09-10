using APIInterface.Models;
using APIInterface.Models.RequestModels;
using APIInterface.Models.ResponseModels;
using APIInterface.WebApiInterfaces;
using APIInterface.WebApis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;

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
       /// <summary>
       /// Index Of URL
       /// </summary>
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
                    Session["UserDomainKey"] = data.SiteContent.UserDomainKey;
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

        /// <summary>
        /// Car Selection
        /// </summary>
        [HttpPost]
        public ActionResult SelectCar(HomeModel model)
        {
            var list = Session["WPS"] as List<WebApiOperationWorkplace>;
            if (list != null)
                foreach (var obj in list)
                {
                    if (obj.OperationWorkplaceId.ToString() == model.ReservationForm.PickupLocation)
                    {
                        Session["pickupId"] = obj.OperationWorkplaceId;
                        Session["pickupName"] = obj.LocationName;
                        Session["pickupCityId"] = obj.CityId;
                    }
                    if (obj.OperationWorkplaceId.ToString() == model.ReservationForm.DropoffLocation)
                    {
                        Session["dropoffId"] = obj.OperationWorkplaceId;
                        Session["dropoffName"] = obj.LocationName;
                        Session["dropoffCityId"] = obj.CityId;
                    }
                }
            Session["pickupDate"] = model.ReservationForm.PickupDateTime;
            Session["dropoffDate"] = model.ReservationForm.DropoffDateTime;
           
            var requestModel = new WebApiGetAvailableHireGroupsRequest
            {
                StartDateTime = model.ReservationForm.PickupDateTime,
                EndDateTime = model.ReservationForm.DropoffDateTime,
                OutLocationId = long.Parse(model.ReservationForm.PickupLocation),
                ReturnLocationId = long.Parse(model.ReservationForm.DropoffLocation),
                DomainKey = long.Parse(Session["UserDomainKey"].ToString()),
                HireGroupId = 0,
                PickUpCityId = short.Parse(Session["pickupCityId"].ToString()),
                DropOffCityId = short.Parse(Session["dropoffCityId"].ToString())
            };
            string response = rentalApiService.GetParentHireGroups(requestModel);
            if (response != "null")
            {
                var rawData = new JavaScriptSerializer();
                var parentHireGroups = rawData.Deserialize<List<WebApiParentHireGroupsApiResponse>>(response);
                ViewBag.ProgList = parentHireGroups;
                // Getting detail of first hire group 
                requestModel.StartDateTime = model.ReservationForm.PickupDateTime;
                requestModel.EndDateTime = model.ReservationForm.DropoffDateTime;
                requestModel.OutLocationId = long.Parse(model.ReservationForm.PickupLocation);
                requestModel.ReturnLocationId = long.Parse(model.ReservationForm.DropoffLocation);
                requestModel.DomainKey = long.Parse(Session["UserDomainKey"].ToString());
                requestModel.HireGroupId = parentHireGroups.FirstOrDefault(hg => hg.HireGroupId!=null).HireGroupId;
                requestModel.PickUpCityId = short.Parse(Session["pickupCityId"].ToString());
                requestModel.DropOffCityId = short.Parse(Session["dropoffCityId"].ToString());
                var data= GetHireGroupDetail(requestModel);
                ViewBag.HGDetail = data.Count == 0 ? null : data;
            }
            return View(model.ReservationForm);
        }

         /// <summary>
         /// Extra's Selection
         /// </summary>
        public ActionResult SelectExtras()
        {
            return View();
        }

        /// <summary>
        /// Final Screen | Checkout
        /// </summary>
        public ActionResult Checkout()
        {
            return View();
        }

        /// <summary>
        /// Get Hire Group Detail On Car Seelction
        /// </summary>
        private List<WebApiHireGroupDetailResponse> GetHireGroupDetail(WebApiGetAvailableHireGroupsRequest request)
        {
           string response=  rentalApiService.GetHireGroupDetail(request);
           if (response != "null")
           {
               Session["HireGroupId"] = request.HireGroupId;
               var rawData = new JavaScriptSerializer();
               var parentHireGroups = rawData.Deserialize<List<WebApiHireGroupDetailResponse>>(response);
               return parentHireGroups;
           }
          return null;
        }
        
        /// <summary>
        /// Get Hire Group Detail On Click
        /// </summary>
        [HttpPost]
        public JsonResult GetHireGroupDetailOnClick(string hireGroupid)
        {
            var requestModel = new WebApiGetAvailableHireGroupsRequest
            {
                StartDateTime = (DateTime) Session["pickupDate"],
                EndDateTime = (DateTime) Session["dropoffDate"],
                OutLocationId = long.Parse(Session["pickupId"].ToString()),
                ReturnLocationId = long.Parse(Session["dropoffId"].ToString()),
                DomainKey = long.Parse(Session["UserDomainKey"].ToString()),
                HireGroupId = long.Parse(hireGroupid),
                PickUpCityId = short.Parse(Session["pickupCityId"].ToString()),
                DropOffCityId = short.Parse(Session["dropoffCityId"].ToString())
            };
            var data = GetHireGroupDetail(requestModel);
            var response = data.Count == 0 ? null : data;
            return Json(new { detail = response });
        }

        /// <summary>
        /// Calculate Charge For hire Group on Car Selection
        /// </summary>
        [HttpPost]
        public JsonResult CalculateCharge(string hireGroupDetailId)
        {
            return null;
        }

        #endregion
    }
}