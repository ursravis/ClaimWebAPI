using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimDataAccess
{
    public class ClaimRepository : IClaimRepository
    {
        private ClaimDatabaseEntities businessEntities;
        public ClaimRepository()
        {
            this.businessEntities = new ClaimDatabaseEntities();
        }
        public IEnumerable<ClaimStatusType> GetAllClaims()
        {
            var claims = businessEntities.ClaimStatusTypes;
            return claims;
        }


        public bool CreateClaim(Claim claim)
        {
            businessEntities.Claims.Add(claim);
            if (businessEntities.SaveChanges() > 0)
                return true;
            else return false;
        }

        public Claim GetClaimById(int claimId)
        {
            var claim = businessEntities.Claims.Include("CauseType").Include("ClaimStatusType").Include("ClaimVechicles.Vehicle").FirstOrDefault(it => it.ClaimId == claimId);
            return claim;
        }
        public Claim GetClaimByClaimNo(string claimNo)
        {
            var claim = businessEntities.Claims.Include("ClaimVechicles.Vehicle").FirstOrDefault(it => it.ClaimNumber == claimNo);
            return claim;
        }
        public IEnumerable<Claim> GetClaimByLossDate(DateTime minDate, DateTime maxDate)
        {
            var claim = businessEntities.Claims.Include("ClaimVechicles.Vehicle").Where(it => it.LossDate >= minDate && it.LossDate <= maxDate);
            return claim.ToList();

        }

        public bool UpdateClaim(Claim ChangedClaim)
        {
            var claim = businessEntities.Claims.Include("ClaimVechicles.Vehicle").FirstOrDefault(it => it.ClaimNumber == ChangedClaim.ClaimNumber);
            ChangedClaim.ClaimId = claim.ClaimId;
            businessEntities.Claims.Attach(ChangedClaim);
              return businessEntities.SaveChanges() > 0;
           
        }

        public Vehicle GetVehicleOfClaim(string vin, string claimNo)
        {
            var claim = businessEntities.Claims.Include("ClaimVechicles.Vehicle").FirstOrDefault(it => it.ClaimNumber == claimNo);
            if (claim != null && claim.ClaimVechicles != null && claim.ClaimVechicles.Count > 0)
            {
                var vehicle = claim.ClaimVechicles.FirstOrDefault(it => it.Vehicle.Vin == vin);
                if (vehicle != null && vehicle.Vehicle != null)
                {
                    return vehicle.Vehicle;
                }
                else
                {
                    return null;
                }
            }
            else
            { return null; }
        }

        public bool DeleteClaim(string claimNo)
        {
              var claim = businessEntities.Claims.Include("ClaimVechicles.Vehicle").FirstOrDefault(it => it.ClaimNumber == claimNo);
              if (claim != null )
              {
                  businessEntities.Claims.Remove(claim);
                  if (claim.ClaimVechicles != null && claim.ClaimVechicles.Count>0)
                  {
                      foreach(var vehicle in claim.ClaimVechicles)
                      {
                          if(vehicle.Vehicle != null)
                          {
                              businessEntities.Vehicles.Remove(vehicle.Vehicle);
                          }
                      }
                  }
                  return businessEntities.SaveChanges() > 0;
              }
              return false;

        }
    }
}
