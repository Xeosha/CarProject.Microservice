using BookingService.Domain.Interfaces.Repositories;
using BookingService.Domain.Models;
using BookingService.Infrastracture.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Infrastracture.Repositories
{
    public class BookingsRepository : IBookingsRepository
    {
        private readonly BookingServiceDbContext _dbContext; // Ваш контекст базы данных

        public BookingsRepository(BookingServiceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Метод для получения бронирований за определенный диапазон дат
        public async Task<List<Booking>> GetBookingsAsync(Guid organizationServiceId, DateTime startDate, DateTime endDate)
        {
            var bookings = await _dbContext.Bookings
                        .Where(b =>
                                b.ServiceOrganizationId == organizationServiceId &&
                                b.DateTime >= startDate &&
                                b.DateTime <= endDate
                            )
            .ToListAsync();

            return bookings.Select(b => new Booking
            {
                BookingId = b.Id,
                UserId = b.UserId,
                ServiceOrganizationId = b.ServiceOrganizationId,
                BookingTime = b.DateTime,
                BookingStatus = b.BookingStatus,
                Notes = b.Description
            }).ToList();
        }

        // Метод для добавления новой брони
        public async Task<Booking> AddBookingAsync(Booking booking)
        {
            var bookingEntity = new BookingEntity
            {
                Id = booking.BookingId, // Уникальный идентификатор для новой записи
                UserId = booking.UserId,
                ServiceOrganizationId = booking.ServiceOrganizationId,
                DateTime = booking.BookingTime,
                BookingStatus = booking.BookingStatus,
                Description = booking.Notes
            };

            await _dbContext.Bookings.AddAsync(bookingEntity);
            await _dbContext.SaveChangesAsync(); // Сохранение изменений в базе данных

            return booking; // Вернуть созданную брони
        }
    }
}
