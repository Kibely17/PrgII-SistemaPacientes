using Microsoft.AspNetCore.Mvc;
using SistemaPacientes.Application.Contracts;
using SistemaPacientes.Application.Dtos;

namespace SistemaPacientes.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DiagnosticosController : ControllerBase
    {
        private readonly IDiagnosticoService _diagnosticoService;

        public DiagnosticosController(IDiagnosticoService diagnosticoService)
        {
            _diagnosticoService = diagnosticoService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var response = _diagnosticoService.GetAllDiagnosticos();
            return response.Success ? Ok(response.Data) : BadRequest(response.Message);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var response = _diagnosticoService.GetDiagnosticoById(id);
            if (response.Success) return Ok(response.Data);
            return response.StatusCode == 404 ? NotFound(response.Message) : BadRequest(response.Message);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateDiagnosticoDTO request)
        {
            var response = _diagnosticoService.CreateDiagnostico(request);
            if (!response.Success) return BadRequest(response.Message);
            return CreatedAtAction(nameof(GetById), new { id = response.Data!.Id }, response.Data);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateDiagnosticoDTO request)
        {
            var response = _diagnosticoService.UpdateDiagnostico(id, request);
            if (response.Success) return NoContent();
            return response.StatusCode == 404 ? NotFound(response.Message) : BadRequest(response.Message);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var response = _diagnosticoService.DeleteDiagnostico(id);
            if (response.Success) return NoContent();
            return response.StatusCode == 404 ? NotFound(response.Message) : BadRequest(response.Message);
        }
    }
}
