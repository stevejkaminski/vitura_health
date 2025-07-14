namespace VituraHealth.PrescriptionManagement.Infrastructure.Settings
{
    /// <summary>
    /// Represents the configuration settings for a JSON-based data store.
    /// </summary>
    /// <remarks>This class provides file paths for storing prescription and patient data, as well as a flag
    /// to  control whether changes to the data are persisted automatically.</remarks>
    public class JsonDataStoreConfig
    {
        public string BasePath { get; set; } = "Data"; // Base path for data files 
        public string PrescriptionsFileName {get; set; } = "prescriptions.json";
        public string PatientsFileName { get; set; } = "patients.json";
        public bool PersistChanges { get; set; } = false;
        
    }
}
