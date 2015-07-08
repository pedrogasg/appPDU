using appPDU.DataLayer;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Data.Entity;

namespace appPDU.Models
{
    public class ObjectModelEntityRepository : IObjectModelRepository<IObjectModel>
    {
        private readonly ObjectModelDbContext _dbContext;

        public ObjectModelEntityRepository(ObjectModelDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(IObjectModel model)
        {
            model.DateCreate = null;
            _dbContext.Add(model);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddManyAsync(IList<IObjectModel> models)
        {
            foreach (var model in models)
            {
                _dbContext.Add(model);

            }
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<IObjectModel>> AllModelsAsync(Expression<Func<IObjectModel, bool>> filter = null,
            Func<IQueryable<IObjectModel>, IOrderedQueryable<IObjectModel>> orderBy = null, bool includeData = false)
        {
            IQueryable<IObjectModel> models = modelsQuery();
            if (filter != null)
            {
                models = models.Where(filter);
            }

            models.Include(o => o.Id);
            models.Include(o => o.Name);
            models.Include(o => o.Title);
            models.Include(o => o.Metadata);
            models.Include(o => o.DateCreate);
            models.Include(o => o.Type);
            models.Include(o => o.TypeName);
            models.Include(o => o.Order);

            if (includeData)
            {
                models.Include(o => o.Data);
            }

            if (orderBy != null)
            {
                models = orderBy(models);
            }
            return await models.ToListAsync<IObjectModel>();
        }

        public async Task<List<IObjectModel>> AllModelsByTypeAsync(int type)
        {
            return await modelsQuery().Where(o => o.Type == type).ToListAsync<IObjectModel>();
        }

        public async Task<IObjectModel> GetByIdAsync(Guid id)
        {
            return await modelsQuery().SingleAsync(o => o.Id == id);
        }

        public async Task<IList<IObjectModel>> GetByIdsAsync(IList<Guid> ids)
        {
            return await modelsQuery().Where(o => ids.Contains(o.Id)).ToListAsync<IObjectModel>();
        }

        public async Task<IObjectModel> GetByNameAsync(string name)
        {
            return await modelsQuery().SingleAsync(o => o.Name == name);
        }

        public async Task<bool> TryDeleteAsync(Guid id)
        {
            _dbContext.Remove(await modelsQuery().SingleAsync(o => o.Id == id));
            var index = await _dbContext.SaveChangesAsync();
            return index != -1;
        }

        public async Task<bool> TryUpdateAsync(IObjectModel model)
        {
            var changeTrack = _dbContext.Attach(model);
            changeTrack.State = EntityState.Modified;
            var index = await _dbContext.SaveChangesAsync();
            return index != -1;
        }
        private IQueryable<IObjectModel> modelsQuery()
        {
            return _dbContext.ObjectModels.AsNoTracking();
        }
    }
}