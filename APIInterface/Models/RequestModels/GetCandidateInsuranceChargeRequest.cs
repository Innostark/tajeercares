using System;

namespace APIInterface.Models.RequestModels
{
    public class GetCandidateInsuranceChargeRequest
    {
        /// <summary>
        /// Operation
        /// </summary>
        public long OperationId { get; set; }

        /// <summary>
        /// Start Date Time
        /// </summary>
        public DateTime StartDtTime { get; set; }

        /// <summary>
        /// End Date Time
        /// </summary>
        public DateTime EndDtTime { get; set; }

        /// <summary>
        /// RA Creation Dt
        /// </summary>
        public DateTime RaCreatedDate { get; set; }

        /// <summary>
        /// Insurance Type Id
        /// </summary>
        public short InsuranceTypeId { get; set; }

        /// <summary>
        /// Hire Group Detail Id
        /// </summary>
        public long HireGroupDetailId { get; set; }

        /// <summary>
        /// Domain key user
        /// </summary>
        public long Domainkey { get; set; }
    }
}