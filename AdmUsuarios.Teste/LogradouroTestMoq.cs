using adm_usuarios.Controllers;
using AdmUsuarios.Models;
using AdmUsuarios.Service;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace AdmUsuarios.Teste
{
    public class LogradouroTestMoq
    {
        private readonly Mock<LogradouroService> _mockService;
        private readonly LogradouroController _controller;

        public LogradouroTestMoq()
        {
            _mockService = new Mock<LogradouroService>();
            _controller = new LogradouroController(_mockService.Object);
        }

        [Fact]
        public async Task GetLogradouros_ReturnsListOfLogradouros()
        {
            var logradouros = new List<Logradouro>
            {
                new Logradouro { Id = "1", Cep = "12345-678", Cidade = new Cidade() }
            };
            _mockService.Setup(service => service.GetAsync()).ReturnsAsync(logradouros);

            var result = await _controller.GetLogradouros();

            var actionResult = Assert.IsType<List<Logradouro>>(result);
            Assert.Single(actionResult);
        }

        [Fact]
        public async Task GetLogradouro_ReturnsLogradouro_WhenLogradouroExists()
        {
            var logradouroId = "1";
            var logradouro = new Logradouro
            {
                Id = logradouroId,
                Cep = "12345-678",
                Cidade = new Cidade()
            };
            _mockService.Setup(service => service.GetAsync(logradouroId)).ReturnsAsync(logradouro);

            var result = await _controller.GetLogradouro(logradouroId);

            var actionResult = Assert.IsType<ActionResult<Logradouro>>(result);
            var returnValue = Assert.IsType<Logradouro>(actionResult.Value);
            Assert.Equal(logradouroId, returnValue.Id);
        }

        [Fact]
        public async Task GetLogradouro_ReturnsNotFound_WhenLogradouroDoesNotExist()
        {
            var logradouroId = "1";
            _mockService.Setup(service => service.GetAsync(logradouroId)).ReturnsAsync((Logradouro)null);

            var result = await _controller.GetLogradouro(logradouroId);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task PostLogradouro_ReturnsCreatedLogradouro()
        {
            var logradouro = new Logradouro
            {
                Id = "1",
                Cep = "12345-678",
                Cidade = new Cidade()
            };
            _mockService.Setup(service => service.CreateAsync(logradouro)).Returns(Task.CompletedTask);

            var result = await _controller.PostLogradouro(logradouro);

            Assert.Equal(logradouro, result);
        }

        [Fact]
        public async Task UpdateLogradouro_ReturnsOk_WhenLogradouroExists()
        {
            var logradouroId = "1";
            var logradouro = new Logradouro
            {
                Id = logradouroId,
                Cep = "12345-678",
                Cidade = new Cidade()
            };
            var logradouroAtualizado = new Logradouro
            {
                Id = logradouroId,
                Cep = "87654-321",
                Cidade = new Cidade()
            };

            _mockService.Setup(service => service.GetAsync(logradouroId)).ReturnsAsync(logradouro);
            _mockService.Setup(service => service.UpdateAsync(logradouroId, logradouroAtualizado)).Returns(Task.CompletedTask);

            var result = await _controller.UpdateLogradouro(logradouroId, logradouroAtualizado);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task UpdateLogradouro_ReturnsNotFound_WhenLogradouroDoesNotExist()
        {
            var logradouroId = "1";
            var logradouroAtualizado = new Logradouro
            {
                Id = logradouroId,
                Cep = "87654-321",
                Cidade = new Cidade()
            };

            _mockService.Setup(service => service.GetAsync(logradouroId)).ReturnsAsync((Logradouro)null);

            var result = await _controller.UpdateLogradouro(logradouroId, logradouroAtualizado);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Logradouro não encontrado.", notFoundResult.Value);
        }

        [Fact]
        public async Task DeleteLogradouro_ReturnsNoContent_WhenLogradouroIsDeleted()
        {
            var logradouroId = "1";
            _mockService.Setup(service => service.RemoveAsync(logradouroId)).Returns(Task.CompletedTask);

            var result = await _controller.DeleteLogradouro(logradouroId);

            Assert.IsType<NoContentResult>(result);
        }
    }
}

