using CatalogService.Domain.Interfaces.Models;

namespace CatalogService.Domain.Interfaces.Repositories
{
    public interface IServiceOrgRepository
    {
        public Task Add(ServiceOrg serviceModel);
        public Task Delete(Guid id);

        public Task Update(Guid id, ServiceOrg serviceModel);
        public Task<List<ServiceOrg>> GetAll();
        public Task<List<ServiceOrg>> GetOrg(Guid id);

    }
}
