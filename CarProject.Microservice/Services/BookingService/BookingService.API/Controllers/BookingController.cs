using Microsoft.AspNetCore.Mvc;
using BookingService.Application.Services;
using BookingService.Domain.Models;

namespace BookingService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly BookingsService _bookingService;

        public BookingController(BookingsService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpPost("request")]
        public async Task<IActionResult> RequestBooking([FromBody] Booking request)
        {
            var response = await _bookingService.RequestBooking(request);

            return Ok(response);
        }

        [HttpPost("confirm")]
        public async Task<IActionResult> ConfirmBooking(int bookingId, [FromQuery] bool isConfirmed)
        {
            bool result = await _bookingService.ConfirmBooking(bookingId, isConfirmed);

            if (result)
                return Ok(new { Message = "Booking confirmed" });
            return BadRequest(new { Message = "Booking declined" });
        }
    }
}
