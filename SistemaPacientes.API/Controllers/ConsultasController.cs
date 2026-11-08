using Microsoft.AspNetCore.Mvc;
using SistemaPacientes.Application.Contracts;
using SistemaPacientes.Application.Dtos;

namespace SistemaPacientes.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConsultasController : ControllerBase
    {
        private readonly IConsultaService _consultaService;

        public ConsultasController(IConsultaService consultaService)
        {
            _consultaService = consultaService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var response = _consultaService.GetAllConsultas();
            return response.Success ? Ok(response.Data) : BadRequest(response.Message);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var response = _consultaService.GetConsultaById(id);
            if (response.Success) return Ok(response.Data);
            return response.StatusCode == 404 ? NotFound(response.Message) : BadRequest(response.Message);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateConsultaDTO request)
        {
            var response = _consultaService.CreateConsulta(request);
            if (!response.Success) return BadRequest(response.Message);
            return CreatedAtAction(nameof(GetById), new { id = response.Data!.Id }, response.Data);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateConsultaDTO request)
        {
            var response = _consultaService.UpdateConsulta(id, request);
            if (response.Success) return NoContent();
            return response.StatusCode == 404 ? NotFound(response.Message) : BadRequest(response.Message);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var response = _consultaService.DeleteConsulta(id);
            if (response.Success) return NoContent();
            return response.StatusCode == 404 ? NotFound(response.Message) : BadRequest(response.Message);
        }
    }
}
