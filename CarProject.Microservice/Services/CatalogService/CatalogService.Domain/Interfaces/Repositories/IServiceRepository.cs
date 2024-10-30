using CatalogService.Domain.Interfaces.Models;

namespace CatalogService.Domain.Interfaces.Repositories
{
    public interface IServiceRepository
    {
        public Task Add(ServiceModel serviceModel);
        public Task Delete(Guid id);

        public Task Update(Guid id, ServiceModel serviceModel);
        public Task<List<ServiceModel>> GetAll();

        public Task<ServiceModel> GetById(Guid id); 
    }
}
