using System;

namespace APIInterface.Models.RequestModels
{
    /// <summary>
    /// For Getting Rate of Service Item
    /// </summary>
    public class GetServiceItemRateRequest
    {
        /// <summary>
        /// Rental Agreement Opening Time 
        /// </summary>
        public DateTime RaCreationDateTime { get; set; }

        /// <summary>
        /// Booking Start Date Time
        /// </summary>
        public DateTime StartDateTime { get; set; }

        /// <summary>
        /// Booking End Date Time
        /// </summary>
        public DateTime EndDateTime { get; set; }

        /// <summary>
        /// Service Item Id [Extra]
        /// </summary>
        public long ServiceItemId { get; set; }

        /// <summary>
        /// Quantity of items
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Operation Id
        /// </summary>
        public long OperationId { get; set; }

        /// <summary>
        /// User Domain key 
        /// </summary>
        public long UserDomainKey { get; set; }
    }
}