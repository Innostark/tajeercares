﻿
namespace APIInterface.Models.ResponseModels
{
    public class WebApiParentHireGroupsApiResponse
    {
        /// <summary>
        /// Hire Group Id
        /// </summary>
        public long HireGroupId { get; set; }
        /// <summary>
        /// Hire Group Code
        /// </summary>
        public string HireGroupCodeName { get; set; }

        /// <summary>
        /// Hire Group Name
        /// </summary>
        public string HireGroupName { get; set; }

        /// <summary>
        /// Dropoff Charge
        /// </summary>
        public double? DropoffCharge { get; set; }
    }
}