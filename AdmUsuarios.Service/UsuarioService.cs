using AdmUsuarios.Data;
using AdmUsuarios.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace AdmUsuarios.Service
{
    public class UsuarioService
    {
        private readonly IMongoCollection<Usuario> _usuarioCollection;

        public UsuarioService(IOptions<UsuarioDbSettings> usuarioService)
        {
            var mongoClient = new MongoClient(usuarioService.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(usuarioService.Value.DatabaseName);

            _usuarioCollection = mongoDatabase.GetCollection<Usuario>(usuarioService.Value.UsuarioCollectionName);
        }

        public async Task<List<Usuario>> GetAsync() => await _usuarioCollection.Find(x => true).ToListAsync();
        public async Task<Usuario> GetAsync(string Id) => await _usuarioCollection.Find(x => x.Id == Id).FirstOrDefaultAsync();

        public async Task CreateAsync(Usuario usuario) => await _usuarioCollection.InsertOneAsync(usuario);

        public async Task UpdateAsync(string Id, Usuario usuario) => await _usuarioCollection.ReplaceOneAsync(x => x.Id == Id, usuario);

        public async Task RemoveAsync(string Id) => await _usuarioCollection.DeleteOneAsync(x => x.Id == Id);

        public async Task<Usuario> GetByLoginAsync(string login)
        {
            throw new NotImplementedException();
        }
    }


}
