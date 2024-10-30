
using BookingService.Domain.Models;
using BookingService.Domain.Models.Dto;
using ShareDTO;

namespace BookingService.Domain.Interfaces
{
    public interface IBookingService
    {
        public Task<Guid> CreateBooking(Guid userId, Guid ServiceOrganizationId);
        public Task<bool> ConfirmBooking(Guid bookingId, bool isConfirmed);
        public Task<Booking> GetBookingById(Guid bookingId);
        public Task<List<AvailableDayDto>> CalculateAvailableSlots(List<WorkingHoursDto> workingHours, List<Booking> existingBookings);
        public Task<List<Booking>> GetBookings(Guid organizationServiceId, DateTime startDate, DateTime endDate);
    }
}
