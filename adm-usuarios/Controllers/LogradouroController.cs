using AdmUsuarios.Models;
using AdmUsuarios.Service;
using Microsoft.AspNetCore.Mvc;

namespace adm_usuarios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogradouroController : ControllerBase
    {
        private readonly LogradouroService _logradouroService;

        public LogradouroController(LogradouroService logradouroService)
        {
            _logradouroService = logradouroService;
        }

        /// <summary>
        /// Retorna todos os logradouros.
        /// </summary>
        /// <returns>Lista de logradouros</returns>
        /// <response code="200">Retorna a lista de logradouros</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<Logradouro>> GetLogradouros() => await _logradouroService.GetAsync();

        /// <summary>
        /// Retorna um logradouro pelo ID.
        /// </summary>
        /// <param name="id">ID do logradouro</param>
        /// <returns>Um logradouro</returns>
        /// <response code="200">Retorna o logradouro solicitado</response>
        /// <response code="404">Se o logradouro não for encontrado</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Logradouro>> GetLogradouro(string id)
        {
            var logradouro = await _logradouroService.GetAsync(id);

            if (logradouro == null)
            {
                return NotFound();
            }

            return logradouro;
        }

        /// <summary>
        /// Cria um novo logradouro.
        /// </summary>
        /// <param name="logradouro">Dados do novo logradouro</param>
        /// <returns>O logradouro criado</returns>
        /// <response code="201">Retorna o logradouro recém-criado</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<Logradouro> PostLogradouro(Logradouro logradouro)
        {
            await _logradouroService.CreateAsync(logradouro);

            return logradouro;
        }

        /// <summary>
        /// Atualiza um logradouro existente.
        /// </summary>
        /// <param name="id">ID do logradouro</param>
        /// <param name="logradouroAtualizado">Dados atualizados do logradouro</param>
        /// <response code="200">Retorna o logradouro atualizado</response>
        /// <response code="404">Se o logradouro não for encontrado</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateLogradouro(string id, Logradouro logradouroAtualizado)
        {
            var logradouro = await _logradouroService.GetAsync(id);

            if (logradouro == null)
            {
                return NotFound("Logradouro não encontrado.");
            }

            await _logradouroService.UpdateAsync(id, logradouroAtualizado);

            return Ok(logradouro);
        }

        /// <summary>
        /// Deleta um logradouro pelo ID.
        /// </summary>
        /// <param name="id">ID do logradouro</param>
        /// <response code="204">Confirma a exclusão do logradouro</response>
        /// <response code="404">Se o logradouro não for encontrado</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteLogradouro(string id)
        {
            await _logradouroService.RemoveAsync(id);

            return NoContent();
        }
    }
}


