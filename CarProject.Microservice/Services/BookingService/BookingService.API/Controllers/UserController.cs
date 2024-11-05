using BookingService.API.Hubs;
using BookingService.Application.Services;
using BookingService.Domain.Interfaces;
using BookingService.Domain.Models;
using BookingService.Domain.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace BookingService.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly ILogger<UserController> _logger;
        private readonly IAvailableSlotsService _availableSlotsService;

        public UserController(
            IBookingService bookingService, 
            ILogger<UserController> logger,
            IAvailableSlotsService availableSlotsService)
        {
            _bookingService = bookingService;
            _logger = logger;
            _availableSlotsService = availableSlotsService;
        }

        [HttpGet("getAllBookings")]
        public async Task<List<BookingDto>> GetAllBookings(Guid userId)
        {
            return await _bookingService.GetBookingsForUser(userId);
        }

        [HttpGet("getAvailableTimes")]
        public async Task<List<AvailableDayDto>> GetAvailableTimes([FromQuery] Guid organizationServiceId)
        {
        
            var availableSlots = await _availableSlotsService.CalculateAvailableSlots(organizationServiceId);


            return availableSlots;  
        }
    }
}
