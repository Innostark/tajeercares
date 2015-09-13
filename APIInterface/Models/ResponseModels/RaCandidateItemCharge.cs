
namespace APIInterface.Models.ResponseModels
{
    /// <summary>
    /// Charge for Service Items 
    /// </summary>
    public class RaCandidateItemCharge
    {
        /// <summary>
        /// Rate
        /// </summary>
        public double Rate { get; set; }

        /// <summary>
        /// Tariff Type Code
        /// </summary>
        public string TariffTypeCode { get; set; }

        /// <summary>
        /// Total Charge
        /// </summary>
        public double Charge { get; set; }
    }
}