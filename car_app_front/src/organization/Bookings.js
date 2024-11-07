import React, { useEffect, useState } from 'react';
import GetAllBookings from "../api/GetAllBookings";

const Bookings = ({ organizationId, connection, servicesVersion }) => {
    const [bookings, setBookings] = useState([]);
    const [loading, setLoading] = useState(true);
    const [noServicesMessage, setNoServicesMessage] = useState(false);

    const userId = organizationId;
    const mode = "organization";

    const fetchServices = async () => {
        setLoading(true);
        setNoServicesMessage(false);

        const filteredServices = await GetAllBookings({ userId, mode });

        if (filteredServices && filteredServices.length > 0) {
            setBookings(filteredServices);
        } else {
            setBookings([]);
            // Set a timer to show the "no services" message
            setTimeout(() => {
                setNoServicesMessage(true);
            }, 5000);
        }

        setLoading(false);
    };

    // Initial fetch of all bookings
    useEffect(() => {

        fetchServices();
    }, [servicesVersion]);


    useEffect(() => {
        if (connection) {
            // Добавляем обработчик события в дочернем элементе
            connection.on('Notify', (booking) => {
                fetchServices();
            });

            // Очищаем обработчик при размонтировании компонента
            return () => {
                connection.off('Notify');
            };
        }
    }, [connection]);


    // Handle booking confirmation
    const handleConfirmBooking = async (bookingId, isConfirmed) => {
        if (connection) {
            try {
                await connection.invoke('ConfirmBooking', bookingId, isConfirmed);
                setBookings(await GetAllBookings({ userId, mode }));
            } catch (e) {
                console.error('Bookings.js confirmation failed: ', e);
            }
        }
    };

    return (
        <div>
            {/* Bookings section */}
            <div>
                {loading ? (
                    <p>Загрузка запросов...</p>
                ) : bookings.length > 0 ? (
                    bookings.map(booking => (
                        <div key={booking.bookingId}>
                            <h3>{booking.serviceOrg.serviceName}</h3>
                            <p>Место: {booking.serviceOrg.organizationName}</p>
                            <p>Время: {booking.bookingTime.split("T")[0] + " " + booking.bookingTime.split("T")[1].split("Z")[0]}</p>
                            <p>Цена: {booking.serviceOrg.price}</p>
                            <p>Комментарий: {booking.notes}</p>
                            <p>Статус: {booking.bookingStatus > 1 ? "Отказ" : (booking.bookingStatus > 0 ? "Подтверждено" : "Ответьте на заявку")}</p>
                            { booking.bookingStatus === 0 ? (
                                <>
                                    <button onClick={() => handleConfirmBooking(booking.bookingId, true)}>Подтвердить</button>
                                    <button onClick={() => handleConfirmBooking(booking.bookingId, false)}>Отклонить</button>
                                </>
                                ) : (<></>)}
                        </div>
                    ))
                ) : (
                    <p>Вашей организации ещё не поступили запросы</p>
                )}
            </div>
        </div>
    );
};

export default Bookings;
