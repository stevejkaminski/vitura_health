using Microsoft.AspNetCore.Mvc;
using Moq;
using VituraHealth.PrescriptionManagement.Server.Controllers;
using VituraHealth.PrescriptionManagement.Server.Interfaces;
using VituraHealth.PrescriptionManagement.Domain.Models;
using Xunit;
using System.Collections.Generic;

namespace VituraHealth.PrescriptionManagement.Server.Test
{
    public class PatientsControllerTests
    {
        [Fact]
        public void GetPatients_ReturnsOkWithPatients()
        {
            // Arrange
            var expectedPatients = new List<Patient>
            {
                new Patient { Id = 1, FullName = "Alice" },
                new Patient { Id = 2, FullName = "Bob" }
            };
            var mockService = new Mock<IPrescriptionService>();
            mockService.Setup(s => s.GetPatients()).Returns(expectedPatients);

            var controller = new PatientsController(mockService.Object);

            // Act
            var result = controller.GetPatients();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expectedPatients, okResult.Value);
            mockService.Verify(s => s.GetPatients(), Times.Once);
        }
    }
}