using AdmUsuarios.Models;
using AdmUsuarios.Service;
using Microsoft.AspNetCore.Mvc;

namespace adm_usuarios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CidadeController : ControllerBase
    {
        private readonly CidadeService _cidadeService;

        public CidadeController(CidadeService cidadeService)
        {
            _cidadeService = cidadeService;
        }

        /// <summary>
        /// Retorna todos as cidades.
        /// </summary>
        /// <returns>Lista de cidades</returns>
        /// <response code="200">Retorna a lista de cidades</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<Cidade>> GetCidades() => await _cidadeService.GetAsync();

        /// <summary>
        /// Retorna uma cidade pelo ID.
        /// </summary>
        /// <param name="id">ID da cidade</param>
        /// <returns>Uma cidade</returns>
        /// <response code="200">Retorna a cidade solicitada</response>
        /// <response code="404">Se a cidade não for encontrada</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Cidade>> GetCidade(string id)
        {
            var cidade = await _cidadeService.GetAsync(id);

            if (cidade == null)
            {
                return NotFound();
            }

            return cidade;
        }

        /// <summary>
        /// Cria uma nova cidade.
        /// </summary>
        /// <param name="cidade">Dados da nova cidade</param>
        /// <returns>A cidade criada</returns>
        /// <response code="201">Retorna a cidade recém-criada</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<Cidade> PostEmpresa(Cidade cidade)
        {
            await _cidadeService.CreateAsync(cidade);

            return cidade;
        }

        /// <summary>
        /// Atualiza uma cidade existente.
        /// </summary>
        /// <param name="id">ID da cidade</param>
        /// <param name="cidadeAtualizada">Dados atualizados da cidade</param>
        /// <response code="200">Retorna a cidade atualizada</response>
        /// <response code="404">Se a cidade não for encontrada</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCidade(string id, Cidade cidadeAtualizada)
        {
            var cidade = await _cidadeService.GetAsync(id);

            if (cidade == null)
            {
                return NotFound("Cidade não encontrada.");
            }

            await _cidadeService.UpdateAsync(id, cidadeAtualizada);

            return Ok(cidade);
        }

        /// <summary>
        /// Deleta uma cidade pelo ID.
        /// </summary>
        /// <param name="id">ID da cidade</param>
        /// <response code="204">Confirma a exclusão da cidade</response>
        /// <response code="404">Se a empresa não for cidade</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCidade(string id)
        {
            await _cidadeService.RemoveAsync(id);

            return NoContent();
        }
    }
}

