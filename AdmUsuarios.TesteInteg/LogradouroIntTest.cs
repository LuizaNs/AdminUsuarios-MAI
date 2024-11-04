using adm_usuarios;
using AdmUsuarios.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using System.Net;
using Xunit;

namespace AdmUsuarios.TesteInteg
{
    public class LogradouroIntTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public LogradouroIntTest(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetLogradouros_ReturnsOk()
        {
            var response = await _client.GetAsync("/api/logradouro");

            response.EnsureSuccessStatusCode();
            var logradouros = await response.Content.ReadFromJsonAsync<List<Logradouro>>();
            Assert.NotNull(logradouros);
        }

        [Fact]
        public async Task GetLogradouro_ReturnsOk_WhenLogradouroExists()
        {
            var logradouroId = "valid_logradouro_id";

            var response = await _client.GetAsync($"/api/logradouro/{logradouroId}");

            response.EnsureSuccessStatusCode();
            var logradouro = await response.Content.ReadFromJsonAsync<Logradouro>();
            Assert.Equal(logradouroId, logradouro.Id);
        }

        [Fact]
        public async Task GetLogradouro_ReturnsNotFound_WhenLogradouroDoesNotExist()
        {
            var logradouroId = "invalid_logradouro_id";

            var response = await _client.GetAsync($"/api/logradouro/{logradouroId}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task PostLogradouro_ReturnsCreated()
        {
            var novaCidade = new Cidade { Id = "valid_city_id", Nome = "Cidade Teste" };
            var novoLogradouro = new Logradouro
            {
                Cep = "12345-678",
                Cidade = novaCidade
            };

            var response = await _client.PostAsJsonAsync("/api/logradouro", novoLogradouro);

            response.EnsureSuccessStatusCode();
            var logradouroCriado = await response.Content.ReadFromJsonAsync<Logradouro>();
            Assert.Equal(novoLogradouro.Cep, logradouroCriado.Cep);
            Assert.Equal(novoLogradouro.Cidade.Id, logradouroCriado.Cidade.Id);
        }

        [Fact]
        public async Task UpdateLogradouro_ReturnsOk_WhenLogradouroExists()
        {
            var logradouroId = "valid_logradouro_id";
            var cidadeAtualizada = new Cidade { Id = "valid_city_id", Nome = "Cidade Atualizada" };
            var logradouroAtualizado = new Logradouro
            {
                Id = logradouroId,
                Cep = "87654-321",
                Cidade = cidadeAtualizada
            };

            var response = await _client.PutAsJsonAsync($"/api/logradouro/{logradouroId}", logradouroAtualizado);

            response.EnsureSuccessStatusCode();
            var logradouro = await response.Content.ReadFromJsonAsync<Logradouro>();
            Assert.Equal(logradouroAtualizado.Cep, logradouro.Cep);
            Assert.Equal(logradouroAtualizado.Cidade.Nome, logradouro.Cidade.Nome);
        }

        [Fact]
        public async Task UpdateLogradouro_ReturnsNotFound_WhenLogradouroDoesNotExist()
        {
            var logradouroId = "invalid_logradouro_id";
            var cidadeAtualizada = new Cidade { Id = "valid_city_id", Nome = "Cidade Atualizada" };
            var logradouroAtualizado = new Logradouro
            {
                Id = logradouroId,
                Cep = "87654-321",
                Cidade = cidadeAtualizada
            };

            var response = await _client.PutAsJsonAsync($"/api/logradouro/{logradouroId}", logradouroAtualizado);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task DeleteLogradouro_ReturnsNoContent_WhenLogradouroIsDeleted()
        {
            var logradouroId = "valid_logradouro_id";

            var response = await _client.DeleteAsync($"/api/logradouro/{logradouroId}");

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task DeleteLogradouro_ReturnsNotFound_WhenLogradouroDoesNotExist()
        {
            var logradouroId = "invalid_logradouro_id";

            var response = await _client.DeleteAsync($"/api/logradouro/{logradouroId}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
