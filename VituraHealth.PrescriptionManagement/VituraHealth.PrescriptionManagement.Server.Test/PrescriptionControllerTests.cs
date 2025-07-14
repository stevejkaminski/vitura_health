using Microsoft.AspNetCore.Mvc;
using Moq;
using VituraHealth.PrescriptionManagement.Domain.Models;
using VituraHealth.PrescriptionManagement.Server.Controllers;
using VituraHealth.PrescriptionManagement.Server.Interfaces;

namespace VituraHealth.PrescriptionManagement.Server.Test
{
    public class PrescriptionControllerTests
    {
        [Fact]
        public void AddPrescription_ReturnsBadRequest_WhenRequiredFieldsMissing()
        {
            var mockService = new Mock<IPrescriptionService>();
            var controller = new PrescriptionsController(mockService.Object);

            var invalidPrescription = new Prescription { PatientId = 0, DrugName = "", Dosage = "" };

            var result = controller.AddPrescription(invalidPrescription);

            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Patient, drug, and dosage are required.", badRequest.Value);
        }

        [Fact]
        public void AddPrescription_ReturnsCreatedAtAction_WhenValid()
        {
            var mockService = new Mock<IPrescriptionService>();
            var input = new Prescription { PatientId = 1, DrugName = "Aspirin", Dosage = "100mg" };
            var created = new Prescription { Id = 42, PatientId = 1, DrugName = "Aspirin", Dosage = "100mg" };

            mockService.Setup(s => s.AddPrescription(input)).Returns(created);

            var controller = new PrescriptionsController(mockService.Object);

            var result = controller.AddPrescription(input);

            var createdAt = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(nameof(controller.GetPrescriptions), createdAt.ActionName);
            Assert.Equal(created, createdAt.Value);
            Assert.Equal(42, ((Prescription)createdAt.Value).Id);
        }
    }
}