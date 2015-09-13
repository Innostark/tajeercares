
using APIInterface.Models.RequestModels;

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

        /// <summary>
        /// get Charge for hire group detail 
        /// </summary>
        string GetCharge(GetCandidateHireGroupChargeRequest request);

        /// <summary>
        /// Gets Extras & Insurances
        /// </summary>
        string GetExtras_Insurances(long domainKey);

        /// <summary>
        /// Get Service Item Rate
        /// </summary>
        string GetServiceItemRate(GetServiceItemRateRequest request);


        /// <summary>
        /// Get Insurance Type Rate
        /// </summary>
        string GetInsuranceTypeRate(GetCandidateInsuranceChargeRequest request);

    }
}
