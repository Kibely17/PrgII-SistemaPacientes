using Microsoft.AspNetCore.Mvc;
using SistemaPacientes.Application.Contracts;
using SistemaPacientes.Application.Dtos;

namespace SistemaPacientes.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EspecialidadesController : ControllerBase
    {
        private readonly IEspecialidadService _especialidadService;

        public EspecialidadesController(IEspecialidadService especialidadService)
        {
            _especialidadService = especialidadService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var response = _especialidadService.GetAllEspecialidades();
            return response.Success ? Ok(response.Data) : BadRequest(response.Message);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var response = _especialidadService.GetEspecialidadById(id);
            if (response.Success) return Ok(response.Data);
            return response.StatusCode == 404 ? NotFound(response.Message) : BadRequest(response.Message);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateEspecialidadDTO request)
        {
            var response = _especialidadService.CreateEspecialidad(request);
            if (!response.Success) return BadRequest(response.Message);
            return CreatedAtAction(nameof(GetById), new { id = response.Data!.Id }, response.Data);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateEspecialidadDTO request)
        {
            var response = _especialidadService.UpdateEspecialidad(id, request);
            if (response.Success) return NoContent();
            return response.StatusCode == 404 ? NotFound(response.Message) : BadRequest(response.Message);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var response = _especialidadService.DeleteEspecialidad(id);
            if (response.Success) return NoContent();
            return response.StatusCode == 404 ? NotFound(response.Message) : BadRequest(response.Message);
        }
    }
}
