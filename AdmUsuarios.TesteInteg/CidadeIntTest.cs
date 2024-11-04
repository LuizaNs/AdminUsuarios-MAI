using AdmUsuarios.Models;
using System.Net.Http.Json;
using System.Net;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using adm_usuarios;

namespace AdmUsuarios.Teste
{
    public class CidadeIntTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public CidadeIntTest(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetCidades_ReturnsOk()
        {
            var response = await _client.GetAsync("/api/cidade");

            response.EnsureSuccessStatusCode();
            var cidades = await response.Content.ReadFromJsonAsync<List<Cidade>>();
            Assert.NotNull(cidades);
        }

        [Fact]
        public async Task GetCidade_ReturnsOk_WhenCidadeExists()
        {
            var cidadeId = "valid_city_id"; 

            var response = await _client.GetAsync($"/api/cidade/{cidadeId}");

            response.EnsureSuccessStatusCode();
            var cidade = await response.Content.ReadFromJsonAsync<Cidade>();
            Assert.Equal(cidadeId, cidade.Id);
        }

        [Fact]
        public async Task GetCidade_ReturnsNotFound_WhenCidadeDoesNotExist()
        {
            var cidadeId = "invalid_city_id"; 

            var response = await _client.GetAsync($"/api/cidade/{cidadeId}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task PostCidade_ReturnsCreated()
        {
            var novaCidade = new Cidade
            {
                Id = "1",
                Nome = "Nova Cidade"
            };

            var response = await _client.PostAsJsonAsync("/api/cidade", novaCidade);

            response.EnsureSuccessStatusCode();
            var cidadeCriada = await response.Content.ReadFromJsonAsync<Cidade>();
            Assert.Equal(novaCidade.Nome, cidadeCriada.Nome);
        }

        [Fact]
        public async Task UpdateCidade_ReturnsOk_WhenCidadeExists()
        {
            var cidadeId = "valid_city_id"; 
            var cidadeAtualizada = new Cidade
            {
                Id = cidadeId,
                Nome = "Cidade Atualizada"
            };

            var response = await _client.PutAsJsonAsync($"/api/cidade/{cidadeId}", cidadeAtualizada);

            response.EnsureSuccessStatusCode();
            var cidade = await response.Content.ReadFromJsonAsync<Cidade>();
            Assert.Equal(cidadeAtualizada.Nome, cidade.Nome);
        }

        [Fact]
        public async Task UpdateCidade_ReturnsNotFound_WhenCidadeDoesNotExist()
        {
            var cidadeId = "invalid_city_id"; 
            var cidadeAtualizada = new Cidade
            {
                Id = cidadeId,
                Nome = "Cidade Atualizada"
            };

            var response = await _client.PutAsJsonAsync($"/api/cidade/{cidadeId}", cidadeAtualizada);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task DeleteCidade_ReturnsNoContent_WhenCidadeIsDeleted()
        {
            var cidadeId = "valid_city_id"; 

            var response = await _client.DeleteAsync($"/api/cidade/{cidadeId}");

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task DeleteCidade_ReturnsNotFound_WhenCidadeDoesNotExist()
        {
            var cidadeId = "invalid_city_id";

            var response = await _client.DeleteAsync($"/api/cidade/{cidadeId}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}

