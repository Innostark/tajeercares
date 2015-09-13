using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using APIInterface.Models;
using APIInterface.Models.RequestModels;
using APIInterface.Models.ResponseModels;
using APIInterface.Resources;
using APIInterface.WebApiInterfaces;
using System;
using System.Net.Http;

namespace APIInterface.WebApis
{
    /// <summary>
    /// Web Api Service class
    /// </summary>
    public class RentalApiService : ApiService,IRentalApiService
    {
        #region Private
         private readonly HttpClient client = new HttpClient();
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
         #endregion
        #region Public 

         #region Site Contents
         /// <summary>
         /// Get Contents from Cares
         /// </summary>
         public string GetSitecontent(string url)
         {
             var obj = new GeneralRequest { URL = url };
             Task<string> sitecontentAsync = GetSitecontentAsync(obj);
             return sitecontentAsync.Result;
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
        public string GetParentHireGroups(WebApiGetAvailableHireGroupsRequest request)
        {
           Task<string> response= GetParentHireGroupsAsync(request);
            return response.Result;
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
        public string GetCharge(GetCandidateHireGroupChargeRequest request)
        {
            return GetChargeAsync(request).Result;
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
        public string GetExtras_Insurances(long domainKey)
        {
            return GetExtras_InsurancesAsync(domainKey).Result;
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
        public string GetServiceItemRate(GetServiceItemRateRequest request)
        {
            return GetServiceItemRateAsync(request).Result;
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
        #endregion
        #endregion
    }
}
  
