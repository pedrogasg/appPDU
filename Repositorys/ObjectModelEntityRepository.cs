using appPDU.DataLayer;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Data.Entity;

namespace appPDU.Models
{
    public class ObjectModelEntityRepository:IObjectModelRepository<IObjectModel>
    {
        private readonly ObjectModelDbContext _dbContext;
        public ObjectModelEntityRepository(ObjectModelDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(IObjectModel model)
        {
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
            Func<IQueryable<IObjectModel>, IOrderedQueryable<IObjectModel>> orderBy = null,bool includeData = false)
        {
            IQueryable<IObjectModel> models = _dbContext.ObjectModels;
            if(filter != null)
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
            return  await _dbContext.ObjectModels.Where(o => o.Type == type).ToListAsync<IObjectModel>();
        }

        public async Task<IObjectModel> GetByIdAsync(Guid id)
        {
            return await _dbContext.ObjectModels.SingleAsync(o => o.Id == id);
        }

        public async Task<IList<IObjectModel>> GetByIdsAsync(IList<Guid> ids)
        {
            return await _dbContext.ObjectModels.Where(o => ids.Contains(o.Id)).ToListAsync<IObjectModel>();
        }

        public async Task<IObjectModel> GetByNameAsync(string name)
        {
            return await _dbContext.ObjectModels.SingleAsync(o => o.Name == name);
        }

        public Task<bool> TryDeleteAsync(Guid Id)
        {
             //_dbContext.ObjectModels.Remove()
            throw new NotImplementedException();
        }

        public Task<bool> TryUpdateAsync(IObjectModel model)
        {
            throw new NotImplementedException();
        }
    }
}