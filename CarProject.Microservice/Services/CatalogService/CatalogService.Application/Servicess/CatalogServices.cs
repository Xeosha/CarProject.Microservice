﻿using CatalogService.Domain.Interfaces;
using CatalogService.Domain.Interfaces.Models;
using CatalogService.Domain.Interfaces.Repositories;
using ShareDTO;

namespace CatalogService.Application.Servicess
{
    public class CatalogServices : ICatalogServices
    {
        private IServiceRepository _serviceRepository;
        private IServiceOrgRepository _serviceOrgRepository;
        //private IDailyWorkingHoursRepository _dailyWorkingHoursRepository;
        private IOrganizationsRepository _organizationsRepository;


        public CatalogServices(IServiceRepository serviceRepository, 
            IServiceOrgRepository serviceOrgRepository, 
           //IDailyWorkingHoursRepository dailyWorkingHoursRepository,
            IOrganizationsRepository organizationsRepository
            )
        {
            _serviceRepository = serviceRepository;
            _serviceOrgRepository = serviceOrgRepository;
            //_dailyWorkingHoursRepository = dailyWorkingHoursRepository;
            _organizationsRepository = organizationsRepository;
        }

        //public async Task<List<WorkingHoursDto>> GetWorkingHours(Guid organizationServiceId)
       // {
        //    return await _dailyWorkingHoursRepository.GetAllTimes(organizationServiceId);
        //}

        public async Task AddServiceToOrg(Guid orgId, Guid serviceId, int price, string description)
        {
            var model = new ServiceOrg()
            {
                Id = new Guid(),
                IdOrganization = orgId,
                IdService = serviceId,
                Price = price,
                Description = description
            };
            await _serviceOrgRepository.Add(model);
        }
        public async Task<List<ServiceOrgDto>> GetAllServices()
        {
            // Получаем все записи ServiceOrg
            var serviceOrgs = await _serviceOrgRepository.GetAll();

            // Заполняем DTO с дополнительными данными
            var serviceOrgDtos = new List<ServiceOrgDto>();
            foreach (var serviceOrg in serviceOrgs)
            {
                var organization = await _organizationsRepository.GetById(serviceOrg.IdOrganization);
                var service = await _serviceRepository.GetById(serviceOrg.IdService);

                serviceOrgDtos.Add(new ServiceOrgDto
                {
                    Id = serviceOrg.Id,
                    IdOrganization = serviceOrg.IdOrganization,
                    OrganizationName = organization?.Name ?? string.Empty,
                    location = organization.Location,
                    IdService = serviceOrg.IdService,
                    ServiceName = service?.Name ?? string.Empty,
                    Price = serviceOrg.Price,
                    Description = serviceOrg.Description
                });
            }

            return serviceOrgDtos;
        }


        public async Task<List<ServiceOrg>> GetOrgServices(Guid organizationId)
        {
            return await _serviceOrgRepository.GetOrg(organizationId);
        }

        public async Task UpdateService(Guid serviceOrgId, Guid orgId, Guid serviceId, int price, string description)
        {
            var model = new ServiceOrg()
            {
                Id = serviceOrgId,
                IdOrganization = orgId,
                IdService = serviceId,
                Price = price,
                Description = description
            };
            await _serviceOrgRepository.Update(serviceOrgId, model);
        }

        public async Task DeleteService(Guid serviceOrgId)
        {
            await _serviceOrgRepository.Delete(serviceOrgId);
        }
        public async Task<List<ServiceModel>> GetNameServices()
        {
            return await _serviceRepository.GetAll();
        }
    }
}
