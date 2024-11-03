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
        public async Task<List<Booking>> GetAll(Guid organizationServiceId, DateTime startDate, DateTime endDate)
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

        public async Task<Booking?> GetById(Guid bookingId)
        {
            var bookingEntity = await _dbContext.Bookings.FindAsync(bookingId);

            if (bookingEntity == null)
            {
                return null;
            }

            return new Booking
            {
                BookingId = bookingEntity.Id,
                UserId = bookingEntity.UserId,
                ServiceOrganizationId = bookingEntity.ServiceOrganizationId,
                BookingTime = bookingEntity.DateTime,
                BookingStatus = bookingEntity.BookingStatus,
                Notes = bookingEntity.Description
            };
        }

        // Метод для добавления новой брони
        public async Task Add(Booking booking)
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
        }

        public async Task Update(Booking booking)
        {
            var bookingEntity = new BookingEntity()
            {
                Id = booking.BookingId,
                UserId = booking.UserId,
                ServiceOrganizationId = booking.ServiceOrganizationId,
                DateTime = booking.BookingTime,
                BookingStatus = booking.BookingStatus,
                Description = booking.Notes ?? string.Empty
            };

            _dbContext.Bookings.Update(bookingEntity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Booking>> GetForUser(Guid userId)
        {
            var bookings = await _dbContext.Bookings
                    .Where(b => b.UserId == userId)
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

        public async Task<List<Booking>> GetForOrg(List<Guid> serviceOrgIds)
        {
            var bookings = await _dbContext.Bookings
                .Where(b => serviceOrgIds.Contains(b.ServiceOrganizationId))
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
    }
}
