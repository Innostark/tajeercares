using System.Activities.Statements;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using APIInterface.Models;
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
        /// Final Booking MOdel
        /// </summary>
        private BookingModel SetOnlineBookingModel(UserInfoModel userInfo)
        {
            var extrasIds = Session["extrasList"] as string[];
            var insuranceTypesIds = Session["insuranceTypeList"] as string[];

            var extrasObjectList = extrasIds.Where(id => id != "-99").Select(double.Parse).ToList();
            var insuranceTypesObjectList = insuranceTypesIds.Where(id => id != "-99").Select(double.Parse).ToList();

            var model = new BookingModel
            {
                UserInfo = new UserInfoModel
                {
                    BillingAddress = userInfo.BillingAddress,
                    DOB = userInfo.DOB,
                    Email = userInfo.Email,
                    FName = userInfo.FName,
                    LName = userInfo.LName,
                    PhoneNumber = userInfo.PhoneNumber
                }
            };
            model.PickUpLocationId = double.Parse(Session["pickupId"].ToString());
            model.DropOffLocationId = double.Parse(Session["dropoffId"].ToString());
            model.PickupOperationId = double.Parse(Session["pickupOperationId"].ToString());

            model.PickupDateTime = Convert.ToDateTime((Session["pickupDate"].ToString()));
            model.DropoffDateTime = Convert.ToDateTime((Session["dropoffDate"].ToString()));

            model.HireGroupDetailId = double.Parse(Session["HireGroupDetailId"].ToString());
            model.DropOffCharges = double.Parse(Session["DropOffCharges"].ToString());

            model.StandardRate = double.Parse(Session["standardRate"].ToString());

            model.SubTotal = double.Parse(Session["SubTotal"].ToString());
            model.FullTotal = double.Parse(Session["GrandTotal"].ToString());

            model.UserDomainKey = long.Parse(Session["UserDomainKey"].ToString());
            model.TariffType = Session["TariffType"].ToString();

            model.ServiceItems = extrasObjectList.ToList();
            model.InsuranceTypes = insuranceTypesObjectList;
            
            return model;
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

        /// <summary>
        /// Check out Model for Data
        /// </summary>
        private UserInfoModel MakCheckoutModel(string ServiceItemsIds, string InsurancesIds)
        {
            var extras = Session["Extras"] as ExtrasResponseModel;
            var extrasIds = GetIdsFromString(ServiceItemsIds);
            var insurancesIds = GetIdsFromString(InsurancesIds);
            double insuranceTotal = 0;
            double serviceItemsTotal = 0;
            double? total = 0;
            var items = new List<string>();
            GetExtrasTotal(extrasIds, insurancesIds, extras, out insuranceTotal, out serviceItemsTotal, out total, out items);
            var model = new UserInfoModel
            {
                ItemsHtml = items,
                InsurancesTotal = insuranceTotal,
                ServiceItemsTotal = serviceItemsTotal,
                SubTotal = total, // hire group wala
                FormatedSubTotal = Convert.ToDecimal(total).ToString("#,##"),

                GrandTotal = total + serviceItemsTotal + insuranceTotal,
                FormatedGrandTotal = Convert.ToDecimal(total + serviceItemsTotal + insuranceTotal).ToString("#,##"),
                DOB = DateTime.Now
            };
            Session["GrandTotal"] = Convert.ToDecimal(model.GrandTotal).ToString("#,##");
            Session["SubTotal"] = Convert.ToDecimal(model.SubTotal).ToString("#,##");
            return model;
        }

        /// <summary>
        /// Sets data from reservation form to session
        /// </summary>
        private HomeModel ExtractDataFromReservationForm(HomeModel model)
        {
            // Restoring Workplaces stored in Index 
            var operationsWorkPlacesst = Session["WPS"] as List<WebApiOperationWorkplace>;

            if (operationsWorkPlacesst != null)
                foreach (var obj in operationsWorkPlacesst)
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
           
            string[] parms=  model.ReservationForm.PickupHours.Split(':');
            var span = new TimeSpan(Int32.Parse(parms[0]), Int32.Parse(parms[1]), 0);
             model.ReservationForm.PickupDateTime = model.ReservationForm.PickupDateTime+span;
             Session["pickupDate"] = model.ReservationForm.PickupDateTime.ToString("MM/dd/yyyy HH:mm"); 

            parms = model.ReservationForm.DropoffHours.Split(':');
            span = new TimeSpan(Int32.Parse(parms[0]), Int32.Parse(parms[1]), 0);
            model.ReservationForm.DropoffDateTime = model.ReservationForm.DropoffDateTime+span;
            Session["dropoffDate"] = model.ReservationForm.DropoffDateTime.ToString("MM/dd/yyyy HH:mm");

            // Re-creating model in case user want to update dates or time 
            var selectCarModel = new HomeModel
            {
                ReservationForm = new ReservationForm
                {
                    PickupDateTime = Convert.ToDateTime(Session["pickupDate"].ToString()),
                    DropoffDateTime = Convert.ToDateTime(Session["dropoffDate"].ToString()),
                    HoursList = ReservationHours.Hours.ToList()
                },
                OperationsWorkPlaces = operationsWorkPlacesst
            };

            DateTime startDateTime = Convert.ToDateTime(Session["pickupDate"].ToString());
            DateTime endDateTime = Convert.ToDateTime(Session["dropoffDate"].ToString());

           TimeSpan periodSpan = (endDateTime - startDateTime);

           string period = String.Format("{0} days, {1} hours",
                 periodSpan.Days, periodSpan.Hours);
            Session["period"] = period;

            return selectCarModel;
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
            Session["HireGroupDetailId"] = hireGroupDetailId;
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
                                string dropOffWithComma = Convert.ToDecimal(pHireGroup.DropoffCharge).ToString("#,##");

                                // standard rate formated with comma for thousands
                                cHireGroup.FormatedStandardRate = Convert.ToDecimal(cHireGroup.StandardRt).ToString("#,##");
                               
                                Session["selectedHireGroupDetail"] = cHireGroup;
                                Session["DropOffCharges"] = dropOffWithComma!=""? (object) dropOffWithComma:0;
                                Session["vehicleImgUrl"] = cHireGroup.ImageUrl;                                
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
                StartDtTime = Session["pickupDate"].ToString(),
                EndDtTime = Session["dropoffDate"].ToString(),
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
                                cHireGroup.StandardRt = Math.Round(response.TotalStandardCharge);
                                cHireGroup.TariffType = response.TariffTypeCode;
                                Session["standardRate"] = cHireGroup.StandardRt;
                                Session["TariffType"] = cHireGroup.TariffType;
                                DateTime startDateTime = Convert.ToDateTime(Session["pickupDate"].ToString());
                                DateTime endDateTime = Convert.ToDateTime(Session["dropoffDate"].ToString());
                                // Per day cost
                                TimeSpan periodSpan = (endDateTime - startDateTime);
                                string period = String.Format("{0}",
                                      periodSpan.Days);
                                int day= int.Parse(period)!=0?int.Parse(period):  1;
                                response.PerDayCost =Math.Round( (response.TotalStandardCharge / day),2).ToString();
                                response.TotalStandardCharge = Math.Round(response.TotalStandardCharge, 2);
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
                StartDateTime = Session["pickupDate"].ToString(),
                EndDateTime = Session["dropoffDate"].ToString(),
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
                    item.ServiceRate = Math.Round(response.ServiceRate,2);
                    item.ServiceCharge = Math.Round(response.ServiceCharge,2);

                    response.ServiceRate = Math.Round(response.ServiceRate, 2);
                    response.ServiceCharge = Math.Round(response.ServiceCharge, 2);
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
                    item.InsuranceRate = Math.Round(response.Rate,2);
                    item.InsuranceCharge = Math.Round(response.Charge,2);

                    response.Rate = Math.Round(response.Rate,2);
                    response.Charge = Math.Round(response.Charge, 2);
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
                StartDtTime = Session["pickupDate"].ToString(),
                EndDtTime = Session["dropoffDate"].ToString(),
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
        private  void GetExtrasTotal(string[] extrasIds, string[] insurancesIds, ExtrasResponseModel extras, out double insuranceTotal, out double serviceItemsTotal, out double? total, out List<string> items)
        {
            Session["extrasList"] = extrasIds;
            Session["insuranceTypeList"] = insurancesIds;
            serviceItemsTotal = 0;
            items = new List<string>();
            foreach (var extra in extras.ServiceItems)
            {
                if (extrasIds.Any(id => Int64.Parse(id) == extra.ServiceItemId))
                {
                    items.Add("<p>" + extra.ServiceItemName + " <span class='price'>" + "SAR " + Convert.ToDecimal(extra.ServiceCharge).ToString("#,##") + "</span></p>");
                    serviceItemsTotal = Math.Round(serviceItemsTotal + extra.ServiceCharge);
                }
            }
            insuranceTotal = 0;
            foreach (var ins in extras.InsuranceTypes)
            {
                if (insurancesIds.Any(id => Int64.Parse(id) == ins.InsuranceTypeId))
                {
                    items.Add("<p>" + ins.InsuranceTypeName + " <span class='price'>" + "SAR " + Convert.ToDecimal(ins.InsuranceCharge).ToString("#,##") + "</span></p>");
                    insuranceTotal = Math.Round(insuranceTotal + ins.InsuranceCharge);
                }
            }
            var detailHireGroup = Session["selectedHireGroupDetail"] as WebApiHireGroupDetailResponse;
            total = detailHireGroup.StandardRt;

            return;
        }


        /// <summary>
        /// Setting up Request n Session
        /// </summary>
        private HomeModel SettingRequestNSession(SiteContentResponseModel response)
        {
            if (response == null)
            {
                throw  new Exception("Response contains no data!");
            }
            Session["siteName"] = response.SiteContent.CompanyDisplayName;
            Session["siteTitle"] = response.SiteContent.Slogan;
            Session["UserDomainKey"] = response.SiteContent.UserDomainKey;
            Session["CompanyShortName"] = response.SiteContent.CompanyShortName;
            Session["EmailForContact"] = response.SiteContent.Email;
            Session["CompanyLogo"] = response.SiteContent.CompanyLogo;

            var model = new HomeModel
            {
                ReservationForm = new ReservationForm
                {
                    PickupDateTime = DateTime.Now.AddDays(1),
                    DropoffDateTime = DateTime.Now.AddDays(2),
                    HoursList = ReservationHours.Hours.ToList()
                },
                Sitecontent = response.SiteContent,
                OperationsWorkPlaces = response.OperationsWorkPlaces // here
            };
            foreach (var workPlace in response.OperationsWorkPlaces)
            {
                string temp = workPlace.Latitude + "-" + workPlace.Longitude + "-"+workPlace.LocationName;
                workPlace.RawString = temp;
                temp = null;
                workPlace.CoordinatesContents = MakeLocationOnMap(workPlace);
            }
           
            // For further Use on next pages 
            Session["WPS"] = response.OperationsWorkPlaces;
            return model;
        }

        /// <summary>
        /// Makes Co-ordinate Div on Map
        /// </summary>
        private string MakeLocationOnMap(WebApiOperationWorkplace source)
        {
             var mapIWcontent = "" +
                "" +
                "<div class='map-info-window'>" +
                "<div class='thumbnail no-border no-padding thumbnail-car-card'>" +
                "<div class='media'>" +
                "<a class='media-link' href='#'>" +
                
                "<span class='icon-view'><strong><i class='fa fa-eye'></i></strong></span>" +
                "</a>" +
                "</div>" +
                "<div class='caption text-center'>" +
                "<h4 class='caption-title'><a href='#'>"+source.LocationName+"</a></h4>" 
                +
                "<table class='table'>" +
                "<tr>" +
                "<td><i class='fa fa-phone'></i> " + source.Phone + "</td>" +
                "<td><i class='fa fa-home'></i>&nbsp;" + source.Address + "</td>" +
                "</tr>" +
                "</table>" +
                "</div>" +
                "</div>" +
                "</div>" +"";
            var contentString = "" +
                "" +
                "<div class='iw-container'>" +
                "<div class='iw-content'>" +
                "" + mapIWcontent +
                "</div>"+
                "<div class='iw-bottom-gradient'></div>" +
                "</div>" +
                "" +
                "";
            return contentString;
        }
        /// <summary>
        /// Converts string to Ids
        /// </summary>
        private string[] GetIdsFromString(string rawString)
        {
            return rawString.Split(',');
        }
        /// <summary>
        /// Send Email 
        /// </summary>
        public static void SendEmailTo(string email, string subject, string body, string fromDisplayName)
        {

            string fromAddress = ConfigurationManager.AppSettings["FromAddress"];
            string fromPwd = ConfigurationManager.AppSettings["FromPassword"];
            //string cc = ConfigurationManager.AppSettings["CC"];
            //string bcc = ConfigurationManager.AppSettings["BCC"];

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
            string enableSsl = ConfigurationManager.AppSettings["EnableSSL"];
            var client = new SmtpClient(smtpServer, Convert.ToInt32(smtpPort))
            {
                EnableSsl = enableSsl == "1",
                Credentials = new NetworkCredential(fromAddress, fromPwd)
            };
            client.Send(oEmail);
        }

        /// <summary>
        /// Add Count 
        /// </summary>
        private static void AddChildHireGroupCount(IEnumerable<WebApiParentHireGroupsApiResponse> parentHireGroups)
        {
            if (parentHireGroups == null)
                throw new ArgumentNullException("parentHireGroups");
            foreach (var pHireGroup in parentHireGroups)
            {
                if (pHireGroup.SubHireGroups != null && pHireGroup.SubHireGroups.Count > 0)
                {
                    pHireGroup.ChildHireGroupCount = "( " + pHireGroup.SubHireGroups.Count.ToString() + " Cars available )";
                }
                else
                {
                    pHireGroup.ChildHireGroupCount = "";
                }
            }
        }

        #endregion
        /// <summary>
        /// Constructor 
        /// </summary>
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
                    // Handling Bad Requests
                    if (response==null || response.SiteContent == null)
                    {
                        Session["siteName"] = "404 Error";
                        return RedirectToAction("Index", "ErrorHandler", new { area = "" });
                    }

                    // Setting up session 
                    var model = SettingRequestNSession(response);
                    return View(model);
            }
            return View();

        }


        /// <summary>
        /// Car Selection
        /// </summary>
        public ActionResult SelectCar(HomeModel model)
        {
            var newModel= ExtractDataFromReservationForm(model);
            var requestModel = MakeGetAvailableHireGroupsRequest(model);
            var parentHireGroups = rentalApiService.GetParentHireGroups(requestModel);
            AddChildHireGroupCount(parentHireGroups);
            if (parentHireGroups != null)
            {
                Session["HGDetail"] = ViewBag.HGDetail = parentHireGroups.Count == 0 ? null : parentHireGroups;
                Session["HGCount"] = parentHireGroups.Count;
            }
            return View(newModel);
        }


        /// <summary>
         /// Extra's Selection
         /// </summary>
        public ActionResult SelectExtras(string hireGroupDetailId)
         {
            ExtractHireGroupById(hireGroupDetailId);
            ExtrasResponseModel response = rentalApiService.GetExtras_Insurances(long.Parse(Session["UserDomainKey"].ToString()));
            ViewBag.Data =  Session["Extras"]=response;
            return View();
        }

        /// <summary>
        /// Send Email 
        /// </summary>
        [HttpPost]
        public JsonResult SendEmail(EmailModel email)
        {
            string companyEmail = Session["EmailForContact"].ToString();
            var emailContent = email;
            if (emailContent != null)
            {
                try
                {
                    SendEmailTo(companyEmail, emailContent.EmailSubject, emailContent.EmailBody, emailContent.SenderName);
                    return Json(new { status = "ok" });
                }
                catch (Exception excp)
                {
                    return Json(new { status = "error" });
                }

            }
            return Json(new { status = "error" });
        }

        /// <summary>
        /// Final Screen | Checkout
        /// </summary>
        [HttpGet]
        public ActionResult Checkout(string ServiceItemsIds, string InsurancesIds)
        {
            var model = MakCheckoutModel(ServiceItemsIds, InsurancesIds);
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
          
            return View();
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

        /// <summary>
        /// Booking Finalize
        /// </summary>
       // [HttpGet]
       // public ActionResult MakeBookingFinal(UserInfoModel model)
       // {
       // //   var onlineBookingModel= SetOnlineBookingModel(model);
       // //    var resposne = rentalApiService.OnlineBooking(onlineBookingModel);

       // //    Session.Clear();
       ////     Session.Abandon();
       //     return View();
       // }

        [HttpPost]
        public ActionResult MakeBookingFinal(UserInfoModel model)
        {
               var onlineBookingModel= SetOnlineBookingModel(model);
                var resposne = rentalApiService.OnlineBooking(onlineBookingModel);

            //    Session.Clear();
            //     Session.Abandon();
            return View();
        }
        #endregion
    }
}