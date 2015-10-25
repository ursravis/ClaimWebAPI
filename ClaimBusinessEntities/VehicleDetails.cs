
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ClaimBusinessEntities
{
    public class VehicleDetails
    {

        public int VehicleId { get; set; }
        [Required]
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
      
    }
}
