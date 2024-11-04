using AdmUsuarios.Data;
using AdmUsuarios.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace AdmUsuarios.Service
{
    public class LogradouroService
    {
        private readonly IMongoCollection<Logradouro> _logradouroCollection;

        public LogradouroService(IOptions<LogradouroDbSettings> logradouroService)
        {
            var mongoClient = new MongoClient(logradouroService.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(logradouroService.Value.DatabaseName);

            _logradouroCollection = mongoDatabase.GetCollection<Logradouro>(logradouroService.Value.LogradouroCollectionName);
        }

        public async Task<List<Logradouro>> GetAsync() => await _logradouroCollection.Find(x => true).ToListAsync();

        public async Task<Logradouro> GetAsync(string Id) => await _logradouroCollection.Find(x => x.Id == Id).FirstOrDefaultAsync();

        public async Task CreateAsync(Logradouro logradouro) => await _logradouroCollection.InsertOneAsync(logradouro);

        public async Task UpdateAsync(string Id, Logradouro logradouro) => await _logradouroCollection.ReplaceOneAsync(x => x.Id == Id, logradouro);

        public async Task RemoveAsync(string Id) => await _logradouroCollection.DeleteOneAsync(x => x.Id == Id);


    }
}
