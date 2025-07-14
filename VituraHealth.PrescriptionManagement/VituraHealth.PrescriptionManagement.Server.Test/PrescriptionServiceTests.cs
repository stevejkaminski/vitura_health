using Moq;
using VituraHealth.PrescriptionManagement.Domain.Models;
using VituraHealth.PrescriptionManagement.Server.Service;
using VituraHealth.PrescriptionManagement.Infrastructure.DataAccess;

namespace VituraHealth.PrescriptionManagement.Server.Test
{
    public class PrescriptionServiceTests
    {
        [Fact]
        public void GetPatients_DelegatesToDataAccessor_AndReturnsPatients()
        {
            // Arrange
            var expectedPatients = new List<Patient>
            {
                new Patient { Id = 1, FullName = "Alice" },
                new Patient { Id = 2, FullName = "Bob" }
            };
            var mockDataAccessor = new Mock<IPrescriptionDataAccessor>();
            mockDataAccessor.Setup(d => d.GetPatients()).Returns(expectedPatients);

            var service = new PrescriptionService(mockDataAccessor.Object);

            // Act
            var result = service.GetPatients();

            // Assert
            Assert.Equal(expectedPatients, result);
            mockDataAccessor.Verify(d => d.GetPatients(), Times.Once);
        }

        [Fact]
        public void GetPrescriptions_DelegatesToDataAccessor_AndReturnsPrescriptions()
        {
            // Arrange
            var expectedPrescriptions = new List<Prescription>
            {
                new Prescription { Id = 1, PatientId = 1, DrugName = "A", Dosage = "10mg" },
                new Prescription { Id = 2, PatientId = 2, DrugName = "B", Dosage = "20mg" }
            };
            var mockDataAccessor = new Mock<IPrescriptionDataAccessor>();
            mockDataAccessor.Setup(d => d.GetPrescriptions()).Returns(expectedPrescriptions);

            var service = new PrescriptionService(mockDataAccessor.Object);

            // Act
            var result = service.GetPrescriptions();

            // Assert
            Assert.Equal(expectedPrescriptions, result);
            mockDataAccessor.Verify(d => d.GetPrescriptions(), Times.Once);
        }

        [Fact]
        public void AddPrescription_DelegatesToDataAccessor_AndReturnsResult()
        {
            // Arrange
            var input = new Prescription { PatientId = 1, DrugName = "TestDrug", Dosage = "10mg" };
            var expected = new Prescription { Id = 42, PatientId = 1, DrugName = "TestDrug", Dosage = "10mg" };
            var mockDataAccessor = new Mock<IPrescriptionDataAccessor>();
            mockDataAccessor.Setup(d => d.AddPrescription(input)).Returns(expected);

            var service = new PrescriptionService(mockDataAccessor.Object);

            // Act
            var result = service.AddPrescription(input);

            // Assert
            Assert.Equal(expected, result);
            mockDataAccessor.Verify(d => d.AddPrescription(input), Times.Once);
        }
    }
}