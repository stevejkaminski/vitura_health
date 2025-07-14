using Microsoft.AspNetCore.Mvc;
using VituraHealth.PrescriptionManagement.Domain.Models;
using VituraHealth.PrescriptionManagement.Server.Interfaces;

namespace VituraHealth.PrescriptionManagement.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrescriptionsController : ControllerBase
    {
        private readonly IPrescriptionService _service;

        public PrescriptionsController(IPrescriptionService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetPrescriptions()
        {
            return Ok(_service.GetPrescriptions());
        }

        [HttpPost]
        public IActionResult AddPrescription([FromBody] Prescription prescription)
        {
            if (prescription.PatientId <= 0 || string.IsNullOrWhiteSpace(prescription.DrugName) || string.IsNullOrWhiteSpace(prescription.Dosage))
            {
                return BadRequest("Patient, drug, and dosage are required.");
            }
            var created = _service.AddPrescription(prescription);
            return CreatedAtAction(nameof(GetPrescriptions), new { id = created.Id }, created);
        }

    }
}