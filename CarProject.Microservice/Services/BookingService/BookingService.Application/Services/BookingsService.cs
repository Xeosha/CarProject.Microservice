using BookingService.Domain.Interfaces;
using BookingService.Domain.Interfaces.Repositories;
using BookingService.Domain.Models;
using BookingService.Domain.Models.Dto;
using Microsoft.Extensions.Logging;
using ShareDTO;

namespace BookingService.Application.Services
{
    public class BookingsService : IBookingService
    {
        ILogger<BookingsService> _logger;
        IBookingsRepository _bookingsRepository { get; set; }  
        ICatalogServiceClient _catalogServiceClient {  get; set; }
        public BookingsService(IBookingsRepository bookingsRepository, ICatalogServiceClient catalogServiceClient, ILogger<BookingsService> logger)
        {
            _bookingsRepository = bookingsRepository;
            _catalogServiceClient = catalogServiceClient;
            _logger = logger;
        }

        public async Task<Booking?> GetBookingById(Guid bookingId)
        {
            return await _bookingsRepository.GetById(bookingId);
        }

        public async Task<Booking> CreateBooking(Guid userId, Guid serviceOrganizationId, DateTime bookingTime, string notes)
        {
            var bookingId = Guid.NewGuid();

            // здесь проверить, что такого айди нет
                
            var booking = Booking.Create(bookingId, userId, serviceOrganizationId, bookingTime, "123");

            await _bookingsRepository.Add(booking);

            return booking;
        }

        public async Task<Booking> ConfirmBooking(Guid bookingId, bool isConfirmed)
        {
            var booking = await GetBookingById(bookingId);

            _logger.LogInformation("\n\n Я вытянул booking: " + booking.BookingId);
            
            booking.BookingStatus = isConfirmed ? BookingStatus.Confirmed : BookingStatus.Declined;

            await _bookingsRepository.Update(booking);
            _logger.LogInformation("\n\n Я обновил booking: " + booking.BookingId);

            return booking;
        }



        public async Task<List<Booking>> GetBookings(Guid organizationServiceId, DateTime start, DateTime end)
        {
            return await _bookingsRepository.GetAll(organizationServiceId, start, end);
        }

        public async Task<List<BookingDto>> GetBookingsForUser(Guid userId)
        {
            // Получаем все бронирования для указанного пользователя
            var bookings = await _bookingsRepository.GetForUser(userId);

            if (bookings == null || !bookings.Any())
            {
                return new List<BookingDto>();
            }

            // Извлекаем список идентификаторов ServiceOrg для всех бронирований
            var serviceOrgIds = bookings.Select(b => b.ServiceOrganizationId).Distinct().ToList();

            // Получаем список ServiceOrgDto, связанных с идентификаторами ServiceOrg
            var serviceOrgs = await _catalogServiceClient.GetServicesWithServiceOrgId(serviceOrgIds);

            // Создаем список BookingDto с вложенным объектом ServiceOrgDto
            var bookingDtos = bookings.Select(booking =>
            {
                var serviceOrg = serviceOrgs.FirstOrDefault(s => s.Id == booking.ServiceOrganizationId);
                return new BookingDto
                {
                    BookingId = booking.BookingId,
                    UserId = booking.UserId,
                    BookingTime = booking.BookingTime,
                    BookingStatus = booking.BookingStatus,
                    Notes = booking.Notes,
                    ServiceOrg = serviceOrg // Вложенный объект ServiceOrgDto
                };
            }).ToList();

            return bookingDtos;
        }

        public async Task<List<BookingDto>> GetBookingsForOrg(Guid organizationId)
        {

            _logger.LogInformation("Начато получение бронирований для организации с ID: {OrganizationId}", organizationId);

            // Получаем список ServiceOrgDto, связанных с organizationId
            var serviceOrgs = await _catalogServiceClient.GetServiceOrgIdsForOrganization(organizationId);

            if (serviceOrgs == null || !serviceOrgs.Any())
            {
                return new List<BookingDto>();
            }

            _logger.LogInformation("Найдено {ServiceOrgsCount} услуг для организации", serviceOrgs.Count);

            // Извлекаем список идентификаторов ServiceOrg для выборки бронирований
            var serviceOrgIds = serviceOrgs.Select(s => s.Id).ToList();

            // Получаем все бронирования для указанной организации
            var bookings = await _bookingsRepository.GetForOrg(serviceOrgIds);

            _logger.LogInformation("Найдено {BookingCount} бронирований для организации", bookings.Count);

            // Создаем список BookingDto с вложенным объектом ServiceOrgDto
            var bookingDtos = bookings.Select(booking =>
            {
                var serviceOrg = serviceOrgs.FirstOrDefault(s => s.Id == booking.ServiceOrganizationId);
                return new BookingDto
                {
                    BookingId = booking.BookingId,
                    UserId = booking.UserId,
                    BookingTime = booking.BookingTime,
                    BookingStatus = booking.BookingStatus,
                    Notes = booking.Notes,
                    ServiceOrg = serviceOrg  // Вложенный объект ServiceOrgDto
                };
            }).ToList();

            return bookingDtos;
        }

    }
}
