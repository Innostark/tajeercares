
namespace APIInterface.Models.ResponseModels
{
    public class WebApiHireGroupDetailResponse
    {
        /// <summary>
        /// Hire Group Detail Id
        /// </summary>
        public long HireGroupDetailId { get; set; }

        /// <summary>
        /// Hire Group
        /// </summary>
        public string HireGroup { get; set; }
        /// <summary>
        /// Hire Group Id
        /// </summary>
        public long HireGroupId { get; set; }
        /// <summary>
        /// Vehicle Make
        /// </summary>
        public string VehicleMake { get; set; }
        /// <summary>
        /// Vehicle Make
        /// </summary>
        public string VehicleModel { get; set; }
        /// <summary>
        /// Vehicle Category
        /// </summary>
        public string VehicleCategory { get; set; }
        /// <summary>
        /// Model Year
        /// </summary>
        public short ModelYear { get; set; }

        /// <summary>
        /// Standard Rate
        /// </summary>
        public double? StandardRt { get; set; }


        /// <summary>
        /// Tariff Type Code
        /// </summary>
        public string TariffType { get; set; }

        /// <summary>
        /// Hire Group Code
        /// </summary>
        public string HireGroupCodeName { get; set; }

        /// <summary>
        /// Hire Group Name
        /// </summary>
        public string HireGroupName { get; set; }

        /// <summary>
        /// Dropoff Charge
        /// </summary>
        public double? DropoffCharge { get; set; }

        /// <summary>
        /// Hire Group Description
        /// </summary>
        public string Description { get; set; }


        /// <summary>
        /// Vehicle Category ID
        /// </summary>
        public short VehicleCategoryId { get; set; }

        /// <summary>
        /// Vehicle Make ID
        /// </summary>
        public short VehicleMakeId { get; set; }

        /// <summary>
        /// Vehicle Mode ld
        /// </summary>
        public short VehicleModelId { get; set; }


        /// <summary>
        /// Image path to hire group
        /// </summary>
        public string ImageUrl { get; set; }


        /// <summary>
        /// Image Description
        /// </summary>
        public string ImageDescription { get; set; }

        /// <summary>
        /// with coma
        /// </summary>
        public string FormatedStandardRate { get; set; }
    }
}