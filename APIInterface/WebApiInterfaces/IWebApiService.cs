using APIInterface.Models;

namespace APIInterface.WebApiInterfaces
{
    partial interface IRentalApiService
    {
        /// <summary>
        /// Get Contents from Cares
        /// </summary>
        string GetSitecontent(string url);
    }
}
