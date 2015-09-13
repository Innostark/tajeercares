using System.Collections.Generic;

namespace APIInterface.Models.ResponseModels
{
    /// <summary>
    /// Site Contents Model 
    /// </summary>
    public class SiteContentResponseModel
    {
        /// <summary>
        /// Site Contents 
        /// </summary>
        public Sitecontent SiteContent { get; set; }

        /// <summary>
        /// Operations Work Places For Reservation Form 
        /// </summary>
        public IEnumerable<WebApiOperationWorkplace> OperationsWorkPlaces { get; set; }
    }
}