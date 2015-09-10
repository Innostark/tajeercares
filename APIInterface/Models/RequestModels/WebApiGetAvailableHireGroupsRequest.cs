using System;

namespace APIInterface.Models.RequestModels
{
    public class WebApiGetAvailableHireGroupsRequest
    {

        /// <summary>
        /// Start Date Time
        /// </summary>
        public DateTime StartDateTime { get; set; }
        /// <summary>
        /// End Date Time
        /// </summary>
        public DateTime EndDateTime { get; set; }
        /// <summary>
        /// Out Location OpeartionWorkplaceId
        /// </summary>
        public long OutLocationId { get; set; }
        /// <summary>
        /// Return Location Operation Workplace Id
        /// </summary>
        public long ReturnLocationId { get; set; }

        /// <summary>
        /// Domain Key
        /// </summary>
        public long DomainKey { get; set; }

        /// <summary>
        /// HireGroupId
        /// </summary>
        public long HireGroupId { get; set; }

        public short? PickUpCityId { get; set; }
        public short? DropOffCityId { get; set; }

    }
}