﻿

using BookingService.Domain.Models;

namespace BookingService.Domain.Interfaces.Repositories
{
    public interface IBookingsRepository
    {
        public Task<List<Booking>> GetAll(Guid organizationServiceId, DateTime startDate, DateTime endDate);

        public Task<Booking?> GetById(Guid bookingId);
        public Task Add(Booking booking);
        public Task Update(Booking booking);
    }
}
