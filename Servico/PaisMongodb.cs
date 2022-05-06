using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using webapi;
using webapi.Models;

namespace webapi_pais.Servico
{
    public class PaisMongodb
    {
        private IMongoDatabase mongoDatabase;

        public PaisMongodb()
        {
            mongoDatabase =  new MongoClient(Program.MongoConn).GetDatabase(Program.MongodbName);
        }

        private IMongoCollection<Pai> mongoCollection => mongoDatabase.GetCollection<Pai>("pais");

        public async void Inserir(Pai pai)
        {
            await mongoCollection.InsertOneAsync(pai);
            // IMongoCollection<Pai> paisDB = mongoDatabase.GetCollection<Pai>("pais").InsertOneAsync(pai);
            // paisDB.InsertOne(pai);
        
        }
        public async void Atualizar(Pai pai)
        {
            await mongoCollection.UpdateOneAsync(x=> x.Id == pai.Id, new ObjectUpdateDefinition<Pai>(pai));
            // IMongoCollection<Pai> paisDB = mongoDatabase.GetCollection<Pai>("pais").InsertOneAsync(pai);
            // paisDB.InsertOne(pai);
        }

        public async Task<IList<Pai>> Todos()
        {
            return await mongoCollection.AsQueryable().ToListAsync();
        }

        public async Task<Pai> BuscarPorId(ObjectId id)
        {
            return await mongoCollection.AsQueryable().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async void RemoverPorId(ObjectId id)
        {
            await mongoCollection.DeleteOneAsync(x => x.Id == id);
        }
    }
}