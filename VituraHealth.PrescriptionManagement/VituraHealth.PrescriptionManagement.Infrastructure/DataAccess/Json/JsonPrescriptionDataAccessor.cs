using Microsoft.Extensions.Configuration;
using System.Text.Json;
using VituraHealth.PrescriptionManagement.Domain.Models;
using VituraHealth.PrescriptionManagement.Infrastructure.Helpers;
using VituraHealth.PrescriptionManagement.Infrastructure.Settings;

namespace VituraHealth.PrescriptionManagement.Infrastructure.DataAccess.Json
{
    /// <summary>
    /// Provides methods for accessing and managing prescription and patient data stored in JSON files.
    /// </summary>
    /// <remarks>This class implements the <see cref="IPrescriptionDataAccessor"/> interface to provide
    /// functionality for retrieving patient and prescription data, as well as adding new prescriptions. Data is
    /// persisted in JSON files, and changes are automatically saved when prescriptions are added.</remarks>
    public class JsonPrescriptionDataAccessor : IPrescriptionDataAccessor
    {
        private List<Patient> _patients;
        private List<Prescription> _prescriptions;
        private int _nextPrescriptionId = 1;
        private bool _persistChanges = false;
        private string _patientsFilePath;
        private string _prescriptionsFilePath;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="persistChanges"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public JsonPrescriptionDataAccessor(JsonDataStoreConfig jsonDataStoreConfig)
        {
            if( string.IsNullOrEmpty(jsonDataStoreConfig.BasePath) || 
                string.IsNullOrEmpty(jsonDataStoreConfig.PatientsFileName) ||
                string.IsNullOrEmpty(jsonDataStoreConfig.PrescriptionsFileName))
            {
                throw new ArgumentNullException("Json file paths are misconfigured! Check your appsettings.json and be sure that the paths to your patients and prescription json files are correct. Then run again ...");
            }
            _patientsFilePath = Path.Combine($"{Directory.GetCurrentDirectory()}\\{jsonDataStoreConfig.BasePath}\\", jsonDataStoreConfig.PatientsFileName);
            _prescriptionsFilePath = Path.Combine($"{Directory.GetCurrentDirectory()}\\{jsonDataStoreConfig.BasePath}\\", jsonDataStoreConfig.PrescriptionsFileName);
            _persistChanges = jsonDataStoreConfig.PersistChanges;

            LoadData();
            if (!_prescriptions.Any())
            {
                throw new InvalidOperationException($"No prescriptions found. Please add a prescription to {_prescriptionsFilePath} file before proceeding.");
            }
            if (!_patients.Any())
            {
                throw new InvalidOperationException($"No patients found. Please add a patient to {_patientsFilePath} file  before proceeding.");
            }
            // Initialize the next prescription ID based on existing prescriptions
            _nextPrescriptionId = _prescriptions.Any() ? _prescriptions.Max(p => p.Id) + 1 : 1;
        }
        public IEnumerable<Patient> GetPatients() => _patients;
        public IEnumerable<Prescription> GetPrescriptions() => _prescriptions;
        public Prescription AddPrescription(Prescription prescription)
        {
            if (prescription == null || prescription.PatientId <= 0 || string.IsNullOrWhiteSpace(prescription.DrugName) || string.IsNullOrWhiteSpace(prescription.Dosage))
            {
                throw new ArgumentException("Patient, drug, and dosage are required.");
            }
            prescription.Id = _nextPrescriptionId++;
            prescription.DatePrescribed = DateTime.UtcNow;
            _prescriptions.Add(prescription);

            if (_persistChanges)
            {   
                JsonFileHelper.WriteToJsonFile(_prescriptions, _prescriptionsFilePath);
            }
            return prescription;
        }
        private void LoadData()
        {
            try
            {
                _patients = JsonFileHelper.ReadFromJsonFile<Patient>(_patientsFilePath) ?? new List<Patient>();
                _prescriptions = JsonFileHelper.ReadFromJsonFile<Prescription>(_prescriptionsFilePath) ?? new List<Prescription>();
            }
            catch (FileNotFoundException ex)
            {
                // If the file does not exist, initialize empty lists
                _patients = new List<Patient>();
                _prescriptions = new List<Prescription>();
                Console.WriteLine($"File not found: {ex.Message}. Initialized empty data lists.");
            }
            catch (JsonException ex)
            {
                // Handle JSON deserialization errors
                Console.WriteLine($"Error reading JSON data: {ex.Message}. Initialized empty data lists.");
                _patients = new List<Patient>();
                _prescriptions = new List<Prescription>();
            }
        }
    }
}
