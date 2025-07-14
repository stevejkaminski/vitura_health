using VituraHealth.PrescriptionManagement.Domain.Models;
using VituraHealth.PrescriptionManagement.Server.Interfaces;
using VituraHealth.PrescriptionManagement.Infrastructure.DataAccess;

namespace VituraHealth.PrescriptionManagement.Server.Service
{
    /// <summary>
    /// Provides functionality for managing prescriptions and retrieving patient and prescription data.
    /// </summary>
    /// <remarks>This service acts as a layer between the application and the underlying data access
    /// implementation. It allows retrieval of patients and prescriptions, as well as adding new
    /// prescriptions.</remarks>
    public class PrescriptionService : IPrescriptionService
    {
        private readonly IPrescriptionDataAccessor _prescriptionDataAccessor;
        public PrescriptionService(IPrescriptionDataAccessor prescriptionDataAccessor)
        {
            _prescriptionDataAccessor = prescriptionDataAccessor;
        }
        public IEnumerable<Patient> GetPatients() => _prescriptionDataAccessor.GetPatients();

        public IEnumerable<Prescription> GetPrescriptions() => _prescriptionDataAccessor.GetPrescriptions();

        public Prescription AddPrescription(Prescription prescription) => _prescriptionDataAccessor.AddPrescription(prescription);
    }
}
