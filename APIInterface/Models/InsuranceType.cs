
namespace APIInterface.Models
{
    public class InsuranceType
    {
        /// <summary>
        ///Insurance Type Id
        /// </summary>
        public short InsuranceTypeId { get; set; }

        /// <summary>
        /// Insurance Type Code
        /// </summary>
        public string InsuranceTypeCode { get; set; }

        /// <summary>
        /// Insurance Type Name
        /// </summary>
        public string InsuranceTypeName { get; set; }

        /// <summary>
        /// Insurance Type Description
        /// </summary>
        public string InsuranceTypeDescription { get; set; }


        /// <summary>
        /// Rate
        /// </summary>
        public double InsuranceRate { get; set; }

        /// <summary>
        /// Charge
        /// </summary>
        public double InsuranceCharge { get; set; }
    }
}