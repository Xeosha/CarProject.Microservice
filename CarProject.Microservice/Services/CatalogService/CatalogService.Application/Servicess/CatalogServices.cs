using CatalogService.Domain.Interfaces;
using CatalogService.Domain.Interfaces.Models;
using CatalogService.Domain.Interfaces.Repositories;

namespace CatalogService.Application.Servicess
{
    public class CatalogServices : ICatalogServices
    {
        private IServiceRepository _serviceRepository;

        private IServiceOrgRepository _serviceOrgRepository;

        public CatalogServices(IServiceRepository serviceRepository, IServiceOrgRepository serviceOrgRepository)
        {
            _serviceRepository = serviceRepository;
            _serviceOrgRepository = serviceOrgRepository;
        }

        public async Task<List<ServiceOrg>> GetAllServices()
        {
            return await _serviceOrgRepository.GetAll();
        }
    }
}
