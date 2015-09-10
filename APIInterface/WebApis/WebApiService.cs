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
    public class WebApiService : ApiService,IWebApiService
    {
        #region Private
         private readonly HttpClient client = new HttpClient();
         private string RegisterUserUri
        {
            get
            {
                return ApiResources.BaseAddress + ApiResources.RegisterUser;
            }
        }

         private string UrlAvailabilityUri
         {
             get
             {
                 return ApiResources.BaseAddress + ApiResources.UserAvailability;
             }
         }

         #endregion
        #region Public

         #region Register User
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
         #region Check URL Availability
         /// <summary>
         /// Checks if URl is available 
         /// </summary>
         public string CheckCompanyUrlAvailability(string url)
         {
             Task<string> registerUserAsync = CheckAvailabiblityAsync(url);
             return registerUserAsync.Result;
         }
         /// <summary>
         /// Register User Api Call
         /// </summary>
         private async Task<string> CheckAvailabiblityAsync(string url)
         {
             GeneralRequest obj = new GeneralRequest { URL = url };
             string urlContents = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
             HttpResponseMessage responseMessage = await GetHttpRequestAsync(urlContents, new Uri(UrlAvailabilityUri)).ConfigureAwait(false);
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
  
