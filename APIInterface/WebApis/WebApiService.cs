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
    public class WebApiService : ApiService,IWebApiService
    {
        #region Private
         private readonly HttpClient client = new HttpClient();
         private string RegisterUserUri
        {
            get
            {
                return ApiResources.WebApiBaseAddress + ApiResources.RegisterUser;
            }
        }
         #endregion
        #region Public 
         /// <summary>
        /// Register user using APi
        /// </summary>
        public bool RegisterUser(RegisterViewModel model)
        {
            Task<bool> registerUserAsync = RegisterUserAsync(model);
            return true;
        }

        /// <summary>
        /// Register User Api Call
        /// </summary>
        private async Task<bool> RegisterUserAsync(RegisterViewModel model)
        {
            string orderContents = Newtonsoft.Json.JsonConvert.SerializeObject(model);
            HttpResponseMessage responseMessage = await PostHttpRequestAsync(orderContents, new Uri(RegisterUserUri)).ConfigureAwait(false);
            if (responseMessage.IsSuccessStatusCode)
            {
                string response = await responseMessage.Content.ReadAsStringAsync();
                return true;
            }
            await responseMessage.Content.ReadAsStringAsync();
            return false;
        }
       
       #endregion
    }
}
  
