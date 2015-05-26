using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace appPDU.Models
{
	public interface IObjectModelRepository
	{
        Task<List<ObjectModel>> AllModelsAsync();
        Task AddAsync(ObjectModel model);
		Task<ObjectModel> GetByIdAsync(Guid id);
		Task<IList<ObjectModel>> GetByIdsAsync(IList<Guid> ids);
		Task<ObjectModel> GetByNameAsync(string name);
		Task<bool> TryDeleteAsync(Guid Id);
	}
}