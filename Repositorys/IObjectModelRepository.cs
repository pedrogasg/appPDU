using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace appPDU.Models
{
	public interface IObjectModelRepository
	{
        Task<List<IObjectModel>> AllModelsAsync();
        Task<List<IObjectModel>> AllModelsByTypeAsync(int type);
        Task AddAsync(IObjectModel model);
        Task AddManyAsync(IList<IObjectModel> models);
        Task<IObjectModel> GetByIdAsync(Guid id);
		Task<IList<IObjectModel>> GetByIdsAsync(IList<Guid> ids);
		Task<IObjectModel> GetByNameAsync(string name);
        Task<bool> TryUpdateAsync(IObjectModel model);
        Task<bool> TryDeleteAsync(Guid Id);
    }
}