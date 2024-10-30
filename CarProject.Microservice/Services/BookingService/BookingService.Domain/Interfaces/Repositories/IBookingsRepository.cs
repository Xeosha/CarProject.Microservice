

using BookingService.Domain.Models;

namespace BookingService.Domain.Interfaces.Repositories
{
    public interface IBookingsRepository
    {
        public Task<List<Booking>> GetBookingsAsync(Guid organizationServiceId, DateTime startDate, DateTime endDate);
        public Task<Booking> AddBookingAsync(Booking booking);
    }
}
