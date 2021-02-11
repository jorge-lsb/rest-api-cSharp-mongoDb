using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using webapi.mongodb.Data.Config;
using webapi.mongodb.Entitys;

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

        public List<Cliente> Get()
        {

            return _clientesCollection.Find(cliente => true).ToList();
        }

        public Cliente GetById(string id)
        {
            return _clientesCollection.Find(cliente => cliente.Id == id).FirstOrDefault();
        }

        public Cliente Create(Cliente cliente)
        {
            _clientesCollection.InsertOne(cliente);
            return cliente;
        }

        public void Update(string id, Cliente cliente)
        {
            _clientesCollection.ReplaceOne(c => c.Id == id, cliente);
        }

        public void Delete(Cliente cliente)
        {
            _clientesCollection.DeleteOne(c => c.Id == cliente.Id);
        }
    }
}
