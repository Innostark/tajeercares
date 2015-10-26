
using System.Collections.Generic;
using APIInterface.Models;
using APIInterface.Models.RequestModels;
using APIInterface.Models.ResponseModels;

namespace APIInterface.WebApiInterfaces
{
    public interface IRentalApiService
    {
        /// <summary>
        /// Get Contents from Cares
        /// </summary>
        SiteContentResponseModel GetSitecontent(string url);


        /// <summary>
        /// Get Parent Hire Groups via APis
        /// </summary>
        List<WebApiParentHireGroupsApiResponse> GetParentHireGroups(WebApiGetAvailableHireGroupsRequest request);

        /// <summary>
        /// Get Hire Group Detail
        /// </summary>
        string GetHireGroupDetail(WebApiGetAvailableHireGroupsRequest request);

        /// <summary>
        /// get Charge for hire group detail 
        /// </summary>
        RaCandidateHireGroupCharge GetHireGroupCharge(GetCandidateHireGroupChargeRequest request);

        /// <summary>
        /// Gets Extras & Insurances
        /// </summary>
        ExtrasResponseModel GetExtras_Insurances(long domainKey);

        /// <summary>
        /// Get Service Item Rate
        /// </summary>
        RaCandidateExtrasCharge GetServiceItemRate(GetServiceItemRateRequest request);


        /// <summary>
        /// Get Insurance Type Rate
        /// </summary>
        RaCandidateItemCharge GetInsuranceTypeRate(GetCandidateInsuranceChargeRequest request);

        /// <summary>
        /// Sets Final Booking to Cares API
        /// </summary>
        string OnlineBooking(BookingModel model);

        /// <summary>
        /// Sees if user is currently registered
        /// </summary>
        BusinessPartnerModel CheckUser(GeneralRequest key);

    }
}
