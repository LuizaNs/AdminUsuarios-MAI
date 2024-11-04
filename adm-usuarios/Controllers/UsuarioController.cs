using AdmUsuarios.Models;
using AdmUsuarios.Service;
using Microsoft.AspNetCore.Mvc;

namespace adm_usuarios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;
        public UsuarioController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        /// <summary>
        /// Endpoint de login que gera um token JWT.
        /// </summary>
        /// <param name="usuario">Dados do usuário para login</param>
        /// <returns>Token JWT se as credenciais estiverem corretas</returns>
        [HttpPost("login")]
        public async Task<ActionResult<dynamic>> Login([FromBody] Usuario usuario)
        {
            var usuarioDb = await _usuarioService.GetByLoginAsync(usuario.Login);
            if (usuarioDb == null || usuarioDb.Senha != usuario.Senha)
                return Unauthorized("Credenciais inválidas.");

            var token = TokenService.GenerateToken(usuarioDb);

            return Ok(new { token });
        }



        /// <summary>
        /// Retorna todos os usuários.
        /// </summary>
        /// <returns>Lista de usuários</returns>
        /// <response code="200">Retorna a lista de usuários</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<Usuario>> GetUsuarios() => await _usuarioService.GetAsync();

        /// <summary>
        /// Retorna um usuário pelo ID.
        /// </summary>
        /// <param name="id">ID do usuário</param>
        /// <returns>Um usuário</returns>
        /// <response code="200">Retorna o usuário solicitado</response>
        /// <response code="404">Se o usuário não for encontrado</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Usuario>> GetUsuario(string id)
        {
            var usuario = await _usuarioService.GetAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        /// <summary>
        /// Cria um novo usuário.
        /// </summary>
        /// <param name="usuario">Dados do novo usuário</param>
        /// <returns>O usuário criado</returns>
        /// <response code="201">Retorna o usuário recém-criado</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<Usuario> PostUsuario(Usuario usuario)
        {
            await _usuarioService.CreateAsync(usuario);

            return usuario;
        }


        /// <summary>
        /// Atualiza um usuário existente.
        /// </summary>
        /// <param name="id">ID do usuário</param>
        /// <param name="usuarioAtualizado">Dados atualizados do usuário</param>
        /// <response code="200">Retorna o usuário atualizado</response>
        /// <response code="404">Se o usuário não for encontrado</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateUsuario(string id, Usuario usuarioAtualizado)
        {
            var usuario = await _usuarioService.GetAsync(id);

            if (usuario == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            await _usuarioService.UpdateAsync(id, usuarioAtualizado);

            return Ok(usuario);
        }

        /// <summary>
        /// Deleta um usuário pelo ID.
        /// </summary>
        /// <param name="id">ID do usuário</param>
        /// <response code="204">Confirma a exclusão do usuário</response>
        /// <response code="404">Se o usuário não for encontrado</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUsuario(string id)
        {
            await _usuarioService.RemoveAsync(id);

            return NoContent();
        }
    }
}
