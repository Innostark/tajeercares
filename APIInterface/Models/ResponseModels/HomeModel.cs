
using System.Collections.Generic;

namespace APIInterface.Models.ResponseModels
{
    /// <summary>
    /// Response Model On Index Page
    /// </summary>
    public class HomeModel
    {
        /// <summary>
        /// Reservation Form Data
        /// </summary>
        public ReservationForm ReservationForm { get; set; }

        /// <summary>
        /// User's Site Contents 
        /// </summary>
        public Sitecontent Sitecontent { get; set; }

        /// <summary>
        /// Operations Work Places For Reservation Form 
        /// </summary>
        public IEnumerable<WebApiOperationWorkplace> OperationsWorkPlaces { get; set; }

        /// <summary>
        /// For Contact Us Form
        /// </summary>
        public EmailModel EmailModel { get; set; }
    }
}