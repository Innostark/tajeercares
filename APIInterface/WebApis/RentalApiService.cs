using System.Collections.Generic;
using System.Web.Script.Serialization;
using APIInterface.Models;
using APIInterface.Models.RequestModels;
using APIInterface.Models.ResponseModels;
using APIInterface.Resources;
using APIInterface.WebApiInterfaces;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace APIInterface.WebApis
{
    /// <summary>
    /// Web Api Service class
    /// </summary>
    public class RentalApiService : ApiService,IRentalApiService
    {
        #region Private
         private readonly HttpClient client = new HttpClient();

        /// <summary>
        /// Uris For Apis Requests
        /// </summary>
         private string GetSiteContentsUri
        {
            get
            {
                return ApiResources.BaseAddress + ApiResources.GetSiteContents;
            }
        }
         private string ParentHireGroupUri
         {
             get
             {
                 return ApiResources.BaseAddress + ApiResources.ParentHireGroup;
             }
         }
         private string HireGroupDetailUri
         {
             get
             {
                 return ApiResources.BaseAddress + ApiResources.HireGroupDetail;
             }
         }
         private string HireGroupRateUri
         {
             get
             {
                 return ApiResources.BaseAddress + ApiResources.HireGroupRate;
             }
         }
         private string ExtrasInsuranceUri
         {
             get
             {
                 return ApiResources.BaseAddress + ApiResources.ExtrasInsurances;
             }
         }
         private string ServiceItemRateUri
         {
             get
             {
                 return ApiResources.BaseAddress + ApiResources.ServiceItemRate;
             }
         }
         private string InsuranceTypeRateUri
         {
             get
             {
                 return ApiResources.BaseAddress + ApiResources.InsuranceTypeRate;
             }
         }
         private string BookingMainUri
         {
             get
             {
                 return ApiResources.BaseAddress + ApiResources.BookingMain;
             }
         }


         /// <summary>
         /// Converts String To Byte Array
         /// </summary>
         private static byte[] GetBytes(string str)
         {
             
             var bytes = new byte[str.Length * sizeof(char)];
             System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
             return bytes;
         }
         #endregion
        #region Public 

         #region Site Contents
         /// <summary>
         /// Get Contents from Cares
         /// </summary>
         public SiteContentResponseModel GetSitecontent(string url)
         {
             var obj = new GeneralRequest { URL = url };
             Task<string> sitecontentAsync = GetSitecontentAsync(obj);
             var response = sitecontentAsync.Result;
             if (response != "null")
             {
                 var jss = new JavaScriptSerializer();
                 try
                 {
                     var data = jss.Deserialize<SiteContentResponseModel>(response);
                     if(data.SiteContent==null || data.OperationsWorkPlaces==null)
                         return null;
                     data.SiteContent.LogoSourceLocal = data.SiteContent.CompanyLogoBytes == null ? null : GetBytes(data.SiteContent.CompanyLogoBytes)  ;
                     data.SiteContent.Banner1SourceLocal = data.SiteContent.Banner1Bytes ==null ? null : GetBytes(data.SiteContent.Banner1Bytes);
                     return data;
                 }
                 catch (Exception exc)
                 {
                     throw new Exception("Error while getting data from server!");
                 }
             }
             return null;
         }

         /// <summary>
         /// Register User Api Call
         /// </summary>
         private async Task<string> GetSitecontentAsync(GeneralRequest model)
         {
             string orderContents = Newtonsoft.Json.JsonConvert.SerializeObject(model);
             HttpResponseMessage responseMessage = await PostHttpRequestAsync(orderContents, new Uri(GetSiteContentsUri)).ConfigureAwait(false);
             if (responseMessage == null)
             {
                 return "Failure";
             }
             if (responseMessage.IsSuccessStatusCode)
             {
                 string response = await responseMessage.Content.ReadAsStringAsync();
                 return response;
             }
             string result = await responseMessage.Content.ReadAsStringAsync();
             return result;
         }
        #endregion
         #region HG
        /// <summary>
         /// Get Parent Hire Groups via APis
        /// </summary>
        public List<WebApiParentHireGroupsApiResponse> GetParentHireGroups(WebApiGetAvailableHireGroupsRequest request)
        {
           Task<string> data= GetParentHireGroupsAsync(request);
            var response= data.Result;
             var rawData = new JavaScriptSerializer();
            return rawData.Deserialize<List<WebApiParentHireGroupsApiResponse>>(response);
        }

        /// <summary>
        /// Register User Api Call
        /// </summary>
        private async Task<string> GetParentHireGroupsAsync(WebApiGetAvailableHireGroupsRequest request)
        {
            string urlContents = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            HttpResponseMessage responseMessage = await GetHttpRequestAsync(urlContents, new Uri(ParentHireGroupUri)).ConfigureAwait(false);
            if (responseMessage == null)
            {
                return "Failure";
            }
            if (responseMessage.IsSuccessStatusCode)
            {
                string response = await responseMessage.Content.ReadAsStringAsync();
                return response;
            }
            string result = await responseMessage.Content.ReadAsStringAsync();
            return result;
        }



        #endregion
         #region HG Detail
        /// <summary>
        /// Get Parent Hire Groups via APis
        /// </summary>
        public string GetHireGroupDetail(WebApiGetAvailableHireGroupsRequest request)
        {
            Task<string> response = GetHireGroupDetailsAsync(request);
            return response.Result;
        }

        /// <summary>
        /// Register User Api Call
        /// </summary>
        private async Task<string> GetHireGroupDetailsAsync(WebApiGetAvailableHireGroupsRequest request)
        {
            string urlContents = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            HttpResponseMessage responseMessage = await GetHttpRequestAsync(urlContents, new Uri(HireGroupDetailUri)).ConfigureAwait(false);
            if (responseMessage == null)
            {
                return "Failure";
            }
            if (responseMessage.IsSuccessStatusCode)
            {
                string response = await responseMessage.Content.ReadAsStringAsync();
                return response;
            }
            string result = await responseMessage.Content.ReadAsStringAsync();
            return result;
        }



        #endregion
         #region Hire Group Charge

        /// <summary>
        /// get Charge for hire group detail 
        /// </summary>
        public RaCandidateHireGroupCharge GetHireGroupCharge(GetCandidateHireGroupChargeRequest request)
        {
            var response= GetChargeAsync(request).Result;
             var rawData = new JavaScriptSerializer();
             return rawData.Deserialize<RaCandidateHireGroupCharge>(response);
        }

       

        /// <summary>
        /// Register User Api Call
        /// </summary>
        private async Task<string> GetChargeAsync(GetCandidateHireGroupChargeRequest request)
        {
            string urlContents = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            HttpResponseMessage responseMessage = await GetHttpRequestAsync(urlContents, new Uri(HireGroupRateUri)).ConfigureAwait(false);
            if (responseMessage == null)
            {
                return "Failure";
            }
            if (responseMessage.IsSuccessStatusCode)
            {
                string response = await responseMessage.Content.ReadAsStringAsync();
                return response;
            }
            string result = await responseMessage.Content.ReadAsStringAsync();
            return result;
        }
        #endregion
         #region Extras & Insurances
        /// <summary>
        /// Get Extras n Insurances
        /// </summary>
        public ExtrasResponseModel GetExtras_Insurances(long domainKey)
        {
            var rawResponse = GetExtras_InsurancesAsync(domainKey).Result;

            var rawData = new JavaScriptSerializer();
            try
            {
                 return rawData.Deserialize<ExtrasResponseModel>(rawResponse);
            }
            catch (Exception exc)
            {
                throw new Exception("Error while getting data from server!");
            }
        }


        /// <summary>
        /// Get Extras n Insurances api async
        /// </summary>
        private async Task<string> GetExtras_InsurancesAsync(long domainkey)
        {
            var request = new GeneralRequest {DomainKey = domainkey,URL = null};
            string urlContents = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            HttpResponseMessage responseMessage = await GetHttpRequestAsync(urlContents, new Uri(ExtrasInsuranceUri)).ConfigureAwait(false);
            if (responseMessage == null)
            {
                return "Failure";
            }
            if (responseMessage.IsSuccessStatusCode)
            {
                string response = await responseMessage.Content.ReadAsStringAsync();
                return response;
            }
            string result = await responseMessage.Content.ReadAsStringAsync();
            return result;
        }



        /// <summary>
        /// Get Service Item Rate
        /// </summary>
        public RaCandidateExtrasCharge GetServiceItemRate(GetServiceItemRateRequest request)
        {
            var response= GetServiceItemRateAsync(request).Result;
            var rawData = new JavaScriptSerializer();
            return rawData.Deserialize<RaCandidateExtrasCharge>(response);
        }


        /// <summary>
        ///  Get Service Item Rate api async
        /// </summary>
        private async Task<string> GetServiceItemRateAsync(GetServiceItemRateRequest request)
        {
            string urlContents = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            HttpResponseMessage responseMessage = await GetHttpRequestAsync(urlContents, new Uri(ServiceItemRateUri)).ConfigureAwait(false);
            if (responseMessage == null)
            {
                return "Failure";
            }
            if (responseMessage.IsSuccessStatusCode)
            {
                string response = await responseMessage.Content.ReadAsStringAsync();
                return response;
            }
            string result = await responseMessage.Content.ReadAsStringAsync();
            return result;
        }

        /// <summary>
        /// Get Insurance Type Rate
        /// </summary>
        public RaCandidateItemCharge GetInsuranceTypeRate(GetCandidateInsuranceChargeRequest request)
        {
            var rawResponse = GetInsuranceTypeRateAsync(request).Result;
            var rawData = new JavaScriptSerializer();
           return rawData.Deserialize<RaCandidateItemCharge>(rawResponse);
        }


        /// <summary>
        /// Get Insurance Type Rate Asyns
        /// </summary>
        private async Task<string> GetInsuranceTypeRateAsync(GetCandidateInsuranceChargeRequest request)
        {
            string urlContents = Newtonsoft.Json.JsonConvert.SerializeObject(request);
            HttpResponseMessage responseMessage = await GetHttpRequestAsync(urlContents, new Uri(InsuranceTypeRateUri)).ConfigureAwait(false);
            if (responseMessage == null)
            {
                return "Failure";
            }
            if (responseMessage.IsSuccessStatusCode)
            {
                string response = await responseMessage.Content.ReadAsStringAsync();
                return response;
            }
            string result = await responseMessage.Content.ReadAsStringAsync();
            return result;
        }
        #endregion
         #region Booking Main

        /// <summary>
        /// Sets Final Booking to Cares API
        /// </summary>
        public string OnlineBooking(BookingModel model)
        {
            return SetBookingMainAsync(model).Result;
        }

        /// <summary>
        /// Get Extras n Insurances api async
        /// </summary>
        private async Task<string> SetBookingMainAsync(BookingModel model)
        {
            string urlContents = Newtonsoft.Json.JsonConvert.SerializeObject(model);
            HttpResponseMessage responseMessage = await GetHttpRequestAsync(urlContents, new Uri(BookingMainUri)).ConfigureAwait(false);
            if (responseMessage == null)
            {
                return "Failure";
            }
            if (responseMessage.IsSuccessStatusCode)
            {
                string response = await responseMessage.Content.ReadAsStringAsync();
                return response;
            }
            string result = await responseMessage.Content.ReadAsStringAsync();
            return result;
        }
        #endregion
        #endregion
    }
}
  
