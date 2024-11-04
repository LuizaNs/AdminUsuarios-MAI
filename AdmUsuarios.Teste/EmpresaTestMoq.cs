using adm_usuarios.Controllers;
using AdmUsuarios.Models;
using AdmUsuarios.Service;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace AdmUsuarios.Teste
{
    public class EmpresaTestMoq
    {
        private readonly Mock<EmpresaService> _mockService;
        private readonly EmpresaController _controller;

        public EmpresaTestMoq()
        {
            _mockService = new Mock<EmpresaService>();
            _controller = new EmpresaController(_mockService.Object);
        }

        [Fact]
        public async Task GetEmpresas_ReturnsListOfEmpresas()
        {
            var empresas = new List<Empresa>
            {
                new Empresa { Id = "1", Nome = "Empresa X", CNPJ = "00.000.000/0001-91", Usuario = new Usuario(), Logradouro = new Logradouro() }
            };
            _mockService.Setup(service => service.GetAsync()).ReturnsAsync(empresas);

            var result = await _controller.GetEmpresas();

            var actionResult = Assert.IsType<List<Empresa>>(result);
            Assert.Single(actionResult);
        }

        [Fact]
        public async Task GetEmpresa_ReturnsEmpresa_WhenEmpresaExists()
        {
            var empresaId = "1";
            var empresa = new Empresa
            {
                Id = empresaId,
                Nome = "Empresa X",
                CNPJ = "00.000.000/0001-91",
                Usuario = new Usuario(),
                Logradouro = new Logradouro()
            };
            _mockService.Setup(service => service.GetAsync(empresaId)).ReturnsAsync(empresa);

            var result = await _controller.GetEmpresa(empresaId);

            var actionResult = Assert.IsType<ActionResult<Empresa>>(result);
            var returnValue = Assert.IsType<Empresa>(actionResult.Value);
            Assert.Equal(empresaId, returnValue.Id);
        }

        [Fact]
        public async Task GetEmpresa_ReturnsNotFound_WhenEmpresaDoesNotExist()
        {
            var empresaId = "1";
            _mockService.Setup(service => service.GetAsync(empresaId)).ReturnsAsync((Empresa)null);

            var result = await _controller.GetEmpresa(empresaId);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task PostEmpresa_ReturnsCreatedEmpresa()
        {
            var empresa = new Empresa
            {
                Id = "1",
                Nome = "Empresa X",
                CNPJ = "00.000.000/0001-91",
                Usuario = new Usuario(),
                Logradouro = new Logradouro()
            };
            _mockService.Setup(service => service.CreateAsync(empresa)).Returns(Task.CompletedTask);

            var result = await _controller.PostEmpresa(empresa);

            Assert.Equal(empresa, result);
        }

        [Fact]
        public async Task UpdateEmpresa_ReturnsOk_WhenEmpresaExists()
        {
            var empresaId = "1";
            var empresa = new Empresa
            {
                Id = empresaId,
                Nome = "Empresa X",
                CNPJ = "00.000.000/0001-91",
                Usuario = new Usuario(),
                Logradouro = new Logradouro()
            };
            var empresaAtualizada = new Empresa
            {
                Id = empresaId,
                Nome = "Empresa Y",
                CNPJ = "11.111.111/0001-12",
                Usuario = new Usuario(),
                Logradouro = new Logradouro()
            };

            _mockService.Setup(service => service.GetAsync(empresaId)).ReturnsAsync(empresa);
            _mockService.Setup(service => service.UpdateAsync(empresaId, empresaAtualizada)).Returns(Task.CompletedTask);

            var result = await _controller.UpdateEmpresa(empresaId, empresaAtualizada);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task UpdateEmpresa_ReturnsNotFound_WhenEmpresaDoesNotExist()
        {
            var empresaId = "1";
            var empresaAtualizada = new Empresa
            {
                Id = empresaId,
                Nome = "Empresa Y",
                CNPJ = "11.111.111/0001-12",
                Usuario = new Usuario(),
                Logradouro = new Logradouro()
            };

            _mockService.Setup(service => service.GetAsync(empresaId)).ReturnsAsync((Empresa)null);

            var result = await _controller.UpdateEmpresa(empresaId, empresaAtualizada);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Empresa não encontrada.", notFoundResult.Value);
        }

        [Fact]
        public async Task DeleteEmpresa_ReturnsNoContent_WhenEmpresaIsDeleted()
        {
            var empresaId = "1";
            _mockService.Setup(service => service.RemoveAsync(empresaId)).Returns(Task.CompletedTask);

            var result = await _controller.DeleteEmpresa(empresaId);

            Assert.IsType<NoContentResult>(result);
        }
    }
}



