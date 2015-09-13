
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace APIInterface.Models
{
    /// <summary>
    /// For User Registration 
    /// </summary>
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Retype password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Account Type")]
        public int AccountType { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string CompanyAddress { get; set; }

        [Required]
        [Display(Name = "Country")]
        public string CountryName { get; set; }


        [Required]
        [Display(Name = "Company Short-URL ")]
        public string ShortUrl { get; set; }

        /// <summary>
        /// List of all countries
        /// </summary>
        public List<string> CountryList { get; set; } 
    }
}