namespace VituraHealth.PrescriptionManagement.Domain.Models
{
    /// <summary>
    /// Preascription domain model representing a patient's prescription.
    /// </summary>
    public class Prescription
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string DrugName { get; set; } = string.Empty;
        public string Dosage { get; set; } = string.Empty;
        public DateTime DatePrescribed { get; set; }
    }
}
