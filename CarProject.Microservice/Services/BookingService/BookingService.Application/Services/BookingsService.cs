using BookingService.Domain.Interfaces;
using BookingService.Domain.Models;

namespace BookingService.Application.Services
{
    public class BookingsService : IBookingService
    {
        //private readonly IBookingRepository _repository;

        public async Task<Booking> GetBookingById(int bookingId)
        {
            return new Booking();
        }

        public async Task CreateBooking(int bookingId, int userId, int organizationId, string service)
        {
            var booking = GetBookingById(bookingId);

            if (booking != null)
            {
                return;
            }

            return;
        }

        public async Task<bool> ConfirmBooking(int bookingId, bool isConfirmed)
        {
            // Логика обновления статуса в базе данных

            string status = isConfirmed ? "Confirmed" : "Declined";

            // Уведомить пользователя


            return isConfirmed;
        }
    }
}
