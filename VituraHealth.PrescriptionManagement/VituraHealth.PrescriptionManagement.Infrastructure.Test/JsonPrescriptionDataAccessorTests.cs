using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Moq;
using VituraHealth.PrescriptionManagement.Domain.Models;
using VituraHealth.PrescriptionManagement.Infrastructure.DataAccess.Json;
using VituraHealth.PrescriptionManagement.Infrastructure.Helpers;
using VituraHealth.PrescriptionManagement.Infrastructure.Settings;
using Xunit;

namespace VituraHealth.PrescriptionManagement.Infrastructure.Test
{
    public class JsonPrescriptionDataAccessorTests
    {
        private readonly string _testBasePath = "TestData";
        private readonly string _patientsFileName = "patients.json";
        private readonly string _prescriptionsFileName = "prescriptions.json";

        private JsonDataStoreConfig GetValidConfig(bool persistChanges = false)
        {
            return new JsonDataStoreConfig
            {
                BasePath = _testBasePath,
                PatientsFileName = _patientsFileName,
                PrescriptionsFileName = _prescriptionsFileName,
                PersistChanges = persistChanges
            };
        }

        private void CreateTestFiles(IEnumerable<Patient> patients, IEnumerable<Prescription> prescriptions)
        {
            Directory.CreateDirectory(_testBasePath);
            JsonFileHelper.WriteToJsonFile(patients.ToList(), Path.Combine(_testBasePath, _patientsFileName));
            JsonFileHelper.WriteToJsonFile(prescriptions.ToList(), Path.Combine(_testBasePath, _prescriptionsFileName));
        }

        private void DeleteTestFiles()
        {
            if (Directory.Exists(_testBasePath))
                Directory.Delete(_testBasePath, true);
        }

        [Fact]
        public void Constructor_ThrowsArgumentNullException_WhenConfigIsInvalid()
        {
            var config = new JsonDataStoreConfig
            {
                BasePath = "",
                PatientsFileName = "",
                PrescriptionsFileName = ""
            };
            Assert.Throws<ArgumentNullException>(() => new JsonPrescriptionDataAccessor(config));
        }

        [Fact]
        public void Constructor_ThrowsInvalidOperationException_WhenNoPatientsOrPrescriptions()
        {
            DeleteTestFiles();
            Directory.CreateDirectory(_testBasePath);
            File.WriteAllText(Path.Combine(_testBasePath, _patientsFileName), "[]");
            File.WriteAllText(Path.Combine(_testBasePath, _prescriptionsFileName), "[]");

            var config = GetValidConfig();
            Assert.Throws<InvalidOperationException>(() => new JsonPrescriptionDataAccessor(config));

            DeleteTestFiles();
        }

        [Fact]
        public void GetPatients_ReturnsPatients()
        {
            DeleteTestFiles();
            var patients = new List<Patient>
            {
                new Patient { Id = 1, FullName = "John Doe", DateOfBirth = new DateTime(1980, 1, 1) }
            };
            var prescriptions = new List<Prescription>
            {
                new Prescription { Id = 1, PatientId = 1, DrugName = "DrugA", Dosage = "10mg", DatePrescribed = DateTime.UtcNow }
            };
            CreateTestFiles(patients, prescriptions);

            var config = GetValidConfig();
            var accessor = new JsonPrescriptionDataAccessor(config);

            var result = accessor.GetPatients();
            Assert.Single(result);
            Assert.Equal("John Doe", result.First().FullName);

            DeleteTestFiles();
        }

        [Fact]
        public void GetPrescriptions_ReturnsPrescriptions()
        {
            DeleteTestFiles();
            var patients = new List<Patient>
            {
                new Patient { Id = 1, FullName = "John Doe", DateOfBirth = new DateTime(1980, 1, 1) }
            };
            var prescriptions = new List<Prescription>
            {
                new Prescription { Id = 1, PatientId = 1, DrugName = "DrugA", Dosage = "10mg", DatePrescribed = DateTime.UtcNow }
            };
            CreateTestFiles(patients, prescriptions);

            var config = GetValidConfig();
            var accessor = new JsonPrescriptionDataAccessor(config);

            var result = accessor.GetPrescriptions();
            Assert.Single(result);
            Assert.Equal("DrugA", result.First().DrugName);

            DeleteTestFiles();
        }

        [Fact]
        public void AddPrescription_AddsPrescriptionAndAssignsIdAndDate()
        {
            DeleteTestFiles();
            var patients = new List<Patient>
            {
                new Patient { Id = 1, FullName = "John Doe", DateOfBirth = new DateTime(1980, 1, 1) }
            };
            var prescriptions = new List<Prescription>
            {
                new Prescription { Id = 1, PatientId = 1, DrugName = "DrugA", Dosage = "10mg", DatePrescribed = DateTime.UtcNow }
            };
            CreateTestFiles(patients, prescriptions);

            var config = GetValidConfig();
            var accessor = new JsonPrescriptionDataAccessor(config);

            var newPrescription = new Prescription
            {
                PatientId = 1,
                DrugName = "DrugB",
                Dosage = "20mg"
            };

            var added = accessor.AddPrescription(newPrescription);
            Assert.Equal(2, added.Id);
            Assert.Equal("DrugB", added.DrugName);
            Assert.True((DateTime.UtcNow - added.DatePrescribed).TotalSeconds < 5);

            DeleteTestFiles();
        }

        [Fact]
        public void AddPrescription_ThrowsArgumentException_WhenInvalid()
        {
            DeleteTestFiles();
            var patients = new List<Patient>
            {
                new Patient { Id = 1, FullName = "John Doe", DateOfBirth = new DateTime(1980, 1, 1) }
            };
            var prescriptions = new List<Prescription>
            {
                new Prescription { Id = 1, PatientId = 1, DrugName = "DrugA", Dosage = "10mg", DatePrescribed = DateTime.UtcNow }
            };
            CreateTestFiles(patients, prescriptions);

            var config = GetValidConfig();
            var accessor = new JsonPrescriptionDataAccessor(config);

            var invalidPrescription = new Prescription
            {
                PatientId = 0,
                DrugName = "",
                Dosage = ""
            };

            Assert.Throws<ArgumentException>(() => accessor.AddPrescription(invalidPrescription));

            DeleteTestFiles();
        }
    }
}