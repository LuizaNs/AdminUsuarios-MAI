using AdmUsuarios.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net.Http.Json;
using System.Net;
using Xunit;
using adm_usuarios;

namespace AdmUsuarios.Teste
{
    public class EmpresaIntTest : IClassFixture<WebApplicationFactory<Program>>
    {
            private readonly HttpClient _client;

            public EmpresaIntTest(WebApplicationFactory<Program> factory)
            {
                _client = factory.CreateClient();
            }

            [Fact]
            public async Task GetEmpresas_ReturnsOk()
            {
                var response = await _client.GetAsync("/api/empresa");

                response.EnsureSuccessStatusCode();
                var empresas = await response.Content.ReadFromJsonAsync<List<Empresa>>();
                Assert.NotNull(empresas);
            }

            [Fact]
            public async Task GetEmpresa_ReturnsOk_WhenEmpresaExists()
            {
                var empresaId = "valid_empresa_id";

                var response = await _client.GetAsync($"/api/empresa/{empresaId}");

                response.EnsureSuccessStatusCode();
                var empresa = await response.Content.ReadFromJsonAsync<Empresa>();
                Assert.Equal(empresaId, empresa.Id);
            }

            [Fact]
            public async Task GetEmpresa_ReturnsNotFound_WhenEmpresaDoesNotExist()
            {
                var empresaId = "invalid_empresa_id";

                var response = await _client.GetAsync($"/api/empresa/{empresaId}");

                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            }

            [Fact]
            public async Task PostEmpresa_ReturnsCreated()
            {
                var novaEmpresa = new Empresa
                {
                    Id = "1",
                    Nome = "Nova Empresa",
                    CNPJ = "12345678000100"
                };

                var response = await _client.PostAsJsonAsync("/api/empresa", novaEmpresa);

                response.EnsureSuccessStatusCode();
                var empresaCriada = await response.Content.ReadFromJsonAsync<Empresa>();
                Assert.Equal(novaEmpresa.Nome, empresaCriada.Nome);
            }

            [Fact]
            public async Task UpdateEmpresa_ReturnsOk_WhenEmpresaExists()
            {
                var empresaId = "valid_empresa_id";
                var empresaAtualizada = new Empresa
                {
                    Id = empresaId,
                    Nome = "Empresa Atualizada",
                    CNPJ = "98765432000100"
                };

                var response = await _client.PutAsJsonAsync($"/api/empresa/{empresaId}", empresaAtualizada);

                response.EnsureSuccessStatusCode();
                var empresa = await response.Content.ReadFromJsonAsync<Empresa>();
                Assert.Equal(empresaAtualizada.Nome, empresa.Nome);
            }

            [Fact]
            public async Task UpdateEmpresa_ReturnsNotFound_WhenEmpresaDoesNotExist()
            {
                var empresaId = "invalid_empresa_id";
                var empresaAtualizada = new Empresa
                {
                    Id = empresaId,
                    Nome = "Empresa Atualizada",
                    CNPJ = "98765432000100"
                };

                var response = await _client.PutAsJsonAsync($"/api/empresa/{empresaId}", empresaAtualizada);

                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            }

            [Fact]
            public async Task DeleteEmpresa_ReturnsNoContent_WhenEmpresaIsDeleted()
            {
                var empresaId = "valid_empresa_id";

                var response = await _client.DeleteAsync($"/api/empresa/{empresaId}");

                Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            }

            [Fact]
            public async Task DeleteEmpresa_ReturnsNotFound_WhenEmpresaDoesNotExist()
            {
                var empresaId = "invalid_empresa_id";

                var response = await _client.DeleteAsync($"/api/empresa/{empresaId}");

                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            }
        }
    }
