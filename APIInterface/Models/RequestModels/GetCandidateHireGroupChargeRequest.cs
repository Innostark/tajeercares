using System;

namespace APIInterface.Models.RequestModels
{
    /// <summary>
    /// For HG Charge
    /// </summary>
    public class GetCandidateHireGroupChargeRequest
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
        /// Hire Group Detail Id
        /// </summary>
        public long HireGroupDetailId { get; set; }

        /// <summary>
        /// User Domain key
        /// </summary>
        public long UserDomainKey { get; set; }
    }
}