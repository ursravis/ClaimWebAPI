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
  [XmlRoot("MitchellClaim", Namespace = "http://www.mitchell.com/examples/claim")]
    public class Claim
    {

        public int ClaimId { get; set; }
        [Required]
        public string ClaimNumber { get; set; }
   
        public string ClaimantFirstName { get; set; }

        public string ClaimantLastName { get; set; }

        [ValidEnumValue]
        public Status? Status { get; set; }
    
        public DateTime? LossDate { get; set; }
    
        public LossInfo LossInfo { get; set; }
    
        public Nullable<long> AssignedAdjusterID { get; set; }

        public List<VehicleDetails> Vehicles { get; set; }

    }
    public enum Status
    {
        OPEN=1,
        CLOSED=2
    }
 
}
