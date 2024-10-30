
using BookingService.Domain.Models;
using BookingService.Domain.Models.Dto;

namespace BookingService.Domain.Interfaces
{
    public interface IBookingService
    {
        public Task CreateBooking(Guid bookingId, Guid userId, Guid organizationId, string service);
        public Task<bool> ConfirmBooking(Guid bookingId, bool isConfirmed);
        public Task<Booking> GetBookingById(Guid bookingId);
        public Task<WorkingHoursDto> CalculateAvailableSlots(WorkingHoursDto workingHours, List<Booking> existingBookings);
        public Task<List<Booking>> GetBookings(Guid organizationServiceId, DateTime date);
    }
}
