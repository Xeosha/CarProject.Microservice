
using CatalogService.Domain.Interfaces.Models;
using CatalogService.Domain.Interfaces.Repositories;


namespace CatalogService.Infrastructure.Repositories
{
    public class OrganizationRepository : IOrganizationsRepository
    {
        private readonly CatalogServiceDbContext _context;

        public OrganizationRepository(CatalogServiceDbContext context)
        {
            _context = context;
        }

        public async Task<Organization> GetById(Guid id)
        {
            var entity = await _context.Organizations.FindAsync(id);

            return new Organization()
            {
                Id = entity.Id,
                Name = entity.Name,
                Location = entity.Location,
                Description = entity.Description,
            };
        }
    }
}
