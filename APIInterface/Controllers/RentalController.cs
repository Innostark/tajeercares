﻿using APIInterface.Models;
using APIInterface.Models.RequestModels;
using APIInterface.Models.ResponseModels;
using APIInterface.WebApiInterfaces;
using APIInterface.WebApis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace APIInterface.Controllers
{
    /// <summary>
    /// Rental Controller To Handle Client's Booking 
    /// </summary>
    public class RentalController : Controller
    {

        #region Private
        private readonly IRentalApiService rentalApiService;
        #region Private Funcs

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


        /// <summary>
        /// Sets data from reservation form to session
        /// </summary>
        private void ExtractDataFromReservationForm(HomeModel model)
        {
            // Restoring Workplaces stored in Index 
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
        }


        /// <summary>
        /// Sets Request
        /// </summary>
        private WebApiGetAvailableHireGroupsRequest MakeGetAvailableHireGroupsRequest(HomeModel model)
        {
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
            return requestModel;
        }


        /// <summary>
        /// Finds Hire Group detail by Detail Id
        /// </summary>
        private void ExtractHireGroupById(string hireGroupDetailId)
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
                                Session["selectedHireGroupDetail"] = cHireGroup;
                            }
                        }
                    }
                }
            }
        }


        /// <summary>
        /// Sets Request For HG Charge
        /// </summary>
        private GetCandidateHireGroupChargeRequest SetCandidateHireGroupChargeRequest(string hireGroupDetailId)
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
            return requestModel;
        }


        /// <summary>
        /// Sets Hire Group rate for requested HG
        /// </summary>
        private void SetHireGroupCharge(string hireGroupDetailId, RaCandidateHireGroupCharge response)
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
                                cHireGroup.StandardRt = response.TotalStandardCharge;
                                cHireGroup.TariffType = response.TariffTypeCode;
                            }
                        }
                    }
                }
            }
            Session["HGDetail"] = parentHireGroups;
        }


        /// <summary>
        /// Sets Request For rate Service Item
        /// </summary>
        private GetServiceItemRateRequest GetServiceItemRateRequest(long serviceItemId, int quantity)
        {
            var requestModel = new GetServiceItemRateRequest
            {
                OperationId = long.Parse(Session["pickupOperationId"].ToString()),
                StartDateTime = (DateTime)Session["pickupDate"],
                EndDateTime = (DateTime)Session["dropoffDate"],
                ServiceItemId = serviceItemId,
                RaCreationDateTime = DateTime.Now,
                Quantity = quantity,
                UserDomainKey = long.Parse(Session["UserDomainKey"].ToString())
            };
            return requestModel;
        }


        /// <summary>
        /// Sets rate for Service Item
        /// </summary>
        private void SetRateForServiceItem(long serviceItemId, RaCandidateExtrasCharge response)
        {
            var extras = Session["Extras"] as ExtrasResponseModel;
            foreach (var item in extras.ServiceItems)
            {
                if (item.ServiceItemId == serviceItemId)
                {
                    item.ServiceRate = response.ServiceRate;
                    item.ServiceCharge = response.ServiceCharge;
                }
            }
            Session["Extras"] = extras;
        }


        /// <summary>
        /// Sets rate for Insurance Types
        /// </summary>
        private void SetRateForInsuranceType(long insuranceTyped, RaCandidateItemCharge response)
        {
            var extras = Session["Extras"] as ExtrasResponseModel;
            foreach (var item in extras.InsuranceTypes)
            {
                if (item.InsuranceTypeId == insuranceTyped)
                {
                    item.InsuranceRate = response.Rate;
                    item.InsuranceCharge = response.Charge;
                }
            }
            Session["Extras"] = extras;
        }

        /// <summary>
        /// Sets Request For rate Insurance Type
        /// </summary>
        private GetCandidateInsuranceChargeRequest GetCandidateInsuranceChargeRequest(short insuranceTypeId)
        {
            var detailHireGroup = Session["selectedHireGroupDetail"] as WebApiHireGroupDetailResponse;
            var requestModel = new GetCandidateInsuranceChargeRequest
            {
                OperationId = long.Parse(Session["pickupOperationId"].ToString()),
                StartDtTime = (DateTime)Session["pickupDate"],
                EndDtTime = (DateTime)Session["dropoffDate"],
                HireGroupDetailId = detailHireGroup.HireGroupDetailId,
                RaCreatedDate = DateTime.Now,
                Domainkey = long.Parse(Session["UserDomainKey"].ToString()),
                InsuranceTypeId = insuranceTypeId
            };
            return requestModel;
        }
        
        /// <summary>
        /// Extras Total
        /// </summary>
        private  double GetExtrasTotal(int[] extrasIds, int[] insurancesIds, ExtrasResponseModel extras,
           out double insuranceTotal, out double serviceItemsTotal, out double? total, out List<string> items )
        {
            serviceItemsTotal = 0;
            items = new List<string>();
            foreach (var extra in extras.ServiceItems)
            {
                if (extrasIds.Any(id => id == extra.ServiceItemId))
                {
                    items.Add("<p>"+extra.ServiceItemName + " <span class='price'>" + extra.ServiceCharge + "</span></p>");
                    serviceItemsTotal = serviceItemsTotal + extra.ServiceCharge;
                }
            }
            insuranceTotal = 0;
            foreach (var ins in extras.InsuranceTypes)
            {
                if (insurancesIds.Any(id => id == ins.InsuranceTypeId))
                {
                    items.Add("<p>"+ins.InsuranceTypeName + " <span class='price'>" + ins.InsuranceCharge + "</span></p>");
                    insuranceTotal = insuranceTotal + ins.InsuranceCharge;
                }
            }
            var detailHireGroup = Session["selectedHireGroupDetail"] as WebApiHireGroupDetailResponse;
            total = detailHireGroup.StandardRt;
            return serviceItemsTotal;
        }


        /// <summary>
        /// Setting up Request n Session
        /// </summary>
        private HomeModel SettingRequestNSession(SiteContentResponseModel response)
        {
            Session["siteName"] = response.SiteContent.CompanyDisplayName;
            Session["siteTitle"] = response.SiteContent.Slogan.ToUpper();
            Session["UserDomainKey"] = response.SiteContent.UserDomainKey;
            var model = new HomeModel
            {
                ReservationForm = new ReservationForm
                {
                    PickupDateTime = DateTime.Now,
                    DropoffDateTime = DateTime.Now.AddDays(1)
                },
                Sitecontent = response.SiteContent,
                OperationsWorkPlaces = response.OperationsWorkPlaces
            };

            // For further Use on next pages 
            Session["WPS"] = response.OperationsWorkPlaces;
            return model;
        }

        #endregion
        public RentalController()
        {
            rentalApiService = new RentalApiService();
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
                // Getting URL & calling server
                string[] parms = Request.Url.LocalPath.Split('/');
                var response = rentalApiService.GetSitecontent(parms[1].ToUpper());
                if (response != null)
                {
                    // Handling Bad Requests
                    if (response.SiteContent == null)
                    {
                        Session["siteName"] = "404 Error";
                        return RedirectToAction("Index", "ErrorHandler", new { area = "" });
                    }

                    // Setting up session 
                    var model = SettingRequestNSession(response);
                    return View(model);
                }

            }
            return View();

        }



        /// <summary>
        /// Car Selection
        /// </summary>
        public ActionResult SelectCar(HomeModel model)
        {
            ExtractDataFromReservationForm(model);
            var requestModel = MakeGetAvailableHireGroupsRequest(model);
            var parentHireGroups = rentalApiService.GetParentHireGroups(requestModel);
            if (parentHireGroups != null)
            {
                Session["HGDetail"] = ViewBag.HGDetail = parentHireGroups.Count == 0 ? null : parentHireGroups;
            }
            return View();
        }


         /// <summary>
         /// Extra's Selection
         /// </summary>
        public ActionResult SelectExtras(string hireGroupDetailId)
         {
            ExtractHireGroupById(hireGroupDetailId);
            var response =   rentalApiService.GetExtras_Insurances(long.Parse(Session["UserDomainKey"].ToString()));
            ViewBag.Data =  Session["Extras"]=response;
            return View();
        }


        /// <summary>
        /// Final Screen | Checkout
        /// </summary>
        [HttpGet]
        public ActionResult Checkout(int[] extrasIds, int[] insurancesIds)
        {
            var extras = Session["Extras"] as ExtrasResponseModel;
            double insuranceTotal=0;
            double serviceItemsTotal=40;
            double? total=180;
            List<string> items = new List<string>();
            items.Add("<p>GPS-GPS Navigation <span class='price'>" + 40 + "</span></p>");
       //     GetExtrasTotal(extrasIds, insurancesIds, extras, out insuranceTotal,out serviceItemsTotal, out total, out items);
            var model = new UserInfoModel
            {
                CountryList = CountryList.Countries.ToList(),
                ItemsHtml = items,
                InsurancesTotal = insuranceTotal,
                ServiceItemsTotal = serviceItemsTotal,
                SubTotal = total, // hire group wala
                GrandTotal = total + serviceItemsTotal + insuranceTotal
            };

            return View(model);
        }

       

        /// <summary>
        /// Final Screen | Checkout
        /// </summary>
        [HttpPost]
        public ActionResult Checkout(UserInfoModel model)
        {
            if (ModelState.IsValid)
            {
                //
            }
            model = new UserInfoModel { CountryList = CountryList.Countries.ToList() };
            return View(model);
        }
      
       
        /// <summary>
        /// Calculate Charge For hire Group on Car Selection
        /// </summary>
        public JsonResult CalculateCharge(string hireGroupDetailId)
        {
            var requestModel = SetCandidateHireGroupChargeRequest(hireGroupDetailId);
            RaCandidateHireGroupCharge response = rentalApiService.GetHireGroupCharge(requestModel);
            if (response != null)
            {
                response.TariffTypeCode = GetHireGroupFullName(response.TariffTypeCode);
                // setting up charge for hiregroup in the list
                SetHireGroupCharge(hireGroupDetailId, response);
                return Json(new { hGcharge = response });
            }
            return null;
        }


        /// <summary>
        /// Get Service rate For Service Item
        /// </summary>
        public JsonResult GetChargeForServiceItem(long serviceItemId, int quantity)
        {
            var requestModel = GetServiceItemRateRequest(serviceItemId, quantity);
            var response=  rentalApiService.GetServiceItemRate(requestModel);
           if (response != null)
           {
               SetRateForServiceItem(serviceItemId, response);
               return Json(new { itemCharge = response });
           }
            return null;
        }


        /// <summary>
        /// Get Service rate For Insurance Type
        /// </summary>
        public JsonResult GetChargeForInsuranceType(short insuranceTypeId)
        {
            var requestModel = GetCandidateInsuranceChargeRequest(insuranceTypeId);
            var response= rentalApiService.GetInsuranceTypeRate(requestModel);
            if (response != null)
            {
                SetRateForInsuranceType(insuranceTypeId, response);
                return Json(new { insuranceCharge = response });
            }
            return null;     
        }

      
        #endregion
    }
}