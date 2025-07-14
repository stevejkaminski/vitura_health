using Microsoft.AspNetCore.Mvc;
using VituraHealth.PrescriptionManagement.Server.Interfaces;

namespace VituraHealth.PrescriptionManagement.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly IPrescriptionService _service;

        public PatientsController(IPrescriptionService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetPatients()
        {
            return Ok(_service.GetPatients());
        }
    }
}
