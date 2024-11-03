
const Booking = async ({ connection, selectedOrganization, confirmationSlot, selectedService }) => {
    if (connection) {
        try {
            const { date, timeSlot } = confirmationSlot;
            console.log(confirmationSlot);
            console.log(date);
            console.log(timeSlot);
            // Создаем объект для bookingTime, объединяя дату и время начала
            const bookingDateTime = new Date(
                `${date.split("T")[0]}T${timeSlot}`
            ).toISOString();

            await connection.invoke('RequestBooking', selectedOrganization, selectedService.id, bookingDateTime);

        } catch (e) {
            console.error('Bookings.js request failed: ', e);
        }
    }
};
export default Booking;
