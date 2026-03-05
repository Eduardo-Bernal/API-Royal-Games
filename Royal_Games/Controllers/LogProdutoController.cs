using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Royal_Games.Applications.Services;

namespace Royal_Games.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogProdutoController : ControllerBase
    {
        private readonly LogAlteracaoJogoService _service;

        public LogProdutoController(LogAlteracaoJogoService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult Listar()
        {
            return Ok(_service.Listar());
        }

        [HttpGet("produto/{id}")]
        public ActionResult ListarPorJogo(int id)
        {
            return Ok(_service.ListarPorJogo(id));
        }
    }
}
