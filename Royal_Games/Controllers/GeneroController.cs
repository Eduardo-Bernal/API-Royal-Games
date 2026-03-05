using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Royal_Games.Applications.Services;
using Royal_Games.DTOs.GeneroDto;
using Royal_Games.DTOs.JogoDTOs;

namespace Royal_Games.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneroController : ControllerBase
    {
        private readonly GeneroService _service;

        public GeneroController(GeneroService service)
        {
            _service = service;
        }

        [HttpGet]

        public ActionResult<List<LerGeneroDto>> Listar()
        {
            List<LerGeneroDto> generos = _service.Listar();
            return Ok(generos);
        }

        [HttpGet("{id}")]

        public ActionResult<LerGeneroDto> ObterPorId(int id)
        {
            LerGeneroDto genero = _service.ObterPorID(id);

            if (genero == null)
            {
                return NotFound();
            }
            return Ok(genero);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Adicionar([FromForm] CriarGeneroDto generoDto)
        {
            try
            {
                _service.Adicionar(generoDto);
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public ActionResult Atualizar(int id, [FromForm] CriarGeneroDto generoDto)
        {
            try
            {
                _service.Atualizar(id, generoDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]

        public ActionResult Remover(int id)
        {
            try
            {
                _service.Remover(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
