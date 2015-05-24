using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
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
        
        public Task<ObjectModel> GetByIdAsync(Guid id)
        {
            return Task.FromResult(GetById(id));
        }
        public IList<ObjectModel> GetByIds(IList<Guid> ids)
        {
            return _models.Where(x=>ids.Contains(x.Id)).ToList();
        }
        public Task<IList<ObjectModel>> GetByIdsASync(IList<Guid> ids)
        {
            return Task.FromResult(GetByIds(ids));
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