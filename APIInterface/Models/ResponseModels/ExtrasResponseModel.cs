
using System.Collections.Generic;

namespace APIInterface.Models.ResponseModels
{
    /// <summary>
    /// Wrapper Model for Extras
    /// </summary>
    public class ExtrasResponseModel
    {
        /// <summary>
        /// List of Service Items
        /// </summary>
        public IEnumerable<ServiceItem> ServiceItems { get; set; }

        /// <summary>
        /// List Of Insurance Types
        /// </summary>
        public IEnumerable<InsuranceType> InsuranceTypes { get; set; }
    }
}