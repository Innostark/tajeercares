using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace APIInterface.Models
{
    /// <summary>
    /// For Booking Reservations
    /// </summary>
    public class ReservationForm
    {
        [Required]
        [Display(Name = "Pick-Up Location")]
        public string PickupLocation { get; set; }


        [Required]
        [Display(Name = "Pick-Up Date")]
        public DateTime PickupDateTime { get; set; }

        [Required]
        [Display(Name = "Pick-Up Hours")]
        public string PickupHours { get; set; }


        [Required]
        [Display(Name = "Drop-off Location")]
        public string DropoffLocation { get; set; }


        [Required]
        [Display(Name = "Drop-off Date")]
        public DateTime DropoffDateTime { get; set; }

        [Required]
        [Display(Name = "Drop-off Hours")]
        public string DropoffHours { get; set; }


        /// <summary>
        /// Hours Data for Reservation
        /// </summary>
        public IEnumerable<string> HoursList { get; set; }
    }
}