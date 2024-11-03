import React, { useEffect, useState } from 'react';
import GetAllBookings from "../api/GetAllBookings";

const Bookings = ({ organizationId, connection, requests, setRequests }) => {
    const [bookings, setBookings] = useState([]);
    const [loading, setLoading] = useState(true);
    const [noServicesMessage, setNoServicesMessage] = useState(false);

    // Initial fetch of all bookings
    useEffect(() => {
        const fetchServices = async () => {
            setLoading(true);
            setNoServicesMessage(false);

            const userId = organizationId;
            const mode = "organization";
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

        fetchServices();
    }, [organizationId]);


    useEffect(() => {
        if (connection) {
            // Добавляем обработчик события в дочернем элементе
            connection.on('Notify', (booking) => {
                setBookings(prevBookings => {
                    // Проверяем, существует ли уже booking с таким же bookingId
                    const isDuplicate = prevBookings.some(b => b.bookingId === booking.bookingId);
                    if (!isDuplicate) {
                        // Добавляем новый объект только если его нет в массиве
                        return [...prevBookings, booking];
                    }
                    return prevBookings; // Возвращаем старый массив, если дубликат найден
                });
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
                // Remove the request from the list after confirmation
                setRequests(prevRequests => prevRequests.filter(req => req.bookingId !== bookingId));
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
                    <p>Загрузка услуг...</p>
                ) : bookings.length > 0 ? (
                    bookings.map(booking => (
                        <div key={booking.bookingId}>
                            <h3>{booking.userId}</h3>
                            <p>Время: {booking.bookingTime}</p>
                            <p>Статус: {booking.bookingStatus}</p>
                            <p>Записи: {booking.notes}</p>
                            <button onClick={() => handleConfirmBooking(booking.bookingId, true)}>Confirm</button>
                            <button onClick={() => handleConfirmBooking(booking.bookingId, false)}>Decline</button>
                        </div>
                    ))
                ) : noServicesMessage ? (
                    <p>У вашей организации ещё нет услуг</p>
                ) : (
                    <p>Загрузка услуг...</p>
                )}
            </div>
        </div>
    );
};

export default Bookings;
