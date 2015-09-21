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
           if (parms.Count() == 2 && parms[0] == "Home" && parms[1] == "CompanyURLAvailability")
           {
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
               values["customRoute"] = parms[0] + "/Rental/Checkout";
               return true;
           }


           if (parms.Count() == 3 && parms[1] == "Rental" && parms[2] == "Checkout")
           {
               values["controller"] = "Rental";
               values["action"] = "Checkout";
               values["customRoute"] = parms[0] + "/Rental/Checkout";
               return true;
           }

           if (parms.Count() == 3 && parms[1] == "Rental" && parms[2] == "BookCar")
           {
               values["controller"] = "Rental";
               values["action"] = "BookCar";
               values["customRoute"] = parms[0] + "/Rental/BookCar";
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