using CatalogService.Domain.Interfaces.Models;
using CatalogService.Domain.Interfaces.Repositories;
using CatalogService.Infrastructure.Entites;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Infrastructure.Repositories
{
    public class ServiceOrgRepository : IServiceOrgRepository
    {
        private readonly CatalogServiceDbContext _context;

        public ServiceOrgRepository(CatalogServiceDbContext context)
        {
            _context = context;
        }

        public async Task Add(ServiceOrg serviceModel)
        {
            // Преобразуйте модель в сущность
            var serviceOrgEntity = new ServiceOrganizationEntity
            {
                // Присвойте свойства из serviceModel
                Description = serviceModel.Description,
                // Присвойте остальные свойства
            };

            await _context.ServiceOrganizations.AddAsync(serviceOrgEntity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(long id)
        {
            var serviceOrg = await _context.ServiceOrganizations.FindAsync(id);
            if (serviceOrg != null)
            {
                _context.ServiceOrganizations.Remove(serviceOrg);
                await _context.SaveChangesAsync();
            }
        }

        public async Task Update(long id, ServiceOrg serviceModel)
        {
            var existingServiceOrg = await _context.ServiceOrganizations.FindAsync(id);
            if (existingServiceOrg != null)
            {
                existingServiceOrg.Description = serviceModel.Description; // пример свойства
                                                                           // обновите остальные свойства по мере необходимости

                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<ServiceOrg>> GetAll()
        {
            // Преобразуем сущности обратно в модели
            return await _context.ServiceOrganizations
                .Select(e => new ServiceOrg
                {
                    Id = e.Id,
                    Description = e.Description,
                    // присвойте остальные свойства
                })
                .ToListAsync();
        }
    }
}
