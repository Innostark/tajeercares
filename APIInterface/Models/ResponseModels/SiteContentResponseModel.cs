using System.Collections.Generic;

namespace APIInterface.Models.ResponseModels
{
    public class SiteContentResponseModel
    {
        public Sitecontent SiteContent { get; set; }
        public IEnumerable<WebApiOperationWorkplace> OperationsWorkPlaces { get; set; }
    }
}