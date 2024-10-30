
using CatalogService.Domain.Interfaces.Models.Dto;
using ShareDTO;

namespace CatalogService.Domain.Interfaces
{
    public interface ICatalogServices
    {
        public Task<List<WorkingHoursDto>> GetWorkingHours(Guid organizationServiceId);
        public Task<List<ServiceOrgDto>> GetAllServices();
        public Task AddServiceToOrg(Guid orgId, Guid serviceId, int price, string description);

    }
}
