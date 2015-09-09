using System.Net;
using System.Text;
using System.Threading.Tasks;
using APIInterface.Models;
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

        

         #endregion
        #region Public 

        /// <summary>
        /// Get Contents from Cares
        /// </summary>
        public string GetSitecontent(string url)
        {
            GeneralRequest obj = new GeneralRequest {URL = url};
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
    }
}
  
