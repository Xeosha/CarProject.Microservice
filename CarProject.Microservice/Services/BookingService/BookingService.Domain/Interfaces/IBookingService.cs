
using BookingService.Domain.Models;

namespace BookingService.Domain.Interfaces
{
    public interface IBookingService
    {
        public Task<Booking> CreateBooking(Booking request);
        public Task<bool> ConfirmBooking(Guid bookingId, bool isConfirmed);
        public Task<Booking> GetBookingById(Guid bookingId);
    }
}
