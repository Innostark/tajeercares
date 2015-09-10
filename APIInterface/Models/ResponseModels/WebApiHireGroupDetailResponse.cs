
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
    }
}