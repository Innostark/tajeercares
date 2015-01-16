using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace APIInterface.Models
{
    public class MyPolicy : ICertificatePolicy
    {
        public bool CheckValidationResult(ServicePoint srvPoint,
           X509Certificate certificate, WebRequest request,
           int certificateProblem)
        {
            //Return True to force the certificate to be accepted.
            return true;
        }
    }
}