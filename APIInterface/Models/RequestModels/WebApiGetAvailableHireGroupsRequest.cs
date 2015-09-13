using System;

namespace APIInterface.Models.RequestModels
{
    /// <summary>
    /// For Getting HGs
    /// </summary>
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

        /// <summary>
        /// PickUp City Id
        /// </summary>
        public short? PickUpCityId { get; set; }

        /// <summary>
        /// DropOff City Id
        /// </summary>
        public short? DropOffCityId { get; set; }

    }
}