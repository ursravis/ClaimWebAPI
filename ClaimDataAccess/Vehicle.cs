//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClaimDataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class Vehicle
    {
        public Vehicle()
        {
            this.ClaimVechicles = new HashSet<ClaimVechicle>();
        }
    
        public int VehicleId { get; set; }
        public int ModelYear { get; set; }
        public string MakeDescription { get; set; }
        public string ModelDescription { get; set; }
        public string EngineDescription { get; set; }
        public string ExteriorColor { get; set; }
        public string Vin { get; set; }
        public string LicPlate { get; set; }
        public string LicPlateState { get; set; }
        public Nullable<System.DateTime> LicPlateExpDate { get; set; }
        public string DamageDescription { get; set; }
        public Nullable<int> Mileage { get; set; }
    
        public virtual ICollection<ClaimVechicle> ClaimVechicles { get; set; }
    }
}