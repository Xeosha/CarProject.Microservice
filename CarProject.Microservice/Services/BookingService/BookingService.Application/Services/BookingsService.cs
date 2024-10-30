using BookingService.Domain.Interfaces;
using BookingService.Domain.Interfaces.Repositories;
using BookingService.Domain.Models;
using BookingService.Domain.Models.Dto;
using ShareDTO;

namespace BookingService.Application.Services
{
    public class BookingsService : IBookingService
    { 
        IBookingsRepository _bookingsRepository { get; set; }    
        public BookingsService(IBookingsRepository bookingsRepository)
        {
            _bookingsRepository = bookingsRepository;
        }

        public async Task<Booking?> GetBookingById(Guid bookingId)
        {
            return await _bookingsRepository.GetById(bookingId);
        }

        public async Task<Booking> CreateBooking(Guid userId, Guid serviceOrganizationId, DateTime bookingTime)
        {
            var bookingId = Guid.NewGuid();

            // здесь проверить, что такого айди нет

            var booking = Booking.Create(bookingId, userId, serviceOrganizationId, bookingTime);

            await _bookingsRepository.Add(booking);

            return booking;
        }

        public async Task<Booking> ConfirmBooking(Guid bookingId, bool isConfirmed)
        {
            var booking = await GetBookingById(bookingId);

            booking.BookingStatus = BookingStatus.Confirmed;

            await _bookingsRepository.Update(booking);

            return booking;
        }

        public async Task<List<AvailableDayDto>> CalculateAvailableSlots(List<WorkingHoursDto> workingHours, List<Booking> existingBookings)
        {
            var availableDays = new List<AvailableDayDto>();

            // Группируем существующие бронирования по дню недели для удобства поиска
            var bookingsByDayOfWeek = existingBookings.GroupBy(b => b.BookingTime.Date).ToDictionary(g => g.Key, g => g.ToList());

            // Определяем текущую дату
            var startDate = DateTime.UtcNow;

            for (int i = 0; i < 7; i++)
            {
                // Определяем дату для текущего дня (сейчас + i дней)
                var currentDate = startDate.AddDays(i);
                var dayOfWeek = currentDate.DayOfWeek;

                // Находим рабочие часы для текущего дня
                var workingHourForDay = workingHours.FirstOrDefault(wh => wh.DayOfWeek == dayOfWeek);

                if (workingHourForDay != null)
                {
                    var availableDay = new AvailableDayDto
                    {
                        DayOfWeek = dayOfWeek,
                        Date = currentDate
                    };

                    foreach (var slot in workingHourForDay.TimeSlots)
                    {
                        // Проверяем, есть ли бронирование на текущий временной слот
                        var isBooked = bookingsByDayOfWeek.TryGetValue(currentDate.Date, out var dayBookings) &&
                                       dayBookings.Any(b => b.BookingTime.TimeOfDay >= slot.StartTime && b.BookingTime.TimeOfDay < slot.EndTime);

                        var availableSlot = new AvailableTimeSlotDto
                        {
                            StartTime = slot.StartTime,
                            EndTime = slot.EndTime,
                            IsBooked = isBooked
                        };

                        availableDay.TimeSlots.Add(availableSlot);
                    }

                    availableDays.Add(availableDay);
                }
            }

            return availableDays;
        }




        public async Task<List<Booking>> GetBookings(Guid organizationServiceId, DateTime start, DateTime end)
        {
            return await _bookingsRepository.GetAll(organizationServiceId, start, end);
        }
    }
}
