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
            var list = Session["WPS"]  as List<WebApiOperationWorkplace>;
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
                Session["HGDetail"] = ViewBag.HGDetail = parentHireGroups.Count == 0 ? null : parentHireGroups;
            }
            return View();
        }

         /// <summary>
         /// Extra's Selection
         /// </summary>
        public ActionResult SelectExtras(string hireGroupDetailId)
         {
             var parentHireGroups = Session["HGDetail"] as List<WebApiParentHireGroupsApiResponse>;
             if (parentHireGroups != null)
             {
                 foreach (var pHireGroup in parentHireGroups)
                 {
                     var cHireGroups = pHireGroup.SubHireGroups;
                     if (cHireGroups != null)
                     {
                         foreach (var cHireGroup in cHireGroups)
                         {
                             if (cHireGroup.HireGroupDetailId == long.Parse(hireGroupDetailId))
                             {
                                 Session["selectedHG"] = cHireGroup;
                             }
                         }
                     }
                 }
             }
           
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
        //private List<WebApiHireGroupDetailResponse> GetHireGroupDetail(WebApiGetAvailableHireGroupsRequest request)
        //{
        //   string response=  rentalApiService.GetHireGroupDetail(request);
        //   if (response != "null")
        //   {
        //       Session["HireGroupId"] = request.HireGroupId;
        //       var rawData = new JavaScriptSerializer();
        //       var parentHireGroups = rawData.Deserialize<List<WebApiHireGroupDetailResponse>>(response);
        //       return parentHireGroups;
        //   }
        //  return null;
        //}
        
       

        /// <summary>
        /// Calculate Charge For hire Group on Car Selection
        /// </summary>
        [HttpPost]
        public JsonResult CalculateCharge(string hireGroupDetailId)
        {
           
            var requestModel = new GetCandidateHireGroupChargeRequest
            {
                OperationId = long.Parse(Session["pickupOperationId"].ToString()),
                StartDtTime = (DateTime)Session["pickupDate"],
                EndDtTime = (DateTime)Session["dropoffDate"],
                HireGroupDetailId = long.Parse(hireGroupDetailId),
                RaCreatedDate = DateTime.Now,
                UserDomainKey = long.Parse(Session["UserDomainKey"].ToString())
            };
            string response = rentalApiService.GetCharge(requestModel);
            if (response != "null")
            {
                var rawData = new JavaScriptSerializer();
                var charge = rawData.Deserialize<RaCandidateHireGroupCharge>(response);
                charge.TariffTypeCode = GetHireGroupFullName(charge.TariffTypeCode);
                // setting up charge for hiregroup in the list
                var parentHireGroups = Session["HGDetail"] as List<WebApiParentHireGroupsApiResponse>;
                if (parentHireGroups != null)
                {
                    foreach (var pHireGroup in parentHireGroups)
                    {
                        var cHireGroups = pHireGroup.SubHireGroups;
                        if (cHireGroups != null)
                        {
                            foreach (var cHireGroup in cHireGroups)
                            {
                                if (cHireGroup.HireGroupDetailId == long.Parse(hireGroupDetailId))
                                {
                                    cHireGroup.StandardRt = charge.TotalStandardCharge;
                                    cHireGroup.TariffType = charge.TariffTypeCode;
                                }
                            }
                        }
                    }   
                }
                Session["HGDetail"] = parentHireGroups;
                return Json(new { hGcharge = charge });
            }
            return null;
        }

        /// <summary>
        /// Sets up Hire Group Name
        /// </summary>
        private string GetHireGroupFullName(string hireGroupCode)
        {
            switch (hireGroupCode)
            {
                case "H":
                    return "H-Hourly";
                case "D":
                    return "D-Daily";
                case "W":
                    return "W-Weekly";
                case "FN":
                    return "FN-Fortnightly";
                case "M":
                    return "M-Monthly";
                default:
                    return "(No Hire Group)";
            }
        }

        [HttpPost]
        public JsonResult GetChargeForServiceItem(long serviceItemId, int quantity)
        {
            var requestModel = new GetServiceItemRateRequest
            {
                OperationId = long.Parse(Session["pickupOperationId"].ToString()),
                StartDateTime = (DateTime) Session["pickupDate"],
                EndDateTime = (DateTime) Session["dropoffDate"],
                ServiceItemId = serviceItemId,
                RaCreationDateTime = DateTime.Now,
                Quantity = quantity,
                UserDomainKey = long.Parse(Session["UserDomainKey"].ToString())
            };
           var rawResponse=  rentalApiService.GetServiceItemRate(requestModel);
           if (rawResponse != "null")
            {
                var rawData = new JavaScriptSerializer();
                var charge = rawData.Deserialize<RaCandidateExtrasCharge>(rawResponse);
                return Json(new { itemCharge = charge });
            }
            return null;
        }
        #endregion
    }
}