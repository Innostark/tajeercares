using System;

namespace APIInterface.Models.RequestModels
{
    public class GetServiceItemRateRequest
    {
        public DateTime RaCreationDateTime { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }


        public long ServiceItemId { get; set; }

        public int Quantity { get; set; }

        public long OperationId { get; set; }

        public long UserDomainKey { get; set; }
    }
}