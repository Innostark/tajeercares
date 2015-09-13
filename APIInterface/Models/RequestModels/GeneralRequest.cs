
namespace APIInterface.Models.RequestModels
{
    /// <summary>
    /// Request Model for General Requests
    /// </summary>
    public class GeneralRequest
    {
        /// <summary>
        /// Url parm
        /// </summary>
        public string URL { get; set; }

        /// <summary>
        /// Domain Key
        /// </summary>
        public long DomainKey { get; set; }
    }
}