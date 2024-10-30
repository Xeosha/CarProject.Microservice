using BookingService.Domain.Interfaces;
using BookingService.Domain.Models;
using BookingService.Domain.Models.Dto;

namespace BookingService.Application.Services
{
    public class BookingsService : IBookingService
    { 
        public async Task<Booking> GetBookingById(Guid bookingId)
        {
            return new Booking();
        }

        public async Task CreateBooking(Guid bookingId, Guid userId, Guid organizationId, string service)
        {
            var booking = GetBookingById(bookingId);

            if (booking != null)
            {
                return;
            }

            return;
        }

        public async Task<bool> ConfirmBooking(Guid bookingId, bool isConfirmed)
        {
            // Логика обновления статуса в базе данных

            string status = isConfirmed ? "Confirmed" : "Declined";

            // Уведомить пользователя


            return isConfirmed;
        }

        public async Task<WorkingHoursDto> CalculateAvailableSlots(WorkingHoursDto workingHours, List<Booking> existingBookings)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Booking>> GetBookings(Guid organizationServiceId, DateTime date)
        {
            throw new NotImplementedException();
        }
    }
}
