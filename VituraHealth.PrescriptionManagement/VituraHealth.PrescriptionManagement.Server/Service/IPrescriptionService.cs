using VituraHealth.PrescriptionManagement.Domain.Models;

namespace VituraHealth.PrescriptionManagement.Server.Interfaces
{
    public interface IPrescriptionService
    {
        IEnumerable<Patient> GetPatients();
        IEnumerable<Prescription> GetPrescriptions();
        Prescription AddPrescription(Prescription prescription);
    }
}
