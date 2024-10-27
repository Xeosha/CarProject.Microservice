
using BookingService.Domain.Models;

namespace BookingService.Domain.Interfaces
{
    public interface IBookingService
    {
        public Task CreateBooking(int bookingId, int userId, int organizationId, string service);
        public Task<bool> ConfirmBooking(int bookingId, bool isConfirmed);
        public Task<Booking> GetBookingById(int bookingId);
    }
}
