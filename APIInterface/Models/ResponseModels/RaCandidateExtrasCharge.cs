
namespace APIInterface.Models.ResponseModels
{
    /// <summary>
    /// Service Charge for Insurances 
    /// </summary>
    public class RaCandidateExtrasCharge
    {

        #region Persisted Properties

        /// <summary>
        /// Service Rate
        /// </summary>
        public double ServiceRate { get; set; }

        /// <summary>
        /// Tariff Type Code
        /// </summary>
        public string TariffTypeCode { get; set; }

        /// <summary>
        /// Service Charge
        /// </summary>
        public double ServiceCharge { get; set; }

        #endregion
    }
}