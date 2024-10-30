using BookingService.API.Hubs;
using BookingService.Application.Services;
using BookingService.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace BookingService.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ICatalogServiceClient _catalogServiceClient;
        private readonly IBookingService _bookingService;
        private readonly ILogger<UserController> _logger;

        public UserController(
            IBookingService bookingService, 
            ILogger<UserController> logger,
            ICatalogServiceClient catalogServiceClient
            )
        {
            _bookingService = bookingService;
            _logger = logger;
            _catalogServiceClient = catalogServiceClient;
        }

        [HttpPost("request")]
        public async Task<IActionResult> RequestBooking()
        {
            
            return StatusCode(500, "Failed to create booking."); 
        }

        [HttpGet("available-times")]
        public async Task<IActionResult> GetAvailableTimes([FromQuery] Guid organizationServiceId, DateTime date)
        {
            // Шаг 1: Запросить расписание и часы работы из CatalogService
            var workingHours = await _catalogServiceClient.GetWorkingHours(organizationServiceId);

            // Шаг 2: Получить существующие брони
            var existingBookings = await _bookingService.GetBookings(organizationServiceId, date);

            // Шаг 3: Сформировать список доступных временных слотов
            var availableSlots = _bookingService.CalculateAvailableSlots(workingHours, existingBookings);

            return Ok(availableSlots);
        }
    }
}
