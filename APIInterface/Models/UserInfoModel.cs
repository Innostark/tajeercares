using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace APIInterface.Models
{
    /// <summary>
    /// User Info Model on Booking Confirmation 
    /// </summary>
    public class UserInfoModel
    {

        [Required]
        [Display(Name = "First Name")]
        public string FName { get; set; }


        [Required]
        [Display(Name = "Last Name")]
        public string LName { get; set; }




        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        



        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

      

        [Required]
        [Display(Name = "Billing address")]
        public string BillingAddress { get; set; }

        [Required]
        [Display(Name = "Date of Birht")]
        public DateTime DOB { get; set; }


        public double ServiceItemsTotal { get; set; }

        public double InsurancesTotal { get; set; }

        public double? SubTotal { get; set; }

        public string FormatedSubTotal { get; set; }
        public double? GrandTotal { get; set; }
        public string FormatedGrandTotal { get; set; }

        public List<string> ItemsHtml { get; set; }
    }
}