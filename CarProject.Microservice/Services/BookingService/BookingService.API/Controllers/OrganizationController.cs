using BookingService.API.Hubs;
using BookingService.Domain.Interfaces;
using BookingService.Domain.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace BookingService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrganizationController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly IHubContext<NotificationBookingHub, IBookingClient> _hubContext;
        private readonly ILogger<OrganizationController> _logger;

        public OrganizationController(IBookingService bookingService, IHubContext<NotificationBookingHub, IBookingClient> hubContext, ILogger<OrganizationController> logger)
        {
            _bookingService = bookingService;
            _hubContext = hubContext;
            _logger = logger;
        }

        [HttpPost("confirm")]
        public async Task<IActionResult> ConfirmBooking([FromBody] ConfirmBookingDto confirmRequest)
        {
            // Проверка входных данных
            if (confirmRequest == null || confirmRequest.UserId <= 0 || confirmRequest.BookingId <= 0)
            {
                return BadRequest("Invalid confirmation request.");
            }

            _logger.LogInformation("ConfirmBooking for userId: {UserId}", confirmRequest.UserId);

            try
            {
                var booking = await _bookingService.ConfirmBooking(confirmRequest.BookingId, confirmRequest.IsConfirmed);
                await _hubContext.Clients.User(confirmRequest.UserId.ToString()).NotifyUser(confirmRequest.IsConfirmed);

                // Уведомить себя (другие соединения)
                var currentUserId = HttpContext.User.Identity.Name; // Получаем текущий userId
                await _hubContext.Clients.User(currentUserId).NotifyUser(confirmRequest.IsConfirmed);

                _logger.LogInformation("User notified for booking confirmation.");
                return Ok("Booking confirmation sent.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to confirm booking for user {UserId}", confirmRequest.UserId);
                return StatusCode(500, "Failed to confirm booking.");
            }
        }
    }
}
