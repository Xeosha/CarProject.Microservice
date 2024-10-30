using CatalogService.Domain.Interfaces;
using CatalogService.Domain.Interfaces.Models;
using CatalogService.Domain.Interfaces.Repositories;
using ShareDTO;

namespace CatalogService.Application.Servicess
{
    public class CatalogServices : ICatalogServices
    {
        private IServiceRepository _serviceRepository;

        private IServiceOrgRepository _serviceOrgRepository;

        private IDailyWorkingHoursRepository _dailyWorkingHoursRepository;

        public CatalogServices(IServiceRepository serviceRepository, IServiceOrgRepository serviceOrgRepository, IDailyWorkingHoursRepository dailyWorkingHoursRepository)
        {
            _serviceRepository = serviceRepository;
            _serviceOrgRepository = serviceOrgRepository;
            _dailyWorkingHoursRepository = dailyWorkingHoursRepository;
        }

        public async Task<List<ServiceOrg>> GetAllServices()
        {
            return await _serviceOrgRepository.GetAll();
        }

        public async Task<List<WorkingHoursDto>> GetWorkingHours(Guid organizationServiceId)
        {
            return await _dailyWorkingHoursRepository.GetAllTimes(organizationServiceId);
        }
    }
}
