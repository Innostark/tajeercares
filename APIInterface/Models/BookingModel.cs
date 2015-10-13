
using System;
using System.Collections.Generic;

namespace APIInterface.Models
{
    /// <summary>
    /// Online Booking Model 
    /// </summary>
    public class BookingModel
    {
        /// <summary>
        /// Pickup location id
        /// </summary>
        public double PickUpLocationId { get; set; }

        /// <summary>
        /// Drop-off location id
        /// </summary>
        public double DropOffLocationId { get; set; }

        /// <summary>
        /// Pick up Operation Id
        /// </summary>
        public double PickupOperationId { get; set; }

        /// <summary>
        /// Pick-up Date Time
        /// </summary>
        public DateTime PickupDateTime { get; set; }

        /// <summary>
        /// Drop - off Date Time
        /// </summary>
        public DateTime DropoffDateTime { get; set; }

        /// <summary>
        /// User Domain Key
        /// </summary>
        public long UserDomainKey { get; set; }

        /// <summary>
        /// Hire Group Detail Id
        /// </summary>
        public double HireGroupDetailId { get; set; }

        /// <summary>
        /// Drop-off charges for hire group 
        /// </summary>
        public double DropOffCharges { get; set; }

        /// <summary>
        /// Standard Rate
        /// </summary>
        public double StandardRate { get; set; }

        /// <summary>
        /// Tariff Type
        /// </summary>
        public String TariffType { get; set; }

        /// <summary>
        /// List of Service Items
        /// </summary>
        public IEnumerable<double> ServiceItems { get; set; }

        /// <summary>
        /// List Insurance Types
        /// </summary>
        public IEnumerable<double> InsuranceTypes { get; set; }

        /// <summary>
        /// Equals to standard Rate
        /// </summary>
        public double SubTotal { get; set; }

        /// <summary>
        /// Includes extras + Insurances + SubTotal
        /// </summary>
        public double FullTotal { get; set; }

        /// <summary>
        /// booking User Info
        /// </summary>
        public UserInfoModel UserInfo { get; set; }

        /// <summary>
        /// Existing customer id 
        /// </summary>
        public int BusinessPartnerId { get; set; }


    }
}