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
        private static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
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
                    SiteContentResponseModel data;
                    var jss = new JavaScriptSerializer();
                    try
                    {
                         data = jss.Deserialize<SiteContentResponseModel>(response);
                         data.SiteContent.LogoSourceLocal = GetBytes(data.SiteContent.CompanyLogoBytes);
                         data.SiteContent.Banner1SourceLocal = GetBytes(data.SiteContent.Banner1Bytes);
                         data.SiteContent.Banner2SourceLocal = GetBytes(data.SiteContent.Banner2Bytes);
                         data.SiteContent.Banner3SourceLocal = GetBytes(data.SiteContent.Banner3Bytes);
                    }
                    catch (Exception exc)
                    {
                        throw new Exception("Error while getting data from server!");
                    }
                    if (data.SiteContent == null)
                    {
                        Session["siteName"] = "404 Error";
                        return RedirectToAction("Index", "ErrorHandler", new { area = "" });
                    }
                    Session["siteName"] = data.SiteContent.CompanyDisplayName;
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
                        Session["pickupOperationId"] = obj.OperationId;
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
        public ActionResult SelectExtras(string idString)
        {
            string id = idString.Substring(7, idString.Length - 7);
            var rawResponse =   rentalApiService.GetExtras_Insurances(long.Parse(Session["UserDomainKey"].ToString()));
            ExtrasResponseModel data= null;

            if (rawResponse != "null")
             {
                 var rawData = new JavaScriptSerializer();
                 try
                 {
                      data = rawData.Deserialize<ExtrasResponseModel>(rawResponse);
                      ViewBag.Data = data;
                 }
                 catch (Exception exc)
                 {
                     throw new Exception("Error while getting data from server!");
                 }
             }
            return View(data);
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
            string id = hireGroupDetailId.Substring(7, hireGroupDetailId.Length - 7);

            var requestModel = new GetCandidateHireGroupChargeRequest
            {
                OperationId = long.Parse(Session["pickupOperationId"].ToString()),
                StartDtTime = (DateTime)Session["pickupDate"],
                EndDtTime = (DateTime)Session["dropoffDate"],
                HireGroupDetailId = long.Parse(id),
                RaCreatedDate = DateTime.Now,
                UserDomainKey = long.Parse(Session["UserDomainKey"].ToString())
            };
            string response = rentalApiService.GetCharge(requestModel);
            if (response != "null")
            {
                var rawData = new JavaScriptSerializer();
                var charge = rawData.Deserialize<RaCandidateHireGroupCharge>(response);
                return Json(new { hGcharge = charge });
            }
            return null;
        }

        #endregion
    }
}