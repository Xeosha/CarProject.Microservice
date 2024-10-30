

using BookingService.Infrastracture.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Infrastracture
{
    public class BookingServiceDbContext : DbContext
    {
        public BookingServiceDbContext(DbContextOptions<BookingServiceDbContext> options)
            : base(options)
        {
        }

        public DbSet<BookingEntity> Bookings { get; set; }
    }
}
