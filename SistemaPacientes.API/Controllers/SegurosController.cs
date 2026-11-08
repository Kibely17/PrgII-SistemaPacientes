using Microsoft.AspNetCore.Mvc;
using SistemaPacientes.Application.Contracts;
using SistemaPacientes.Application.Dtos;

namespace SistemaPacientes.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SegurosController : ControllerBase
    {
        private readonly ISeguroService _seguroService;

        public SegurosController(ISeguroService seguroService)
        {
            _seguroService = seguroService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var response = _seguroService.GetAllSeguros();
            return response.Success ? Ok(response.Data) : BadRequest(response.Message);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var response = _seguroService.GetSeguroById(id);
            if (response.Success) return Ok(response.Data);
            return response.StatusCode == 404 ? NotFound(response.Message) : BadRequest(response.Message);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateSeguroDTO request)
        {
            var response = _seguroService.CreateSeguro(request);
            if (!response.Success) return BadRequest(response.Message);
            return CreatedAtAction(nameof(GetById), new { id = response.Data!.Id }, response.Data);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateSeguroDTO request)
        {
            var response = _seguroService.UpdateSeguro(id, request);
            if (response.Success) return NoContent();
            return response.StatusCode == 404 ? NotFound(response.Message) : BadRequest(response.Message);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var response = _seguroService.DeleteSeguro(id);
            if (response.Success) return NoContent();
            return response.StatusCode == 404 ? NotFound(response.Message) : BadRequest(response.Message);
        }
    }
}
