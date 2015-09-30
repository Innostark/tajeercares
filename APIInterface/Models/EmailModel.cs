
namespace APIInterface.Models
{
    public class EmailModel
    {
        /// <summary>
        /// Email sender name
        /// </summary>
        public string SenderName { get; set; }

        /// <summary>
        /// Sender's Email
        /// </summary>
        public string SenderEmail { get; set; }

        /// <summary>
        /// Email subject
        /// </summary>
        public string EmailSubject { get; set; }

        /// <summary>
        /// Email body 
        /// </summary>
        public string EmailBody { get; set; }

        /// <summary>
        /// To Email 
        /// </summary>
        public string RecipientEmail { get; set; }
    }
}