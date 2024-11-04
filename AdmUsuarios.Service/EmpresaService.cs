using AdmUsuarios.Data;
using AdmUsuarios.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace AdmUsuarios.Service
{
    public class EmpresaService
    {
        private readonly IMongoCollection<Empresa> _empresaCollection;

        public EmpresaService(IOptions<EmpresaDbSettings> empresaService)
        {
            var mongoClient = new MongoClient(empresaService.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(empresaService.Value.DatabaseName);

            _empresaCollection = mongoDatabase.GetCollection<Empresa>(empresaService.Value.EmpresaCollectionName);
        }

        public async Task<List<Empresa>> GetAsync() => await _empresaCollection.Find(x => true).ToListAsync();

        public async Task<Empresa> GetAsync(string Id) => await _empresaCollection.Find(x => x.Id == Id).FirstOrDefaultAsync();

        public async Task CreateAsync(Empresa empresa) => await _empresaCollection.InsertOneAsync(empresa);

        public async Task UpdateAsync(string Id, Empresa empresa) => await _empresaCollection.ReplaceOneAsync(x => x.Id == Id, empresa);

        public async Task RemoveAsync(string Id) => await _empresaCollection.DeleteOneAsync(x => x.Id == Id);


    }


}
