
namespace APIInterface.Models
{
    public class WebApiOperationWorkplace
    {
        /// <summary>
        /// Location name
        /// </summary>
        public string LocationName { get; set; }

        /// <summary>
        /// Workplace id
        /// </summary>
        public long OperationWorkplaceId { get; set; }

        /// <summary>
        /// City Id 
        /// </summary>
        public short? CityId { get; set; }

        /// <summary>
        /// Operation Id
        /// </summary>
        public long OperationId { get; set; }

        /// <summary>
        /// Longitude
        /// </summary>
        public string Longitude { get; set; }

        /// <summary>
        /// Latitude
        /// </summary>
        public string Latitude { get; set; }


        public string Address { get; set; }

        public string Phone { get; set; }

        public string RawString { get; set; }

        public string CoordinatesContents { get; set; }

        /// <summary>
        /// To show data in toastr
        /// </summary>
        public string ToastrData { get; set; }
    }
}