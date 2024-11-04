using adm_usuarios;
using AdmUsuarios.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using System.Net;
using Xunit;

namespace AdmUsuarios.TesteInteg
{
    public class UsuarioIntTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public UsuarioIntTest(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Login_ReturnsToken_WhenCredentialsAreValid()
        {
            var usuario = new Usuario
            {
                Login = "usuarioValido",
                Senha = "senhaValida"
            };

            var response = await _client.PostAsJsonAsync("/api/usuario/login", usuario);

            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<dynamic>();
            Assert.NotNull(result.token);
        }

        [Fact]
        public async Task Login_ReturnsUnauthorized_WhenCredentialsAreInvalid()
        {
            var usuario = new Usuario
            {
                Login = "usuarioInvalido",
                Senha = "senhaInvalida"
            };

            var response = await _client.PostAsJsonAsync("/api/usuario/login", usuario);

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
            var message = await response.Content.ReadAsStringAsync();
            Assert.Equal("Credenciais inválidas.", message);
        }

        [Fact]
        public async Task GetUsuarios_ReturnsListOfUsuarios()
        {
            var response = await _client.GetAsync("/api/usuario");

            response.EnsureSuccessStatusCode();
            var usuarios = await response.Content.ReadFromJsonAsync<List<Usuario>>();
            Assert.NotNull(usuarios);
        }

        [Fact]
        public async Task GetUsuario_ReturnsUsuario_WhenUsuarioExists()
        {
            var usuarioId = "valid_usuario_id";

            var response = await _client.GetAsync($"/api/usuario/{usuarioId}");

            response.EnsureSuccessStatusCode();
            var usuario = await response.Content.ReadFromJsonAsync<Usuario>();
            Assert.Equal(usuarioId, usuario.Id);
        }

        [Fact]
        public async Task GetUsuario_ReturnsNotFound_WhenUsuarioDoesNotExist()
        {
            var usuarioId = "invalid_usuario_id";

            var response = await _client.GetAsync($"/api/usuario/{usuarioId}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task PostUsuario_ReturnsCreatedUsuario()
        {
            var novoUsuario = new Usuario
            {
                Login = "novoUsuario",
                Senha = "novaSenha"
            };

            var response = await _client.PostAsJsonAsync("/api/usuario", novoUsuario);

            response.EnsureSuccessStatusCode();
            var usuarioCriado = await response.Content.ReadFromJsonAsync<Usuario>();
            Assert.Equal(novoUsuario.Login, usuarioCriado.Login);
        }

        [Fact]
        public async Task UpdateUsuario_ReturnsOk_WhenUsuarioExists()
        {
            var usuarioId = "valid_usuario_id";
            var usuarioAtualizado = new Usuario
            {
                Id = usuarioId,
                Login = "usuarioAtualizado",
                Senha = "senhaAtualizada"
            };

            var response = await _client.PutAsJsonAsync($"/api/usuario/{usuarioId}", usuarioAtualizado);

            response.EnsureSuccessStatusCode();
            var usuario = await response.Content.ReadFromJsonAsync<Usuario>();
            Assert.Equal(usuarioAtualizado.Login, usuario.Login);
        }

        [Fact]
        public async Task UpdateUsuario_ReturnsNotFound_WhenUsuarioDoesNotExist()
        {
            var usuarioId = "invalid_usuario_id";
            var usuarioAtualizado = new Usuario
            {
                Id = usuarioId,
                Login = "usuarioAtualizado",
                Senha = "senhaAtualizada"
            };

            var response = await _client.PutAsJsonAsync($"/api/usuario/{usuarioId}", usuarioAtualizado);

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            var message = await response.Content.ReadAsStringAsync();
            Assert.Equal("Usuário não encontrado.", message);
        }

        [Fact]
        public async Task DeleteUsuario_ReturnsNoContent_WhenUsuarioIsDeleted()
        {
            var usuarioId = "valid_usuario_id";

            var response = await _client.DeleteAsync($"/api/usuario/{usuarioId}");

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task DeleteUsuario_ReturnsNotFound_WhenUsuarioDoesNotExist()
        {
            var usuarioId = "invalid_usuario_id";

            var response = await _client.DeleteAsync($"/api/usuario/{usuarioId}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
