
using CatalogService.Domain.Interfaces.Models;

namespace CatalogService.Domain.Interfaces.Repositories
{
    public interface IOrganizationsRepository
    {
        public Task<Organization> GetById(Guid id);
    }
}
