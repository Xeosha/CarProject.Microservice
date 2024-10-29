using CatalogService.Domain.Interfaces.Models;

namespace CatalogService.Domain.Interfaces.Repositories
{
    public interface IServiceRepository
    {
        public Task Add(ServiceModel serviceModel);
        public Task Delete(long id);

        public Task Update(long id, ServiceModel serviceModel);
        public Task<List<ServiceModel>> GetAll();
    }
}
