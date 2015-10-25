using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ClaimBusinessServices;
using ClaimDataAccess;
using System.Text;

namespace ProcessClaimService.Controllers
{
    /// <summary>
    /// TO manage claim information.
    /// </summary>
    [RoutePrefix("Claims")]
    public class ClaimController : ApiController
    {

        IClaimService claimService;
        /// <summary>
        /// Initializes a new instance of the <see cref="ClaimController"/> class.
        /// </summary>
        /// <param name="service">The service.</param>
        public ClaimController(IClaimService service)
        {
            this.claimService = service;
        }
        /// <summary>
        /// Creates the claim.
        /// </summary>
        /// <param name="claim">The claim.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Claim")]
        public IHttpActionResult CreateClaim(ClaimBusinessEntities.Claim claim)
        {
            if (claim != null && ModelState.IsValid)
            {
                claimService.CreateClaim(claim);

                return CreatedAtRoute("GetByClaimNo", new { claimNo = claim.ClaimNumber }, claim);
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Gets the claim by identifier.
        /// </summary>
        /// <param name="claimId">The claim identifier.</param>
        /// <returns></returns>
        [Route("Id/{claimId}")]
        public IHttpActionResult GetClaimById(int claimId)
        {
          
            var claim = claimService.GetClaimById(claimId);
            if (claim != null)
            {
                return Ok(claim);
            }
            else
            {
                return NotFound();
            }
        }
        /// <summary>
        /// Gets the claim by claim no.
        /// </summary>
        /// <param name="claimNo">The claim no.</param>
        /// <returns></returns>
        [Route("ClaimNo/{claimNo}",Name="GetByClaimNo")]
        public IHttpActionResult GetClaimByClaimNo(string claimNo)
        {
            var claim = claimService.GetClaimByClaimNo(claimNo);
            if (claim != null)
            {
                return Ok(claim);
            }
            else
            {
                return NotFound();
            }
        }
        /// <summary>
        /// Gets the claim by loss date.
        /// </summary>
        /// <param name="minDate">The minimum date.</param>
        /// <param name="maxDate">The maximum date.</param>
        /// <returns></returns>
        [Route("LossDate/{minDate}/{maxDate}")]
        public IHttpActionResult GetClaimByLossDate(DateTime minDate, DateTime maxDate)
        {
            var claims = claimService.GetClaimByLossDate(minDate, maxDate);
            if (claims != null)
            {
                return Ok(claims.ToList());
            }
            else
            {
                return NotFound();
            }
        }
        /// <summary>
        /// Gets the vehicle of claim.
        /// </summary>
        /// <param name="vin">The vin.</param>
        /// <param name="claimNo">The claim no.</param>
        /// <returns></returns>
        [Route("Claim/{claimNo}/Vehicle/{vin}")]
        public IHttpActionResult GetVehicleOfClaim(string vin, string claimNo)
        {
            var vehicle = claimService.GetVehicleOfClaim(vin, claimNo);
            if (vehicle != null)
            {
                return Ok(vehicle);
            }
            else
            {
                return NotFound();
            }
        }


        /// <summary>
        /// Deletes the claim.
        /// </summary>
        /// <param name="claimNo">The claim no.</param>
        /// <returns></returns>
        [Route("Claim")]
        [HttpDelete]
        public IHttpActionResult DeleteClaim(string claimNo)
        {
            if (claimService.DeleteClaim(claimNo))
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
        //Update is not implemented because of gap in the requirement. Requirements says to update only  fields witch are passed in the XML. However given XML does not contain any primary Keys.
        //Shall we consider Vin is the primary key to update vehicle info? But in XSD doc, Vin is not a mandatary to create or update.
        //[Route("Claim")]
        //[HttpPatch]
        //public IHttpActionResult UpdateClaim(ClaimBusinessEntities.Claim claim)
        //{
        //    if (claimService.UpdateClaim(claim))
        //    {
        //        return Ok();
        //    }
        //    else
        //    {
        //        return NotFound();
        //    }
        //}
    }
}
