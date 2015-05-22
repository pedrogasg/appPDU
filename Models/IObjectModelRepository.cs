using System;
using System.Collections.Generic;

namespace appPDU.Models
{
	public interface IObjectModelRepository
	{
		IEnumerable<ObjectModel> AllModels{get;}
		void Add(ObjectModel model);
		ObjectModel GetById(Guid Id);
		
		bool TryDelete(Guid Id);
	}
}