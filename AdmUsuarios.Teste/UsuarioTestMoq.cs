using adm_usuarios.Controllers;
using AdmUsuarios.Models;
using AdmUsuarios.Service;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace AdmUsuarios.Teste
{
    public class UsuarioTestMoq
    {
        private readonly Mock<UsuarioService> _mockService;
        private readonly UsuarioController _controller;

        public UsuarioTestMoq()
        {
            _mockService = new Mock<UsuarioService>();
            _controller = new UsuarioController(_mockService.Object);
        }

        [Fact]
        public async Task Login_ReturnsToken_WhenCredentialsAreValid()
        {
            var usuario = new Usuario
            {
                Login = "usuario",
                Senha = "senha"
            };
            var usuarioDb = new Usuario
            {
                Id = "1",
                Login = "usuario",
                Senha = "senha"
            };
            _mockService.Setup(service => service.GetByLoginAsync(usuario.Login)).ReturnsAsync(usuarioDb);

            var result = await _controller.Login(usuario);

            var actionResult = Assert.IsType<ActionResult<dynamic>>(result);
            var returnValue = Assert.IsType<OkObjectResult>(actionResult.Result);
            Assert.Contains("token", ((dynamic)returnValue.Value).GetType().GetPropertyNames());
        }

        [Fact]
        public async Task Login_ReturnsUnauthorized_WhenCredentialsAreInvalid()
        {
            var usuario = new Usuario
            {
                Login = "usuario",
                Senha = "senha"
            };
            _mockService.Setup(service => service.GetByLoginAsync(usuario.Login)).ReturnsAsync((Usuario)null);

            var result = await _controller.Login(usuario);

            var actionResult = Assert.IsType<ActionResult<dynamic>>(result);
            Assert.IsType<UnauthorizedObjectResult>(actionResult.Result);
            Assert.Equal("Credenciais inválidas.", ((UnauthorizedObjectResult)actionResult.Result).Value);
        }

        [Fact]
        public async Task GetUsuarios_ReturnsListOfUsuarios()
        {
            var usuarios = new List<Usuario>
            {
                new Usuario { Id = "1", Login = "usuario1", Senha = "senha1" },
                new Usuario { Id = "2", Login = "usuario2", Senha = "senha2" }
            };
            _mockService.Setup(service => service.GetAsync()).ReturnsAsync(usuarios);

            var result = await _controller.GetUsuarios();

            var actionResult = Assert.IsType<List<Usuario>>(result);
            Assert.Equal(2, actionResult.Count);
        }

        [Fact]
        public async Task GetUsuario_ReturnsUsuario_WhenUsuarioExists()
        {
            var usuarioId = "1";
            var usuario = new Usuario
            {
                Id = usuarioId,
                Login = "usuario1",
                Senha = "senha1"
            };
            _mockService.Setup(service => service.GetAsync(usuarioId)).ReturnsAsync(usuario);

            var result = await _controller.GetUsuario(usuarioId);

            var actionResult = Assert.IsType<ActionResult<Usuario>>(result);
            var returnValue = Assert.IsType<Usuario>(actionResult.Value);
            Assert.Equal(usuarioId, returnValue.Id);
        }

        [Fact]
        public async Task GetUsuario_ReturnsNotFound_WhenUsuarioDoesNotExist()
        {
            var usuarioId = "1";
            _mockService.Setup(service => service.GetAsync(usuarioId)).ReturnsAsync((Usuario)null);

            var result = await _controller.GetUsuario(usuarioId);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task PostUsuario_ReturnsCreatedUsuario()
        {
            var usuario = new Usuario
            {
                Id = "1",
                Login = "usuario1",
                Senha = "senha1"
            };
            _mockService.Setup(service => service.CreateAsync(usuario)).Returns(Task.CompletedTask);

            var result = await _controller.PostUsuario(usuario);

            Assert.Equal(usuario, result);
        }

        [Fact]
        public async Task UpdateUsuario_ReturnsOk_WhenUsuarioExists()
        {
            var usuarioId = "1";
            var usuario = new Usuario
            {
                Id = usuarioId,
                Login = "usuario1",
                Senha = "senha1"
            };
            var usuarioAtualizado = new Usuario
            {
                Id = usuarioId,
                Login = "usuarioAtualizado",
                Senha = "senhaAtualizada"
            };

            _mockService.Setup(service => service.GetAsync(usuarioId)).ReturnsAsync(usuario);
            _mockService.Setup(service => service.UpdateAsync(usuarioId, usuarioAtualizado)).Returns(Task.CompletedTask);

            var result = await _controller.UpdateUsuario(usuarioId, usuarioAtualizado);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task UpdateUsuario_ReturnsNotFound_WhenUsuarioDoesNotExist()
        {
            var usuarioId = "1";
            var usuarioAtualizado = new Usuario
            {
                Id = usuarioId,
                Login = "usuarioAtualizado",
                Senha = "senhaAtualizada"
            };

            _mockService.Setup(service => service.GetAsync(usuarioId)).ReturnsAsync((Usuario)null);

            var result = await _controller.UpdateUsuario(usuarioId, usuarioAtualizado);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Usuário não encontrado.", notFoundResult.Value);
        }

        [Fact]
        public async Task DeleteUsuario_ReturnsNoContent_WhenUsuarioIsDeleted()
        {
            var usuarioId = "1";
            _mockService.Setup(service => service.RemoveAsync(usuarioId)).Returns(Task.CompletedTask);

            var result = await _controller.DeleteUsuario(usuarioId);

            Assert.IsType<NoContentResult>(result);
        }
    }
}
