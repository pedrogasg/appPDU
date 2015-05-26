using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Framework.OptionsModel;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Builders;

namespace appPDU.Models
{
    public class ObjectModelRepository : IObjectModelRepository
    {
        readonly List<ObjectModel> _models = new List<ObjectModel>();
        private readonly Settings _settings;
        private readonly IMongoDatabase _database;
        public IMongoCollection<ObjectModel> _collection { get { return _database.GetCollection<ObjectModel>("objectModels"); } }
        public ObjectModelRepository(IOptions<Settings> settings)
        {
            _settings = settings.Options;
            _database = Connect();
        }

        public async Task<List<ObjectModel>> AllModelsAsync()
        {
           return await _collection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<ObjectModel> GetByIdAsync(Guid id)
        {
            var filter = Builders<ObjectModel>.Filter.Eq(e => e.Id, id);
            var result = await _collection.Find(filter).ToListAsync();
            return result.FirstOrDefault();
        }

        public async Task<IList<ObjectModel>> GetByIdsAsync(IList<Guid> ids)
        {
            var filter = Builders<ObjectModel>.Filter.Where(e => ids.Contains(e.Id));
            return await _collection.Find(filter).ToListAsync(); ;
        }

        public async Task<ObjectModel> GetByNameAsync(string name)
        {
            var filter = Builders<ObjectModel>.Filter.Eq(e => e.Name, name);
            var result = await _collection.Find(filter).ToListAsync();
            return result.FirstOrDefault();
        }

        async Task IObjectModelRepository.AddAsync(ObjectModel model)
        {
            await _collection.InsertOneAsync(model);
        }

        async Task<bool> IObjectModelRepository.TryDeleteAsync(Guid id)
        {
            var filter = Builders<ObjectModel>.Filter.Eq(e => e.Id, id);
            var result = await _collection.DeleteOneAsync(filter);
            return result.IsAcknowledged;
        }

        private IMongoDatabase Connect()
        {
            var client = new MongoClient(_settings.MongoConnection);
            return client.GetDatabase(_settings.Database);
        }


    }
}