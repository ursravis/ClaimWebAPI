using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClaimDataAccess;

namespace ClaimBusinessServices
{
    public class ClaimService : IClaimService
    {
        IClaimRepository repository;
        public ClaimService(IClaimRepository repository)
        {
            this.repository = repository;
        }
        public IEnumerable<string> GetAllClaims()
        {
            var claims = repository.GetAllClaims();
            if (claims.Any())
            {
                return claims.Select(it => it.StatusType);
            }
            return null;
        }
        public bool CreateClaim(ClaimBusinessEntities.Claim claim)
        {
            bool isSuccess = false;

            var dbClaim = Mapper.ConverBusinessToDBEntity(claim);
            isSuccess = repository.CreateClaim(dbClaim);
            if(isSuccess)
            {
                claim.ClaimId = dbClaim.ClaimId;
            }

            return isSuccess;
        }


        public ClaimBusinessEntities.Claim GetClaimById(int claimId)
        {
            var claim = repository.GetClaimById(claimId);
            if (claim != null)
            {
                return Mapper.ConverDBToBusinessEntity(claim);
            }
            return null;
        }

        public ClaimBusinessEntities.Claim GetClaimByClaimNo(string claimNo)
        {
            var claim = repository.GetClaimByClaimNo(claimNo);
            if (claim != null)
            {
                return Mapper.ConverDBToBusinessEntity(claim);
            }
            return null;
        }

        public IEnumerable<ClaimBusinessEntities.Claim> GetClaimByLossDate(DateTime minDate, DateTime maxDate)
        {
            var claims = repository.GetClaimByLossDate(minDate, maxDate);
            if (claims != null && claims.Any())
            {
                List<ClaimBusinessEntities.Claim> claimList = new List<ClaimBusinessEntities.Claim>();
                claims.ToList().ForEach(it => claimList.Add(Mapper.ConverDBToBusinessEntity(it)));
                return claimList;
            }
            return null;
        }
        public ClaimBusinessEntities.VehicleDetails GetVehicleOfClaim(string vin, string claimNo)
        {
            var vehicle = repository.GetVehicleOfClaim(vin, claimNo);
            if (vehicle != null)
            {
                return Mapper.ConverDBToBusinessEntity(vehicle);
            }
            return null;
        }
        public bool UpdateClaim(ClaimBusinessEntities.Claim claim)
        {
            bool isSuccess = false;

            var dbClaim = Mapper.ConverBusinessToDBEntity(claim);
            isSuccess = repository.UpdateClaim(dbClaim);

            return isSuccess;
        }



        public bool DeleteClaim(string claimNo)
        {
            return repository.DeleteClaim(claimNo);

        }

    }
}
