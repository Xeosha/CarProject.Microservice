

using BookingService.Domain.Models.Dto;

namespace BookingService.Domain.Interfaces
{
    public interface ICatalogServiceClient
    {
        Task<WorkingHoursDto> GetWorkingHours(Guid organizationServiceId);
    }
}
