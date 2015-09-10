
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
    }
}