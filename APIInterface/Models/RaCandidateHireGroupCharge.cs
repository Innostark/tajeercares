﻿
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
        /// Period in dasys & Minutes 
        /// </summary>
        public string CalculatedPeriod { get; set; }

        #endregion
    }
}