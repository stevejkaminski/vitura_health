using VituraHealth.PrescriptionManagement.Domain.Models;

namespace VituraHealth.PrescriptionManagement.Infrastructure.DataAccess
{
    /// <summary>
    /// Generic Data Accessor interface for Prescription Management
    /// </summary>
    public interface IPrescriptionDataAccessor
    {
        public IEnumerable<Patient> GetPatients();
        public IEnumerable<Prescription> GetPrescriptions();
        public Prescription AddPrescription(Prescription prescription);
    }
}
