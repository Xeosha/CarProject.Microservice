

using CatalogService.Domain.Interfaces.Models;

namespace CatalogService.Domain.Interfaces
{
    public interface ICatalogServices
    {
        public Task<List<ServiceOrg>> GetAllServices();
    }
}
