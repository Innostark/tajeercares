using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

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


        [Display(Name = "Age")]
        public string Age { get; set; }

        [Required]
        [Display(Name = "City")]
        public string City { get; set; }


        [Required]
        [Display(Name = "PostalCode")]
        public string PostalCode { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        

        [Display(Name = "Retype Email Address")]
        [Compare("Email", ErrorMessage = "The email and confirmation email do not match!")]
        public string ConfirmEmail { get; set; }


        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

      

        [Required]
        [Display(Name = "Billing address")]
        public string BillingAddress { get; set; }

        [Required]
        [Display(Name = "Country")]
        public string CountryName { get; set; }


        

        /// <summary>
        /// List of all countries
        /// </summary>
        public List<string> CountryList { get; set; } 
    }
}