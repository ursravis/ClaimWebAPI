using ClaimDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimBusinessServices
{
    public static class Mapper
    {
        public static ClaimBusinessEntities.Claim ConverDBToBusinessEntity(Claim dbClaim)
        {
            ClaimBusinessEntities.Claim claim = new ClaimBusinessEntities.Claim()
            {
                AssignedAdjusterID = dbClaim.AssignedAdjusterID,
                ClaimantFirstName = dbClaim.ClaimantFirstName,
                ClaimantLastName = dbClaim.ClaimantLastName,
                ClaimId = dbClaim.ClaimId,
                ClaimNumber = dbClaim.ClaimNumber,
                LossDate = dbClaim.LossDate,


                LossInfo = new ClaimBusinessEntities.LossInfo()
                {
                   
                    LossDescription = dbClaim.LossDescription,
                    ReportedDate = dbClaim.ReportedDate
                }
               
            };
            if (dbClaim.CauseType != null && dbClaim.CauseType.CauseTypeCode != null)
                claim.LossInfo.CauseOfLoss = ParseEnum<ClaimBusinessEntities.CauseOfLoss>(dbClaim.CauseType.CauseTypeCode);
            if (dbClaim.ClaimStatusType != null)
            {
                claim.Status = ParseEnum<ClaimBusinessEntities.Status>(dbClaim.ClaimStatusType.StatusType);
            }
            if (dbClaim.ClaimVechicles != null && dbClaim.ClaimVechicles.Count > 0)
            {
                claim.Vehicles = new List<ClaimBusinessEntities.VehicleDetails>();
                foreach (var vehicle in dbClaim.ClaimVechicles)
                {
                    if (vehicle.Vehicle != null)
                    {
                        claim.Vehicles.Add(ConverDBToBusinessEntity(vehicle.Vehicle));
                    }
                }
            }
            return claim;
        }
        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
        public static Claim ConverBusinessToDBEntity(ClaimBusinessEntities.Claim claim)
        {
            Claim dbClaim = new Claim()
                  {
                      AssignedAdjusterID = claim.AssignedAdjusterID,
                      ClaimantFirstName = claim.ClaimantFirstName,
                      ClaimantLastName = claim.ClaimantLastName,
                      ClaimNumber = claim.ClaimNumber,
                      LossDate = claim.LossDate,


                  };
            if (claim.LossInfo != null)
            {
                if (claim.LossInfo.CauseOfLoss != null)
                    dbClaim.CauseOfLoss = (int)claim.LossInfo.CauseOfLoss;
                dbClaim.LossDescription = claim.LossInfo.LossDescription;
                dbClaim.ReportedDate = claim.LossInfo.ReportedDate;
            }
            if (claim.Status != null)
            {
                dbClaim.ClaimStatus = (int)claim.Status;
            }
            if (claim.Vehicles != null && claim.Vehicles.Any())
            {
                List<Vehicle> vechicles = new List<Vehicle>();
                dbClaim.ClaimVechicles = new List<ClaimVechicle>();
                foreach (var vehicle in claim.Vehicles)
                {
                    dbClaim.ClaimVechicles.Add(new ClaimVechicle()
                    {
                        Vehicle = new Vehicle()
                            {
                                DamageDescription = vehicle.DamageDescription,
                                EngineDescription = vehicle.EngineDescription,
                                ExteriorColor = vehicle.ExteriorColor,
                                LicPlate = vehicle.LicPlate,
                                LicPlateExpDate = vehicle.LicPlateExpDate,
                                LicPlateState = vehicle.LicPlateState,
                                MakeDescription = vehicle.MakeDescription,
                                Mileage = vehicle.Mileage,
                                ModelDescription = vehicle.ModelDescription,
                                ModelYear = vehicle.ModelYear,
                                Vin = vehicle.Vin
                            }
                    });

                }

            }
            return dbClaim;
        }
        public static ClaimBusinessEntities.VehicleDetails ConverDBToBusinessEntity(Vehicle vehicle)
        {
            var vehdetails = new ClaimBusinessEntities.VehicleDetails()
            {
                VehicleId = vehicle.VehicleId,
                DamageDescription = vehicle.DamageDescription,
                EngineDescription = vehicle.EngineDescription,
                ExteriorColor = vehicle.ExteriorColor,
                LicPlate = vehicle.LicPlate,
                LicPlateExpDate = vehicle.LicPlateExpDate,
                LicPlateState = vehicle.LicPlateState,
                MakeDescription = vehicle.MakeDescription,
                Mileage = vehicle.Mileage,
                ModelDescription = vehicle.ModelDescription,
                ModelYear = vehicle.ModelYear,
                Vin = vehicle.Vin
            };
            return vehdetails;
        }
    }
}
