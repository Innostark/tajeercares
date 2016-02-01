using System.Linq;
using System.Web;
using System.Web.Routing;

namespace APIInterface.App_Start
{
    public class PermaLinkRouteConstraint : IRouteConstraint
    {
        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values,
            RouteDirection routeDirection)
        {
              
            var permaRoute = values[parameterName] as string;
            if (permaRoute == null)
                return false;

           string[] parms=  permaRoute.Split('/');
           if (parms.Count() == 2 && parms[0] == "Home" && parms[1] == "Index")
            {
                    return false;
            }
           if (parms.Count() == 2 && parms[0] == "Home" && parms[1] == "RegisterUser")
           {
               return false;
           }
           if (parms.Count() == 2 && parms[0] == "Home" && (parms[1] == "Features" || parms[1] == "features" || parms[1] == "overview" || parms[1] == "Overview"))
           {
               return false;
           }
           if (parms.Count() == 2 && parms[0] == "Home" && parms[1] == "CompanyURLAvailability")
           {
               return false;
           }

           if ((parms.Count() == 1 && parms[0] == "ChangeCulture") || (parms.Count() == 2 && parms[1] == "ChangeCulture" && parms[0]=="Home"))
           {
               values["controller"] = "Home";
               values["action"] = "ChangeCulture";
               values["customRoute"] = "";
               return false;
           }
           if ( parms.Count() == 2 &&  parms[0] == "Rental" &&parms[1] == "ChangeCulture")
           {
               values["controller"] = "Rental";
               values["action"] = "ChangeCulture";
               values["customRoute"] = "";
               return false;
           }
           if (parms.Count() == 3 && parms[1] == "Rental" && parms[2] == "ChangeCulture")
           {
           //    values["controller"] = "Rental";
            //   values["action"] = "ChangeCulture";
          //     values["customRoute"] = parms[0];
               return false;
           }
           if (parms[0] == "ErrorHandler" )
           {
               values["controller"] = "ErrorHandler";
               values["action"] = "Index";
               values["customRoute"] = "";
               return false;
           }
           if (parms.Count() == 2 && parms[0] == "Rental" && parms[1] == "Index")
           {
               return true;
           }

           if (parms.Count() == 3 && parms[1] == "Rental" && parms[2] == "SelectCar")
           {
               values["controller"] = "Rental";
               values["action"] = "SelectCar";
               values["customRoute"] = parms[0]+"/Rental/SelectExtras";
               return true;
           }


           if (parms.Count() == 3 && parms[1] == "Rental" && parms[2] == "SelectExtras")
           {
               values["controller"] = "Rental";
               values["action"] = "SelectExtras";
               values["customRoute"] = parms[0] + "/Rental/SelectExtras";
               return true;
           }

           if (parms.Count() == 2 && parms[0] == "Rental" && parms[1] == "SendEmail")
           {
               values["controller"] = "Rental";
               values["action"] = "SendEmail";
               //values["customRoute"] = parms[0] + "/Rental/SendEmail";
               return true;
           }
           if (parms.Count() == 2 && parms[0] == "Home" && parms[1] == "SendEmail")
           {
               values["controller"] = "Home";
               values["action"] = "SendEmail";
               return true;
           }


           if (parms.Count() == 3 && parms[0] == "Rental" && parms[1] == "CheckUserRegistration")
           {
               values["controller"] = "Rental";
               values["action"] = "CheckUserRegistration";
               return true;
           }


           if (parms.Count() == 3 && parms[1] == "Rental" && parms[2] == "Checkout")
           {
               values["controller"] = "Rental";
               values["action"] = "Checkout";
               values["customRoute"] = parms[0] + "/Rental/Checkout";
               return true;
           }

           if (parms.Count() == 3 && parms[1] == "Rental" && parms[2] == "MakeBookingFinal")
           {
               values["controller"] = "Rental";
               values["action"] = "MakeBookingFinal";
               values["customRoute"] = parms[0] + "/Rental/MakeBookingFinal";
               return true;
           }
           if (parms.Count() == 3 && parms[1] == "Rental" && parms[2] == "Index")
           {
               values["controller"] = "Rental";
               values["action"] = "Index";
               values["customRoute"] = parms[0] + "/Rental/Index";
               return true;
           }
            if (parms.Count()== 1)
            {
                values["controller"] = "Rental";
                values["action"] = "Index";
                    return true;
            }
            if (parms.Count() == 3 && parms[1] == "Rental" && parms[2] == "MoveToIndex")
           {
               values["controller"] = "Rental";
               values["action"] = "Index";
               values["customRoute"] = parms[0] ;
               return true;
           }
            if (parms.Count() == 3 && parms[1] == "Rental")
            {
                values["controller"] = "Rental";
                values["action"] = parms[2];
                return true;
            }
            values["controller"] = "ErrorHandler";
            values["action"] = "Index"; 
            return true;
        }
    }
}