using System;
using System.Collections.Generic;
using System.Linq;
namespace appPDU.Models
{
	public class ObjectModelRepository:IObjectModelRepository
	{
        readonly List<ObjectModel> _models = new List<ObjectModel>();

        public IEnumerable<ObjectModel> AllModels
        {
            get
            {
                return _models;
            }
        }

        public ObjectModel GetById(Guid id)
        {
            return _models.FirstOrDefault(x => x.Id == id);
        }

        public void Add(ObjectModel item)
        {
            item.Id = Guid.NewGuid();
            _models.Add(item);
        }

        public bool TryDelete(Guid id)
        {
            var item = GetById(id);
            if (item == null)
            {
                return false;
            }
            _models.Remove(item);
            return true;
        }
	}
}