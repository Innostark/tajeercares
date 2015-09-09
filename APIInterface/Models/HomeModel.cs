
using System.Collections.Generic;

namespace APIInterface.Models
{
    public class HomeModel
    {
        public ReservationForm ReservationForm { get; set; }
        public Sitecontent Sitecontent { get; set; }
        public IEnumerable<WebApiOperationWorkplace> OperationsWorkPlaces { get; set; }
    }
}