using BookingService.Domain.Interfaces;
using BookingService.Domain.Models;

namespace BookingService.Application.Services
{
    public class BookingsService : IBookingService
    {
        //private readonly IBookingRepository _repository;

        public async Task<Booking> GetBookingById(Guid bookingId)
        {
            return new Booking();
        }

        public async Task<Booking> CreateBooking(Booking request)
        {
            // Здесь логика сохранения в БД и генерации BookingId
            

            return new Booking();
        }

        public async Task<bool> ConfirmBooking(Guid bookingId, bool isConfirmed)
        {
            // Логика обновления статуса в базе данных

            string status = isConfirmed ? "Confirmed" : "Declined";

            // Уведомить пользователя


            return isConfirmed;
        }
    }
}
