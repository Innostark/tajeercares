using System;

namespace APIInterface.Models.ResponseModels
{
    public class BusinessPartnerModel
    {
        /// <summary>
        /// Existing BP Id
        /// </summary>
        public double BusinessPartnerId { get; set; }

        /// <summary>
        /// BP First Name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// BP Last name
        /// </summary>
        public string LName { get; set; }

        /// <summary>
        /// BP Phone
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// BP's email address
        /// </summary>
        public string Email { get; set; }


        /// <summary>
        /// Address of BP
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Date of Birth
        /// </summary>
        public DateTime DOB { get; set; }

        public string DOB_String { get; set; }
    }
}