using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Linq;
using System.Threading.Tasks;
using webapi.mongodb.Data.Config;
using webapi.mongodb.Entitys;
using webapi.mongodb.Model;

namespace webapi.mongodb.Data
{
    public class ClienteService
    {
        private readonly IMongoCollection<Cliente> _clientesCollection;

        public ClienteService(IOptions<ClientesStoreDatabaseSettings> settings)
        {
            var mdbClient = new MongoClient(settings.Value.ConnectionString);
            var database = mdbClient.GetDatabase(settings.Value.DatabaseName);
            _clientesCollection = database.GetCollection<Cliente>(settings.Value.ClientesCollectionName);
        }

        public async Task<PagedResult<Cliente>> Get(int pageIndex, int pageSize)
        {
            var start = DateTime.Now.AddDays(1);
            var end = DateTime.Now.AddDays(60);

            var filterBuilder = Builders<Cliente>.Filter;

            var filter = filterBuilder.Gte(c => c.Criado, start) & filterBuilder.Lte(c => c.Atualizado, end);

            var query = _clientesCollection.Find(filter);
            var total = query.CountDocuments();
            return new PagedResult<Cliente>()
            {
                List = await query.Skip((pageIndex - 1) * pageSize).Limit(pageSize).ToListAsync(),
                TotalResults = total,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
        }

        public Cliente GetById(string id)
        {
            return _clientesCollection.Find(cliente => cliente.Id == id).FirstOrDefault();
        }

        public Cliente Create(Cliente cliente)
        {
            cliente.Atualizado = DateTime.Now;
            cliente.Criado = new DateTime(2021, 2, 24);
            _clientesCollection.InsertOne(cliente);
            return cliente;
        }

        public void Update(string id, Cliente cliente)
        {
            cliente.Atualizado = DateTime.Now;
            _clientesCollection.ReplaceOne(c => c.Id == id, cliente);
        }

        public void Delete(Cliente cliente)
        {
            _clientesCollection.DeleteOne(c => c.Id == cliente.Id);
        }
    }
}
