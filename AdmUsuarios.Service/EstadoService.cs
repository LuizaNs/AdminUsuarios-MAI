using AdmUsuarios.Data;
using AdmUsuarios.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace AdmUsuarios.Service
{
    public class EstadoService
    {
        private readonly IMongoCollection<Estado> _estadoCollection;

        public EstadoService(IOptions<EstadoDbSettings> estadoService)
        {
            var mongoClient = new MongoClient(estadoService.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(estadoService.Value.DatabaseName);

            _estadoCollection = mongoDatabase.GetCollection<Estado>(estadoService.Value.EstadoCollectionName);
        }

        public async Task<List<Estado>> GetAsync() => await _estadoCollection.Find(x => true).ToListAsync();

        public async Task<Estado> GetAsync(string Id) => await _estadoCollection.Find(x => x.Id == Id).FirstOrDefaultAsync();

        public async Task CreateAsync(Estado estado) => await _estadoCollection.InsertOneAsync(estado);

        public async Task UpdateAsync(string Id, Estado estado) => await _estadoCollection.ReplaceOneAsync(x => x.Id == Id, estado);

        public async Task RemoveAsync(string Id) => await _estadoCollection.DeleteOneAsync(x => x.Id == Id);


    }
}

