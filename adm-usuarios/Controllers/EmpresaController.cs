using AdmUsuarios.Models;
using AdmUsuarios.Service;
using Microsoft.AspNetCore.Mvc;

namespace adm_usuarios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {
        private readonly EmpresaService _empresaService;

        public EmpresaController(EmpresaService empresaService)
        {
            _empresaService = empresaService;
        }

        /// <summary>
        /// Retorna todos as empresas.
        /// </summary>
        /// <returns>Lista de empresas</returns>
        /// <response code="200">Retorna a lista de empresas</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<Empresa>> GetEmpresas() => await _empresaService.GetAsync();

        /// <summary>
        /// Retorna uma empresa pelo ID.
        /// </summary>
        /// <param name="id">ID da empresa</param>
        /// <returns>Uma empresa</returns>
        /// <response code="200">Retorna a empresa solicitada</response>
        /// <response code="404">Se a empresa não for encontrada</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Empresa>> GetEmpresa(string id)
        {
            var empresa = await _empresaService.GetAsync(id);

            if (empresa == null)
            {
                return NotFound();
            }

            return empresa;
        }

        /// <summary>
        /// Cria uma nova empresa.
        /// </summary>
        /// <param name="empresa">Dados da nova empresa</param>
        /// <returns>A empresa criada</returns>
        /// <response code="201">Retorna a empresa recém-criada</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<Empresa> PostEmpresa(Empresa empresa)
        {
            await _empresaService.CreateAsync(empresa);

            return empresa;
        }

        /// <summary>
        /// Atualiza uma empresa existente.
        /// </summary>
        /// <param name="id">ID da empresa</param>
        /// <param name="empresaAtualizada">Dados atualizados da empresa</param>
        /// <response code="200">Retorna a empresa atualizada</response>
        /// <response code="404">Se a empresa não for encontrada</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateEmpresa(string id, Empresa empresaAtualizada)
        {
            var empresa = await _empresaService.GetAsync(id);

            if (empresa == null)
            {
                return NotFound("Empresa não encontrada.");
            }

            await _empresaService.UpdateAsync(id, empresaAtualizada);

            return Ok(empresa);
        }

        /// <summary>
        /// Deleta uma empresa pelo ID.
        /// </summary>
        /// <param name="id">ID da empresa</param>
        /// <response code="204">Confirma a exclusão da empresa</response>
        /// <response code="404">Se a empresa não for encontrada</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteEmpresa(string id)
        {
            await _empresaService.RemoveAsync(id);

            return NoContent();
        }
    }


}

