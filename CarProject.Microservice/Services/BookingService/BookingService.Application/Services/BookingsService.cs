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
        ICatalogServiceClient _catalogServiceClient {  get; set; }
        public BookingsService(IBookingsRepository bookingsRepository, ICatalogServiceClient catalogServiceClient)
        {
            _bookingsRepository = bookingsRepository;
            _catalogServiceClient = catalogServiceClient;
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



        public async Task<List<Booking>> GetBookings(Guid organizationServiceId, DateTime start, DateTime end)
        {
            return await _bookingsRepository.GetAll(organizationServiceId, start, end);
        }

        public async Task<List<Booking>> GetBookingsForUser(Guid userId)
        {
            return await _bookingsRepository.GetForUser(userId);
        }

        public async Task<List<Booking>> GetBookingsForOrg(Guid organizationId)
        {
            // Получаем список идентификаторов `ServiceOrgId`, связанных с `organizationId` из CatalogService
            var serviceOrgIds = await _catalogServiceClient.GetServiceOrgIdsForOrganization(organizationId);

            // Если `ServiceOrgId` не найдены, возвращаем пустой список
            if (serviceOrgIds == null || !serviceOrgIds.Any())
            {
                return new List<Booking>();
            }

            // Запрашиваем все бронирования, где `ServiceOrgId` входит в полученный список
            return await _bookingsRepository.GetForOrg(serviceOrgIds);
        }

    }
}
