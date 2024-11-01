﻿

using BookingService.Domain.Models.Dto;
using BookingService.Domain.Models;
using ShareDTO;
using BookingService.Domain.Interfaces;

namespace BookingService.Application.Services
{
    public class AvailableSlotsService : IAvailableSlotsService
    {
        ICatalogServiceClient _catalogServiceClient { get; set; }
        IBookingService _bookingService { get; set; }

        public AvailableSlotsService(ICatalogServiceClient catalogService, IBookingService bookingService) 
        { 
            _catalogServiceClient = catalogService;
            _bookingService = bookingService;   
        }


        public async Task<List<AvailableDayDto>> CalculateAvailableSlots(Guid organizationServiceId)
        {
            DateTime startDate = DateTime.UtcNow; // Начало текущего времени
            DateTime endDate = startDate.AddDays(7); // Конец через неделю

            // вынести это в слой Application

            // Шаг 1: Запросить расписание и часы работы из CatalogService
            var workingHours = await _catalogServiceClient.GetWorkingHours(organizationServiceId);

            // Шаг 2: Получить существующие брони
            var existingBookings = await _bookingService.GetBookings(organizationServiceId, startDate, endDate);

            // Шаг 3: Сформировать список доступных временных слотов
            var availableSlots = CalculateAvailableSlotsHandle(workingHours, existingBookings);

            return availableSlots;
        }

        public List<AvailableDayDto> CalculateAvailableSlotsHandle(List<WorkingHoursDto> workingHours, List<Booking> existingBookings)
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
    }
}