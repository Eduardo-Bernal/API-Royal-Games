using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Royal_Games.Applications.Services;
using Royal_Games.DTOs.JogoDTOs;
using Royal_Games.Exceptions;
using System.Security.Claims;

namespace Royal_Games.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JogoController : ControllerBase
    {
        private readonly JogoService _service;

        public JogoController(JogoService service)
        {
            _service = service;
        }

        private int ObterIdUsuarioLogado()
        {
            string? idTexto = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(idTexto))
            {
                throw new DomainException("usuário não autenticado");
            }

            return int.Parse(idTexto);
        }

        [HttpGet]
        public ActionResult<List<LerJogoDto>> Listar()
        {
            List<LerJogoDto> jogo = _service.Listar();
            return Ok(jogo);
        }

        [HttpGet("{id}")]
        public ActionResult<LerJogoDto> ObterPorId(int id)
        {
            LerJogoDto jogo = _service.ObterPorID(id);
            if (jogo == null)
            {
                return NotFound();
            }
            return Ok(jogo);
        }

        [HttpGet("{id}/imagem")]
        public ActionResult ObterImagem(int id)
        {
            try
            {
                var Imagem = _service.ObterImagem(id);
                return File(Imagem, "image/jpeg");
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        [Authorize]
        public ActionResult Adicionar([FromForm] CriarJogoDto jogoDto)
        {
            try
            {
                int usuarioId = ObterIdUsuarioLogado();
                _service.Adicionar(jogoDto, usuarioId);
                return StatusCode(201);
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{id}")]
        [Consumes("multipart/form-data")]
        [Authorize]

        public ActionResult Atualizar(int id, [FromForm] AtualizarJogoDto jogoDto)
        {
            try
            {
                _service.Atualizar(id, jogoDto);
                return NoContent();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Authorize]

        public ActionResult Remover(int id)
        {
            try
            {
                _service.Remover(id);
                return NoContent();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
