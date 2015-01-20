using APIInterface.Models;

namespace APIInterface.WebApiInterfaces
{
    partial interface IWebApiService
    {
        /// <summary>
        /// Register user using APi
        /// </summary>
        string RegisterUser(RegisterViewModel model);
    }
}
