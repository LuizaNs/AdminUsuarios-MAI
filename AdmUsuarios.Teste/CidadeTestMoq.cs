using adm_usuarios.Controllers;
using AdmUsuarios.Models;
using AdmUsuarios.Service;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace AdmUsuarios.Teste
{
    public class CidadeTestMoq
    {
            private readonly Mock<CidadeService> _mockService;
            private readonly CidadeController _controller;

            public CidadeTestMoq()
            {
                _mockService = new Mock<CidadeService>();
                _controller = new CidadeController(_mockService.Object);
            }

            [Fact]
            public async Task GetCidades_ReturnsListOfCidades()
            {
                var cidades = new List<Cidade> { new Cidade { Id = "1", Nome = "São Paulo" } };
                _mockService.Setup(service => service.GetAsync()).ReturnsAsync(cidades);

                var result = await _controller.GetCidades();

                Assert.IsType<List<Cidade>>(result);
                Assert.Single(result);
            }

            [Fact]
            public async Task GetCidade_ReturnsCidade_WhenCidadeExists()
            {
                var cidadeId = "1";
                var cidade = new Cidade { Id = cidadeId, Nome = "São Paulo" };
                _mockService.Setup(service => service.GetAsync(cidadeId)).ReturnsAsync(cidade);

                var result = await _controller.GetCidade(cidadeId);

                var actionResult = Assert.IsType<ActionResult<Cidade>>(result);
                var returnValue = Assert.IsType<Cidade>(actionResult.Value);
                Assert.Equal(cidadeId, returnValue.Id);
            }

            [Fact]
            public async Task GetCidade_ReturnsNotFound_WhenCidadeDoesNotExist()
            {
                var cidadeId = "1";
                _mockService.Setup(service => service.GetAsync(cidadeId)).ReturnsAsync((Cidade)null);

                var result = await _controller.GetCidade(cidadeId);

                Assert.IsType<NotFoundResult>(result.Result);
            }

            [Fact]
            public async Task PostEmpresa_ReturnsCreatedCidade()
            {
                var cidade = new Cidade { Id = "1", Nome = "São Paulo" };
                _mockService.Setup(service => service.CreateAsync(cidade)).Returns(Task.CompletedTask);

                var result = await _controller.PostEmpresa(cidade);

                Assert.Equal(cidade, result);
            }

            [Fact]
            public async Task UpdateCidade_ReturnsOk_WhenCidadeExists()
            {
                var cidadeId = "1";
                var cidade = new Cidade { Id = cidadeId, Nome = "São Paulo" };
                var cidadeAtualizada = new Cidade { Id = cidadeId, Nome = "Rio de Janeiro" };

                _mockService.Setup(service => service.GetAsync(cidadeId)).ReturnsAsync(cidade);
                _mockService.Setup(service => service.UpdateAsync(cidadeId, cidadeAtualizada)).Returns(Task.CompletedTask);

                var result = await _controller.UpdateCidade(cidadeId, cidadeAtualizada);

                Assert.IsType<OkObjectResult>(result);
            }

            [Fact]
            public async Task UpdateCidade_ReturnsNotFound_WhenCidadeDoesNotExist()
            {
                var cidadeId = "1";
                var cidadeAtualizada = new Cidade { Id = cidadeId, Nome = "Rio de Janeiro" };

                _mockService.Setup(service => service.GetAsync(cidadeId)).ReturnsAsync((Cidade)null);

                var result = await _controller.UpdateCidade(cidadeId, cidadeAtualizada);

                var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
                Assert.Equal("Cidade não encontrada.", notFoundResult.Value);
            }

            [Fact]
            public async Task DeleteCidade_ReturnsNoContent_WhenCidadeIsDeleted()
            {
                var cidadeId = "1";
                _mockService.Setup(service => service.RemoveAsync(cidadeId)).Returns(Task.CompletedTask);

                var result = await _controller.DeleteCidade(cidadeId);

                Assert.IsType<NoContentResult>(result);
            }
        }
    }


