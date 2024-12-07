using BookingService.API.Hubs;
using BookingService.Domain.Interfaces;
using BookingService.Domain.Models;
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
        private readonly ILogger<OrganizationController> _logger;

        public OrganizationController(IBookingService bookingService, ILogger<OrganizationController> logger)
        {
            _bookingService = bookingService;
            _logger = logger;
        }

        [HttpGet("getAllBookings")]
        public async Task<List<BookingDto>> GetAllBookings([FromQuery] Guid organizationId)
        {
            return await _bookingService.GetBookingsForOrg(organizationId);
        }
    }
}
