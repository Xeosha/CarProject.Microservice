using BookingService.Domain.Models.Dto;

namespace BookingService.Domain.Interfaces
{
    public interface IAvailableSlotsService
    {
        public Task<List<AvailableDayDto>> CalculateAvailableSlots(Guid organizationServiceId);
    }
}
