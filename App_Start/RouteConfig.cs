using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace GoogFleet
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
            name: "Appgetservicepart",
            url: "App/getservicepart",
            defaults: new { controller = "App", action = "getservicepart" }
           );

            routes.MapRoute(
             name: "AppAddservicepart",
             url: "App/Addservicepart/{servicepartname}",
             defaults: new { controller = "App", action = "Addservicepart", servicepartname = UrlParameter.Optional }
            );

            routes.MapRoute(
             name: "AppVehicleGroup",
             url: "App/VehicleGroup/{fleetcompanyid}",
             defaults: new { controller = "App", action = "VehicleGroup", fleetcompanyid = UrlParameter.Optional }
            );
            routes.MapRoute(
             name: "AppVehicleType",
             url: "App/VehicleType/{fleetcompanyid}",
             defaults: new { controller = "App", action = "VehicleType", fleetcompanyid = UrlParameter.Optional }
            );
            routes.MapRoute(
            name: "AppAutoBrand",
            url: "App/AutoBrand/{fleetcompanyid}",
            defaults: new { controller = "App", action = "AutoBrand", fleetcompanyid = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "AppAddVehicleGroup",
            url: "App/AddVehicleGroup/{FleetCompanyID}/{VehicleGroup}",
            defaults: new { controller = "App", action = "AddVehicleGroup", FleetCompanyID = UrlParameter.Optional, VehicleGroup = UrlParameter.Optional }
           );
            routes.MapRoute(
             name: "AppAddVehicleType",
             url: "App/AddVehicleType/{fleetcompanyid}/{VehicleType}",
             defaults: new { controller = "App", action = "AddVehicleType", fleetcompanyid = UrlParameter.Optional, VehicleType = UrlParameter.Optional }
            );
            routes.MapRoute(
            name: "AppAddAutoBrand",
            url: "App/AddAutoBrand/{fleetcompanyid}/{Make}/{Model}",
            defaults: new { controller = "App", action = "AddAutoBrand", fleetcompanyid = UrlParameter.Optional, Make = UrlParameter.Optional, Model = UrlParameter.Optional }
            );



            routes.MapRoute(
             name: "BillDetail",
             url: "BillDetail/{billid}",
             defaults: new { controller = "BillDetails", action = "Index", billid = UrlParameter.Optional }
            );

    

            routes.MapRoute(
              name: "UserVehicleAsso1",
              url: "UserVehicleAsso1/{userid}",
              defaults: new { controller = "User", action = "UserVehicleAsso1", userid = UrlParameter.Optional }
             );

            routes.MapRoute(
              name: "VehicleUserAllocation",
              url: "VehicleUserAllocation/{vehicleid}/{message}",
              defaults: new { controller = "Vehicle", action = "VehicleUserAllocation", vehicleid = UrlParameter.Optional, message = UrlParameter.Optional }
             );


            routes.MapRoute(
              name: "VehicleRemoveAllocation",
              url: "VehicleRemoveAllocation",
              defaults: new { controller = "Vehicle", action = "VehicleRemoveAllocation" }
             );



            routes.MapRoute(
              name: "VehicleDetailsEdit",
              url: "Vehicle/VehicleDetailsEdit",
              defaults: new { controller = "Vehicle", action = "Edit" }
             );

            routes.MapRoute(
             name: "VehicleCreate",
             url: "Vehicle/Create",
             defaults: new { controller = "Vehicle", action = "Create" }
            );





            routes.MapRoute(
              name: "VehicleTrafficContraventions",
              url: "VehicleTrafficContraventions",
              defaults: new { controller = "Vehicle", action = "VehicleTrafficContraventions" }
             );
            routes.MapRoute(
              name: "VehicleDetails",
              url: "VehicleDetails/{vehicleid}/{message}",
              defaults: new { controller = "Vehicle", action = "VehicleDetails", vehicleid = UrlParameter.Optional, message = UrlParameter.Optional }
             );

            routes.MapRoute(
                name: "Help",
                url: "Help",
                defaults: new { controller = "Home", action = "Help" }
               );

            routes.MapRoute(
             name: "Userx",
             url: "Userx/Createx",
             defaults: new { controller = "User", action = "Createx" }
            );

            routes.MapRoute(
             name: "User",
             url: "User/{user}",
             defaults: new { controller = "User", action = "Index", user = UrlParameter.Optional }
            );

            routes.MapRoute(
             name: "Vehicle",
             url: "Vehicle/{which}",
             defaults: new { controller = "Vehicle", action = "Index", which = UrlParameter.Optional }
            );
            routes.MapRoute(
             name: "Login",
             url: "Login",
             defaults: new { controller = "Home", action = "Login", id = UrlParameter.Optional }
            );
            
           routes.MapRoute(
             name: "MyAccount",
             url: "MyAccount",
             defaults: new { controller = "Home", action = "MyAccount" }
            );
            routes.MapRoute(
             name: "Logout",
             url: "Logout",
             defaults: new { controller = "Home", action = "Logout", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
