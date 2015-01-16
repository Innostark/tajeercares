using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using APIInterface.Models;
using APIInterface.Resources;

namespace APIInterface
{
    public class ApiService
    {
        #region Private

        private const string BasicAuthenticationUsernamePasswordFormat = "{0}:{1}";


        #endregion

        #region Protected

        protected readonly HttpClient Client = new HttpClient();

        /// <summary>
        /// Get request
        /// </summary>
        protected async Task<HttpResponseMessage> GetHttpRequestAsync(Uri uri)
        {
            HttpResponseMessage response = await Client.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            return response;
        }

        /// <summary>
        /// Post Request
        /// </summary>
        protected async Task<HttpResponseMessage> PostHttpRequestAsync(string content, Uri uri)
        {
            try
            {
                ServicePointManager.CertificatePolicy = new MyPolicy();
                HttpContent stringContent = new StringContent(content, Encoding.UTF8, "application/json");
                HttpResponseMessage messge = await Client.PostAsync(uri, stringContent).ConfigureAwait(false); ;
                return messge;
            }
            catch (Exception exp)
            {
                return new HttpResponseMessage { StatusCode = HttpStatusCode.ExpectationFailed };
            }
        }

        private void Exc()
        {

        }
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public ApiService()
        {
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(
            Encoding.ASCII.GetBytes(
                string.Format(BasicAuthenticationUsernamePasswordFormat,
                ApiResources.WebApiUserName,
                ApiResources.WebApiPassword))));
        }


        #endregion
    }
}