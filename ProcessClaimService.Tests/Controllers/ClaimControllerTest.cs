using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProcessClaimService;
using ProcessClaimService.Controllers;
using Moq;
using ClaimDataAccess;
using ClaimBusinessServices;
using System.Web.Http.Results;

namespace ProcessClaimService.Tests.Controllers
{
    [TestClass]
    public class ClaimControllerTest
    {
        [TestMethod]
        public void CreateMethodSetsLocationHeader()
        {
            // Arrange
            var mockRepository = new Mock<IClaimRepository>();

            var service = new ClaimService(mockRepository.Object);
            var controller = new ClaimController(service);

            // Act
            IHttpActionResult actionResult = controller.CreateClaim(new ClaimBusinessEntities.Claim
            {
                ClaimNumber = "testClaimNo",
                ClaimantFirstName = "test",
                Vehicles = new List<ClaimBusinessEntities.VehicleDetails>() { new ClaimBusinessEntities.VehicleDetails() { VehicleId = 2, Vin = "100" } }

            });
            var createdResult = actionResult as CreatedAtRouteNegotiatedContentResult<ClaimBusinessEntities.Claim>;

            // Assert
            Assert.IsNotNull(createdResult);
            Assert.AreEqual("GetByClaimNo", createdResult.RouteName);
            Assert.AreEqual("testClaimNo", createdResult.RouteValues["claimNo"]);
        }
        [TestMethod]
        public void CreateMethodReturnsBadRequest()
        {
            // Arrange
            var mockRepository = new Mock<IClaimRepository>();

            var service = new ClaimService(mockRepository.Object);
            var controller = new ClaimController(service);

            // Act
            IHttpActionResult actionResult = controller.CreateClaim(null);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }
        [TestMethod]
        public void GetReturnsClaimWithSameId()
        {
            // Arrange
            var mockRepository = new Mock<IClaimRepository>();
            mockRepository.Setup(x => x.GetClaimById(42))
                .Returns(new Claim
                {
                    ClaimId = 42,
                    ClaimNumber = "testing",
                    ClaimantFirstName = "test",
                    ClaimVechicles = new List<ClaimVechicle>()
                        {
                           new ClaimVechicle(){Vehicle = new Vehicle() { VehicleId=2,Vin="100"}}
                        }
                });
            var service = new ClaimService(mockRepository.Object);
            var controller = new ClaimController(service);

            // Act
            IHttpActionResult actionResult = controller.GetClaimById(42);
            var contentResult = actionResult as OkNegotiatedContentResult<ClaimBusinessEntities.Claim>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(42, contentResult.Content.ClaimId);
            Assert.AreEqual("100", contentResult.Content.Vehicles.FirstOrDefault().Vin);
        }
        [TestMethod]
        public void GetReturnsNotFound()
        {
            // Arrange
            var mockRepository = new Mock<IClaimRepository>();
            var service = new ClaimService(mockRepository.Object);
            var controller = new ClaimController(service);

            // Act
            IHttpActionResult actionResult = controller.GetClaimByClaimNo("test");

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }
        [TestMethod]
        public void DeleteReturnsNotFound()
        {
            // Arrange
            var mockRepository = new Mock<IClaimRepository>();

            var service = new ClaimService(mockRepository.Object);
            var controller = new ClaimController(service);

            // Act
            IHttpActionResult actionResult = controller.DeleteClaim("testing");

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }
    }
}
