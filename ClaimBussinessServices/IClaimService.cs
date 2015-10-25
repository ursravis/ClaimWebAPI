using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimBusinessServices
{
    public interface IClaimService
	{
        IEnumerable<string> GetAllClaims();
        bool CreateClaim(ClaimBusinessEntities.Claim claim);
        ClaimBusinessEntities.Claim GetClaimById(int claimId);
        ClaimBusinessEntities.Claim GetClaimByClaimNo(string claimNo);
        IEnumerable<ClaimBusinessEntities.Claim> GetClaimByLossDate(DateTime minDate, DateTime maxDate);
        bool UpdateClaim(ClaimBusinessEntities.Claim claim);
        ClaimBusinessEntities.VehicleDetails GetVehicleOfClaim(string vin , string claimNo);
        bool DeleteClaim(string claimNo);
    }
}
