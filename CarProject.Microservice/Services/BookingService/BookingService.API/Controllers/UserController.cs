using BookingService.API.Hubs;
using BookingService.Domain.Interfaces;
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
        private readonly IHubContext<NotificationBookingHub, IBookingClient> _hubContext;
        private readonly ILogger<UserController> _logger;

        public UserController(IBookingService bookingService, IHubContext<NotificationBookingHub, IBookingClient> hubContext, ILogger<UserController> logger)
        {
            _bookingService = bookingService;
            _hubContext = hubContext;
            _logger = logger;
        }

        [HttpPost("request")]
        public async Task<IActionResult> RequestBooking([FromBody] RequestBookingDto request)
        {
            // Проверка входных данных
            if (request == null || request.OrganizationId <= 0 || string.IsNullOrEmpty(request.Service))
            {
                return BadRequest("Invalid booking request.");
            }

            var userId = HttpContext.User.Identity.Name;  // Получаем userId из контекста

            _logger.LogInformation("RequestBooking for userId: {UserId}", userId);

            try
            {
                await _bookingService.CreateBooking(1, Int32.Parse(userId), request.OrganizationId, request.Service);
                await _hubContext.Clients.User(request.OrganizationId.ToString()).NotifyOrganization(Int32.Parse(userId), request.Service);

                _logger.LogInformation("Organization notified for booking request.");
                return Ok("Booking request sent.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create booking for user {UserId}", userId);
                return StatusCode(500, "Failed to create booking.");
            }
        }
    }
}
