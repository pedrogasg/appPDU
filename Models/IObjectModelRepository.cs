using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace appPDU.Models
{
	public interface IObjectModelRepository
	{
		IEnumerable<ObjectModel> AllModels{get;}
		void Add(ObjectModel model);
		ObjectModel GetById(Guid Id);
		Task<ObjectModel> GetByIdAsync(Guid id);
		IList<ObjectModel> GetByIds(IList<Guid> ids);
		Task<IList<ObjectModel>> GetByIdsASync(IList<Guid> ids);
		bool TryDelete(Guid Id);
	}
}