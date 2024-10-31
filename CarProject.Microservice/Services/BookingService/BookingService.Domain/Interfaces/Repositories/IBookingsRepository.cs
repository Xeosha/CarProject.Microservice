

using BookingService.Domain.Models;

namespace BookingService.Domain.Interfaces.Repositories
{
    public interface IBookingsRepository
    {
        public Task<List<Booking>> GetAll(Guid organizationServiceId, DateTime startDate, DateTime endDate);
        public Task<List<Booking>> GetForUser(Guid userId);
        public Task<List<Booking>> GetForOrg(Guid organizationId);
        public Task<Booking?> GetById(Guid bookingId);
        public Task Add(Booking booking);
        public Task Update(Booking booking);
    }
}
