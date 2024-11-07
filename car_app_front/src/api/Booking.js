
const Booking = async ({ connection, selectedOrganization, confirmationSlot, selectedService, description }) => {
    if (connection) {
        try {
            const { date, timeSlot } = confirmationSlot;

            // Создаем объект для bookingTime, объединяя дату и время начала
            const bookingDateTime = new Date(
                `${date.split("T")[0]}T${timeSlot}Z`
            ).toISOString();
            console.log("1111111", description);
            await connection.invoke('RequestBooking', selectedOrganization, selectedService.id, bookingDateTime, description);

        } catch (e) {
            console.error('Bookings.js request failed: ', e);
        }
    }
};
export default Booking;
