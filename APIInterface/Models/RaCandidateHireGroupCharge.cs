
namespace APIInterface.Models
{
    public class RaCandidateHireGroupCharge
    {
        #region Persisted Properties

        /// <summary>
        /// Standard Rate
        /// </summary>
        public double StandardRate { get; set; }

        /// <summary>
        /// Tariff Type Code
        /// </summary>
        public string TariffTypeCode { get; set; }

        /// <summary>
        /// Total Standard Charge
        /// </summary>
        public double TotalStandardCharge { get; set; }

        /// <summary>
        /// Per Day Charge
        /// </summary>
        public string PerDayCost { get; set; }

        #endregion
    }
}