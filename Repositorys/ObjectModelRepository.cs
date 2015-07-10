using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Framework.OptionsModel;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using System.Linq.Expressions;

namespace appPDU.Models
{
    public class ObjectModelRepository : IObjectModelRepository<IObjectModel>
    {
        readonly List<ObjectModel> _models = new List<ObjectModel>();
        private readonly Settings _settings;
        private readonly IMongoDatabase _database;
        public IMongoCollection<IObjectModel> _collection { get { return _database.GetCollection<IObjectModel>("objectModels"); } }
        public ObjectModelRepository(IOptions<Settings> settings)
        {
            var serializer = BsonSerializer.LookupSerializer<ObjectModel>();
            BsonSerializer.RegisterSerializer<IObjectModel>(new ImpliedImplementationInterfaceSerializer<IObjectModel, ObjectModel>(serializer));
            _settings = settings.Options;
            _database = Connect();
        }

        public async Task<List<IObjectModel>> AllModelsAsync()
        {
           return await _collection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<IObjectModel> GetByIdAsync(Guid id)
        {
            var filter = Builders<IObjectModel>.Filter.Eq(e => e.Id, id);
            var result = await _collection.Find(filter).ToListAsync();
            return result.FirstOrDefault();
        }

        public async Task<IList<IObjectModel>> GetByIdsAsync(IList<Guid> ids)
        {
            if(ids != null)
            {
                var filter = Builders<IObjectModel>.Filter.Where(e => ids.Contains(e.Id));
                var sort = Builders<IObjectModel>.Sort.Ascending(e => e.Order);
                return await _collection.Find(filter).Sort(sort).ToListAsync(); ;
            }
            return new List<IObjectModel>();
        }

        public async Task<IObjectModel> GetByNameAsync(string name)
        {
            var filter = Builders<IObjectModel>.Filter.Eq(e => e.Name, name);
            var result = await _collection.Find(filter).ToListAsync();
            return result.FirstOrDefault();
        }
        
        public async Task AddAsync(IObjectModel model)
        {
            await _collection.InsertOneAsync(model);
        }
        public async Task AddManyAsync(IList<IObjectModel> models)
        {
            await _collection.InsertManyAsync(models);
        }
        public async Task<bool> TryUpdateAsync(IObjectModel model)
        {
            var filter = Builders<IObjectModel>.Filter.Eq(e => e.Id, model.Id);
            var result = await _collection.ReplaceOneAsync(filter, model);
            return result.IsAcknowledged;
        }

        public async Task<bool> TryDeleteAsync(Guid id)
        {
            var filter = Builders<IObjectModel>.Filter.Eq(e => e.Id, id);
            var result = await _collection.DeleteOneAsync(filter);
            return result.IsAcknowledged;
        }

        private IMongoDatabase Connect()
        {
            var client = new MongoClient(_settings.MongoConnection);
            return client.GetDatabase(_settings.Database);
        }

        public async Task<List<IObjectModel>> AllModelsByTypeAsync(int type)
        {
            var filter = Builders<IObjectModel>.Filter.Eq(e => e.Type, type);
            return await _collection.Find(filter).ToListAsync();

        }

        public Task<List<IObjectModel>> AllModelsAsync(Expression<Func<IObjectModel, bool>> filter, Func<IQueryable<IObjectModel>, IOrderedQueryable<IObjectModel>> orderBy, bool includeDefaults)
        {
            throw new NotImplementedException();
        }

        public Task AddSuccessors(IObjectModel model, IList<IObjectModel> successors)
        {
            throw new NotImplementedException();
        }
    }
}