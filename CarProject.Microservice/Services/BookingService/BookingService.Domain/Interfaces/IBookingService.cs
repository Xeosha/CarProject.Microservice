
using BookingService.Domain.Models;
using BookingService.Domain.Models.Dto;
using ShareDTO;

namespace BookingService.Domain.Interfaces
{
    public interface IBookingService
    {
        public Task<Booking> CreateBooking(Guid userId, Guid ServiceOrganizationId, DateTime BookingTime, string notes);
        public Task<Booking> ConfirmBooking(Guid bookingId, bool isConfirmed);
        public Task<Booking?> GetBookingById(Guid bookingId);
        public Task<List<Booking>> GetBookings(Guid organizationServiceId, DateTime startDate, DateTime endDate);
        public Task<List<BookingDto>> GetBookingsForUser(Guid userId);
        public Task<List<BookingDto>> GetBookingsForOrg(Guid orgId);
    }
}
