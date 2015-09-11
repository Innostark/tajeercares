using System;

namespace APIInterface.Models
{
    public class InsuranceRtMainContent
    {
        /// <summary>
        /// Insurance Rate Main ID
        /// </summary>
        public long InsuranceRtMainId { get; set; }
        /// <summary>
        /// Standard Rate Main Code
        /// </summary>
        public string InsuranceRtMainCode { get; set; }
        /// <summary>
        ///Insurance Rate Main Name
        /// </summary>
        public string InsuranceRtName { get; set; }
        /// <summary>
        /// Insurance Rate Main Description
        /// </summary>
        public string InsuranceRtMainDescription { get; set; }

        /// <summary>
        /// Tariff Type Code
        /// </summary>
        public string TariffTypeCodeName { get; set; }

        /// <summary>
        /// Tariff Type ID
        /// </summary>
        public long TariffTypeId { get; set; }

        /// <summary>
        /// Start Date
        /// </summary>
        public DateTime StartDt { get; set; }
        /// <summary>
        /// Operation Id
        /// </summary>
        public long OperationId { get; set; }

        /// <summary>
        /// Operation Code Name
        /// </summary>
        public string OperationCodeName { get; set; }
    }
}