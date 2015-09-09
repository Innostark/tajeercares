using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace APIInterface.Models
{
    public class ReservationForm
    {
        [Required]
        [Display(Name = "Pick-Up Location")]
        public string PickupLocation { get; set; }


        [Required]
        [Display(Name = "Pick-Up Date Time")]
        public DateTime PickupDateTime { get; set; }


        [Required]
        [Display(Name = "Drop-off Location")]
        public string DropoffLocation { get; set; }


        [Required]
        [Display(Name = "Drop-off Date Time")]
        public DateTime DropoffDateTime { get; set; }
    }
}