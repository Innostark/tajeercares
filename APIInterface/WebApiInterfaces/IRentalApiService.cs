
using System.Collections.Generic;
using APIInterface.Models.RequestModels;
using APIInterface.Models.ResponseModels;

namespace APIInterface.WebApiInterfaces
{
    public interface IRentalApiService
    {
        /// <summary>
        /// Get Contents from Cares
        /// </summary>
        string GetSitecontent(string url);


        /// <summary>
        /// Get Parent Hire Groups via APis
        /// </summary>
        string GetParentHireGroups(WebApiGetAvailableHireGroupsRequest request);

        /// <summary>
        /// Get Hire Group Detail
        /// </summary>
        string GetHireGroupDetail(WebApiGetAvailableHireGroupsRequest request);

    }
}
