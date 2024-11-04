using AdmUsuarios.Models;
using AdmUsuarios.Service;
using Microsoft.AspNetCore.Mvc;

namespace adm_usuarios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoController : ControllerBase
    {
        private readonly EstadoService _estadoService;

        public EstadoController(EstadoService estadoService)
        {
            _estadoService = estadoService;
        }

        /// <summary>
        /// Retorna todos os estados.
        /// </summary>
        /// <returns>Lista de estados</returns>
        /// <response code="200">Retorna a lista de estados</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<Estado>> GetEstados() => await _estadoService.GetAsync();

        /// <summary>
        /// Retorna um estado pelo ID.
        /// </summary>
        /// <param name="id">ID do estado</param>
        /// <returns>Um estado</returns>
        /// <response code="200">Retorna o estado solicitado</response>
        /// <response code="404">Se o estado não for encontrado</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Estado>> GetEstado(string id)
        {
            var estado = await _estadoService.GetAsync(id);

            if (estado == null)
            {
                return NotFound();
            }

            return estado;
        }

        /// <summary>
        /// Cria um novo estado.
        /// </summary>
        /// <param name="estado">Dados do novo estado</param>
        /// <returns>O estado criado</returns>
        /// <response code="201">Retorna o estado recém-criado</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<Estado> PostEstado(Estado estado)
        {
            await _estadoService.CreateAsync(estado);

            return estado;
        }

        /// <summary>
        /// Atualiza um estado existente.
        /// </summary>
        /// <param name="id">ID do estado</param>
        /// <param name="estadoAtualizado">Dados atualizados do estado</param>
        /// <response code="200">Retorna o estado atualizado</response>
        /// <response code="404">Se o estado não for encontrado</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateEstado(string id, Estado estadoAtualizado)
        {
            var estado = await _estadoService.GetAsync(id);

            if (estado == null)
            {
                return NotFound("Estado não encontrado.");
            }

            await _estadoService.UpdateAsync(id, estadoAtualizado);

            return Ok(estado);
        }

        /// <summary>
        /// Deleta um estado pelo ID.
        /// </summary>
        /// <param name="id">ID do estado</param>
        /// <response code="204">Confirma a exclusão do estado</response>
        /// <response code="404">Se o estado não for encontrado</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteEstado(string id)
        {
            await _estadoService.RemoveAsync(id);

            return NoContent();
        }
    }
}

