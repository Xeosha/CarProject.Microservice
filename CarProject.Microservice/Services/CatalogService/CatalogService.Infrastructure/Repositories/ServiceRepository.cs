

using CatalogService.Domain.Interfaces.Models;
using CatalogService.Domain.Interfaces.Repositories;
using CatalogService.Infrastructure.Entites;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Infrastructure.Repositories
{
    public class ServiceRepository : IServiceRepository
    {

        private readonly CatalogServiceDbContext _context;

        public ServiceRepository(CatalogServiceDbContext context)
        {
            _context = context;
        }

        public async Task Add(ServiceModel serviceModel)
        {
            // Преобразуйте модель в сущность
            var serviceEntity = new ServiceEntity
            {
                // Присвойте свойства из serviceModel
                Name = serviceModel.Name,
                Description = serviceModel.Description,
                // Присвойте остальные свойства
            };

            await _context.Services.AddAsync(serviceEntity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var service = await _context.Services.FindAsync(id);
            if (service != null)
            {
                _context.Services.Remove(service);
                await _context.SaveChangesAsync();
            }
        }

        public async Task Update(Guid id, ServiceModel serviceModel)
        {
            var existingService = await _context.Services.FindAsync(id);
            if (existingService != null)
            {
                existingService.Name = serviceModel.Name; // пример свойства
                existingService.Description = serviceModel.Description; // пример свойства
                                                                        // обновите остальные свойства по мере необходимости

                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<ServiceModel>> GetAll()
        {
            // Преобразуем сущности обратно в модели
            return await _context.Services
                .Select(e => new ServiceModel
                {
                    Id = e.Id,
                    Name = e.Name,
                    Description = e.Description,
                    // присвойте остальные свойства
                })
                .ToListAsync();
        }
    }
}
