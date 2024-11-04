using adm_usuarios.Controllers;
using AdmUsuarios.Models;
using AdmUsuarios.Service;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace AdmUsuarios.Teste
{
    public class EstadoTestMoq
    {
        private readonly Mock<EstadoService> _mockService;
        private readonly EstadoController _controller;

        public EstadoTestMoq()
        {
            _mockService = new Mock<EstadoService>();
            _controller = new EstadoController(_mockService.Object);
        }

        [Fact]
        public async Task GetEstados_ReturnsListOfEstados()
        {
            var estados = new List<Estado>
            {
                new Estado { Id = "1", Nome = "São Paulo", Sigla = "SP" }
            };
            _mockService.Setup(service => service.GetAsync()).ReturnsAsync(estados);

            var result = await _controller.GetEstados();

            var actionResult = Assert.IsType<List<Estado>>(result);
            Assert.Single(actionResult);
        }

        [Fact]
        public async Task GetEstado_ReturnsEstado_WhenEstadoExists()
        {
            var estadoId = "1";
            var estado = new Estado
            {
                Id = estadoId,
                Nome = "São Paulo",
                Sigla = "SP"
            };
            _mockService.Setup(service => service.GetAsync(estadoId)).ReturnsAsync(estado);

            var result = await _controller.GetEstado(estadoId);

            var actionResult = Assert.IsType<ActionResult<Estado>>(result);
            var returnValue = Assert.IsType<Estado>(actionResult.Value);
            Assert.Equal(estadoId, returnValue.Id);
        }

        [Fact]
        public async Task GetEstado_ReturnsNotFound_WhenEstadoDoesNotExist()
        {
            var estadoId = "1";
            _mockService.Setup(service => service.GetAsync(estadoId)).ReturnsAsync((Estado)null);

            var result = await _controller.GetEstado(estadoId);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task PostEstado_ReturnsCreatedEstado()
        {
            var estado = new Estado
            {
                Id = "1",
                Nome = "São Paulo",
                Sigla = "SP"
            };
            _mockService.Setup(service => service.CreateAsync(estado)).Returns(Task.CompletedTask);

            var result = await _controller.PostEstado(estado);

            Assert.Equal(estado, result);
        }

        [Fact]
        public async Task UpdateEstado_ReturnsOk_WhenEstadoExists()
        {
            var estadoId = "1";
            var estado = new Estado
            {
                Id = estadoId,
                Nome = "São Paulo",
                Sigla = "SP"
            };
            var estadoAtualizado = new Estado
            {
                Id = estadoId,
                Nome = "Rio de Janeiro",
                Sigla = "RJ"
            };

            _mockService.Setup(service => service.GetAsync(estadoId)).ReturnsAsync(estado);
            _mockService.Setup(service => service.UpdateAsync(estadoId, estadoAtualizado)).Returns(Task.CompletedTask);

            var result = await _controller.UpdateEstado(estadoId, estadoAtualizado);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task UpdateEstado_ReturnsNotFound_WhenEstadoDoesNotExist()
        {
            var estadoId = "1";
            var estadoAtualizado = new Estado
            {
                Id = estadoId,
                Nome = "Rio de Janeiro",
                Sigla = "RJ"
            };

            _mockService.Setup(service => service.GetAsync(estadoId)).ReturnsAsync((Estado)null);

            var result = await _controller.UpdateEstado(estadoId, estadoAtualizado);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Estado não encontrado.", notFoundResult.Value);
        }

        [Fact]
        public async Task DeleteEstado_ReturnsNoContent_WhenEstadoIsDeleted()
        {
            var estadoId = "1";
            _mockService.Setup(service => service.RemoveAsync(estadoId)).Returns(Task.CompletedTask);

            var result = await _controller.DeleteEstado(estadoId);

            Assert.IsType<NoContentResult>(result);
        }
    }
}

