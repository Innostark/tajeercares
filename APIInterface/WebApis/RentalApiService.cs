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

         #endregion
        #region Public 

         #region Site Contents
         /// <summary>
         /// Get Contents from Cares
         /// </summary>
         public string GetSitecontent(string url)
         {
             GeneralRequest obj = new GeneralRequest { URL = url };
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

       #endregion
    }
}
  
