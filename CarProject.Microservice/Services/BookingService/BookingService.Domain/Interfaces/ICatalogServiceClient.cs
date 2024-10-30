
using ShareDTO;

namespace BookingService.Domain.Interfaces
{
    public interface ICatalogServiceClient
    {
        Task<List<WorkingHoursDto>> GetWorkingHours(Guid organizationServiceId);
    }
}
