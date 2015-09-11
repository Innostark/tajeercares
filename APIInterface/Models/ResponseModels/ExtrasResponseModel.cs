
using System.Collections.Generic;

namespace APIInterface.Models.ResponseModels
{
    /// <summary>
    /// Wrapper Model for Extras
    /// </summary>
    public class ExtrasResponseModel
    {

        public IEnumerable<ServiceItem> ServiceItems { get; set; }
        public IEnumerable<InsuranceType> InsuranceTypes { get; set; }
    }
}