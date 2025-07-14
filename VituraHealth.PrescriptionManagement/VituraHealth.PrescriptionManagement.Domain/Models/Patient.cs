namespace VituraHealth.PrescriptionManagement.Domain.Models
{
    /// <summary>
    /// Patient domain model representing a patient in the system.
    /// </summary>
    public class Patient
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
    }
}
