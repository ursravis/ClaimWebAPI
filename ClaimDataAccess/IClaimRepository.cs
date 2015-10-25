using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimDataAccess
{
    public interface IClaimRepository
    {
        IEnumerable<ClaimStatusType> GetAllClaims();
        bool CreateClaim(Claim claim);
        Claim GetClaimById(int claimId);
        Claim GetClaimByClaimNo(string claimNo);
        IEnumerable<Claim> GetClaimByLossDate(DateTime minDate, DateTime maxDate);
        bool UpdateClaim(Claim claim);
        Vehicle GetVehicleOfClaim(string vin, string claimNo);
        bool DeleteClaim(string claimId);


    }
}
