using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoogFleet.Models
{
    public class fleetbox
    {
    }
    public class Vehicles
    {
        public int VehicleID { get; set; }
        public string VehicleNoName { get; set; }
        public int VehicleNo { get; set; }
        public int VehicleGroupID { get; set; }
        public string VehicleGroup { get; set; }
        public int VehicleTypeID { get; set; }
        public string VehicleType { get; set; }
        public string RegistrationNo { get; set; }
        public int AutoBrandID { get; set; }
        public string Brand { get; set; }
        public string GPSURL { get; set; }
        public DateTime YearofManufacture { get; set; }
        public string Note { get; set; }
    }

    public class VehicleInsurance {

        public int VehicleInsuranceID { get; set; }
        public int? VehicleID { get; set; }
        public int? InsuranceID { get; set; }
        public string Insurance { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

    public class VehicleUser
    {
        public int VehicleUserID { get; set; }
        public string UserType { get; set; }
        public string UserName { get; set; }
        public int? UserID { get; set; }
        public string EmailID { get; set; }
        public string NationalID { get; set; }
        public string MobileNo { get; set; }
        public string Address { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Note { get; set; }
    }

    public class VehicleTraffic
    {
        public DateTime? TrafficContraventionsDate { get; set; }
        public string Description { get; set; }
        public string UserName { get; set; }
    }

    public class BillDetailed {

        public int? BillID { get; set; }
        public string ServiceParts { get; set; }
        public string Description { get; set; }
        public int? Quantity { get; set; }
        public decimal? Amount { get; set; }

    }

}