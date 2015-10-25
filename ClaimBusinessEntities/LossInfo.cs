using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ClaimBusinessEntities
{
    public class LossInfo
    {
        [ValidEnumValue]
        public CauseOfLoss? CauseOfLoss { get; set; }

        public Nullable<System.DateTime> ReportedDate { get; set; }

        public string LossDescription { get; set; }
    }
    public enum CauseOfLoss
    {
        Collision = 1,
        Explosion = 2,
        Fire = 3,
        Hail = 4,        
        MechanicalBreakdown = 5,
        Other = 6
    }

}
