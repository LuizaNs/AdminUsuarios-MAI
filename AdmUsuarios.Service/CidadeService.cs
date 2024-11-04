
using AdmUsuarios.Data;
using AdmUsuarios.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace AdmUsuarios.Service
{
    public class CidadeService
    {
        private readonly IMongoCollection<Cidade> _cidadeCollection;

        public CidadeService(IOptions<CidadeDbSettings> cidadeService)
        {
            var mongoClient = new MongoClient(cidadeService.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(cidadeService.Value.DatabaseName);

            _cidadeCollection = mongoDatabase.GetCollection<Cidade>(cidadeService.Value.CidadeCollectionName);
        }

        public async Task<List<Cidade>> GetAsync() => await _cidadeCollection.Find(x => true).ToListAsync();

        public async Task<Cidade> GetAsync(string Id) => await _cidadeCollection.Find(x => x.Id == Id).FirstOrDefaultAsync();

        public async Task CreateAsync(Cidade cidade) => await _cidadeCollection.InsertOneAsync(cidade);

        public async Task UpdateAsync(string Id, Cidade cidade) => await _cidadeCollection.ReplaceOneAsync(x => x.Id == Id, cidade);

        public async Task RemoveAsync(string Id) => await _cidadeCollection.DeleteOneAsync(x => x.Id == Id);

    }
}
