
using System.Collections.Generic;

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

        /// <summary>
        /// Hire Group Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Number OF child hire group 
        /// </summary>
        public string ChildHireGroupCount { get; set; }

        /// <summary>
        /// List of children Hire Group 
        /// </summary>
        public List<WebApiHireGroupDetailResponse> SubHireGroups { set; get; }

        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Sub Title
        /// </summary>
        public string SubTitle { get; set; }

        /// <summary>
        /// Photo Url
        /// </summary>
        public string PhotoUrl { get; set; }
    }
}