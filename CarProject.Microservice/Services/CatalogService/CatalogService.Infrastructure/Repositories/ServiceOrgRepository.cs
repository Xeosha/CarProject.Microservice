﻿using CatalogService.Domain.Interfaces.Models;
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
                Id = serviceModel.Id,
                IdOrganization = serviceModel.IdOrganization,
                IdService = serviceModel.IdService,
                Price = serviceModel.Price, 
                Description = serviceModel.Description,
                // Присвойте остальные свойства
            };

            await _context.ServiceOrganizations.AddAsync(serviceOrgEntity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var serviceOrg = await _context.ServiceOrganizations.FindAsync(id);
            if (serviceOrg != null)
            {
                _context.ServiceOrganizations.Remove(serviceOrg);
                await _context.SaveChangesAsync();
            }
        }

        public async Task Update(Guid id, ServiceOrg serviceModel)
        {
            var existingServiceOrg = await _context.ServiceOrganizations.FindAsync(id);
            if (existingServiceOrg != null)
            {
                existingServiceOrg.IdService = serviceModel.IdService;
                existingServiceOrg.Price = serviceModel.Price;
                existingServiceOrg.Description = serviceModel.Description;
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
                    IdOrganization = e.IdOrganization,
                    IdService = e.IdService,
                    Price = e.Price,
                    Description = e.Description,
                })
                .ToListAsync();
        }

        public async Task<List<ServiceOrg>> GetOrg(Guid orgId)
        {
            // Преобразуем сущности обратно в модели
            return await _context.ServiceOrganizations
                .Select(e => new ServiceOrg
                {
                    Id = e.Id,
                    IdOrganization = e.IdOrganization,
                    IdService = e.IdService,
                    Price = e.Price,
                    Description = e.Description,
                })
                .Where(b => b.IdOrganization == orgId)
                .ToListAsync();
        }
    }
}
