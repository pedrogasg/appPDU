using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;

namespace appPDU.Models
{
	public interface IObjectModelRepository<TObjectModel> where TObjectModel : class, IObjectModel
	{
        Task<List<TObjectModel>> AllModelsAsync(Expression<Func<TObjectModel, bool>> filter = null,
            Func<IQueryable<TObjectModel>, IOrderedQueryable<TObjectModel>> orderBy = null, bool includeData = false);
        Task<List<TObjectModel>> AllModelsByTypeAsync(int type);
        Task AddAsync(TObjectModel model);
        Task AddManyAsync(IList<TObjectModel> models);
        Task AddSuccessors(IObjectModel model, IList<IObjectModel> successors);
        Task<TObjectModel> GetByIdAsync(Guid id);
		Task<IList<TObjectModel>> GetByIdsAsync(IList<Guid> ids);
		Task<TObjectModel> GetByNameAsync(string name);
        Task<bool> TryUpdateAsync(TObjectModel model);
        Task<bool> TryDeleteAsync(Guid Id);
    }
}