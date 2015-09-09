using System.Collections.Generic;

namespace APIInterface.Models
{
    public class SiteContentResponseModel
    {
        public Sitecontent SiteContent { get; set; }
        public IEnumerable<WebApiOperationWorkplace> OperationsWorkPlaces { get; set; }
    }
}