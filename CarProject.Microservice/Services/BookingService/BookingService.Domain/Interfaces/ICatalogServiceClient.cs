
using ShareDTO;

namespace BookingService.Domain.Interfaces
{
    public interface ICatalogServiceClient
    {
        Task<List<WorkingHoursDto>> GetWorkingHours(Guid organizationServiceId);

        Task<List<ServiceOrgDto>> GetServiceOrgIdsForOrganization(Guid organizationServiceId);

        Task<List<ServiceOrgDto>> GetServicesWithServiceOrgId(List<Guid> serviceOrgIds);

    }
}
