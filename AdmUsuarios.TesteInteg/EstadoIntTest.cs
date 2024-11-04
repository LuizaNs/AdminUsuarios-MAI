using adm_usuarios;
using AdmUsuarios.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using System.Net;
using Xunit;

namespace AdmUsuarios.TesteInteg
{
    public class EstadoIntTest : IClassFixture<WebApplicationFactory<Program>>
    {
            private readonly HttpClient _client;

            public EstadoIntTest(WebApplicationFactory<Program> factory)
            {
                _client = factory.CreateClient();
            }

            [Fact]
            public async Task GetEstados_ReturnsOk()
            {
                var response = await _client.GetAsync("/api/estado");

                response.EnsureSuccessStatusCode();
                var estados = await response.Content.ReadFromJsonAsync<List<Estado>>();
                Assert.NotNull(estados);
            }

            [Fact]
            public async Task GetEstado_ReturnsOk_WhenEstadoExists()
            {
                var estadoId = "valid_estado_id";

                var response = await _client.GetAsync($"/api/estado/{estadoId}");

                response.EnsureSuccessStatusCode();
                var estado = await response.Content.ReadFromJsonAsync<Estado>();
                Assert.Equal(estadoId, estado.Id);
            }

            [Fact]
            public async Task GetEstado_ReturnsNotFound_WhenEstadoDoesNotExist()
            {
                var estadoId = "invalid_estado_id";

                var response = await _client.GetAsync($"/api/estado/{estadoId}");

                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            }

            [Fact]
            public async Task PostEstado_ReturnsCreated()
            {
                var novoEstado = new Estado
                {
                    Id = "1",
                    Nome = "Novo Estado",
                    Sigla = "NE"
                };

                var response = await _client.PostAsJsonAsync("/api/estado", novoEstado);

                response.EnsureSuccessStatusCode();
                var estadoCriado = await response.Content.ReadFromJsonAsync<Estado>();
                Assert.Equal(novoEstado.Nome, estadoCriado.Nome);
            }

            [Fact]
            public async Task UpdateEstado_ReturnsOk_WhenEstadoExists()
            {
                var estadoId = "valid_estado_id"; 
                var estadoAtualizado = new Estado
                {
                    Id = estadoId,
                    Nome = "Estado Atualizado",
                    Sigla = "EA"
                };

                var response = await _client.PutAsJsonAsync($"/api/estado/{estadoId}", estadoAtualizado);

                response.EnsureSuccessStatusCode();
                var estado = await response.Content.ReadFromJsonAsync<Estado>();
                Assert.Equal(estadoAtualizado.Nome, estado.Nome);
            }

            [Fact]
            public async Task UpdateEstado_ReturnsNotFound_WhenEstadoDoesNotExist()
            {
                var estadoId = "invalid_estado_id";
                var estadoAtualizado = new Estado
                {
                    Id = estadoId,
                    Nome = "Estado Atualizado",
                    Sigla = "EA"
                };

                var response = await _client.PutAsJsonAsync($"/api/estado/{estadoId}", estadoAtualizado);

                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            }

            [Fact]
            public async Task DeleteEstado_ReturnsNoContent_WhenEstadoIsDeleted()
            {
                var estadoId = "valid_estado_id";

                var response = await _client.DeleteAsync($"/api/estado/{estadoId}");

                Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            }

            [Fact]
            public async Task DeleteEstado_ReturnsNotFound_WhenEstadoDoesNotExist()
            {
                var estadoId = "invalid_estado_id";

                var response = await _client.DeleteAsync($"/api/estado/{estadoId}");

                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            }
        }
    }

