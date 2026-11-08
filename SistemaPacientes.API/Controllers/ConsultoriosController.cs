using Microsoft.AspNetCore.Mvc;
using SistemaPacientes.Application.Contracts;
using SistemaPacientes.Application.Dtos;

namespace SistemaPacientes.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConsultoriosController : ControllerBase
    {
        private readonly IConsultorioService _consultorioService;

        public ConsultoriosController(IConsultorioService consultorioService)
        {
            _consultorioService = consultorioService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var response = _consultorioService.GetAllConsultorios();
            return response.Success ? Ok(response.Data) : BadRequest(response.Message);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var response = _consultorioService.GetConsultorioById(id);
            if (response.Success) return Ok(response.Data);
            return response.StatusCode == 404 ? NotFound(response.Message) : BadRequest(response.Message);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateConsultorioDTO request)
        {
            var response = _consultorioService.CreateConsultorio(request);
            if (!response.Success) return BadRequest(response.Message);
            return CreatedAtAction(nameof(GetById), new { id = response.Data!.Id }, response.Data);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateConsultorioDTO request)
        {
            var response = _consultorioService.UpdateConsultorio(id, request);
            if (response.Success) return NoContent();
            return response.StatusCode == 404 ? NotFound(response.Message) : BadRequest(response.Message);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var response = _consultorioService.DeleteConsultorio(id);
            if (response.Success) return NoContent();
            return response.StatusCode == 404 ? NotFound(response.Message) : BadRequest(response.Message);
        }
    }
}
