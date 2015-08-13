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
        public string RegisterUser(RegisterViewModel model)
        {
            Task<string> registerUserAsync = RegisterUserAsync(model);
            return registerUserAsync.Result;
        }

        /// <summary>
        /// Register User Api Call
        /// </summary>
        private async Task<string> RegisterUserAsync(RegisterViewModel model)
        {
            string orderContents = Newtonsoft.Json.JsonConvert.SerializeObject(model);
            HttpResponseMessage responseMessage = await PostHttpRequestAsync(orderContents, new Uri(RegisterUserUri)).ConfigureAwait(false);
            if (responseMessage == null)
            {
                return "Failure";
            }
            if (responseMessage.IsSuccessStatusCode)
            {
                string response = await responseMessage.Content.ReadAsStringAsync();
                return "Success";
            }
            string result = await responseMessage.Content.ReadAsStringAsync();
            return result;
        }
       
       #endregion
    }
}
  
