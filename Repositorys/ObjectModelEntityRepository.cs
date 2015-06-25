using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace appPDU.Models
{
    public class ObjectModelEnityRepository : IObjectModelRepository
    {
        public Task AddAsync(IObjectModel model)
        {
            throw new NotImplementedException();
        }

        public Task AddManyAsync(IList<IObjectModel> models)
        {
            throw new NotImplementedException();
        }

        public Task<List<IObjectModel>> AllModelsAsync()
        {
            return null;
        }

        public Task<List<IObjectModel>> AllModelsByTypeAsync(int type)
        {
            return null;
        }

        public Task<IObjectModel> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IList<IObjectModel>> GetByIdsAsync(IList<Guid> ids)
        {
            throw new NotImplementedException();
        }

        public Task<IObjectModel> GetByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<bool> TryDeleteAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> TryUpdateAsync(IObjectModel model)
        {
            throw new NotImplementedException();
        }
    }
}