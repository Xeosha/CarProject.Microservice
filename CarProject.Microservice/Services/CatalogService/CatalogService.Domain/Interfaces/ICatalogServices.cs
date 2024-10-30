

using CatalogService.Domain.Interfaces.Models;
using CatalogService.Domain.Interfaces.Models.Dto;

namespace CatalogService.Domain.Interfaces
{
    public interface ICatalogServices
    {
        public Task<List<WorkingHoursDto>> GetWorkingHours(Guid organizationServiceId);
        public Task<List<ServiceOrg>> GetAllServices();
    }
}
