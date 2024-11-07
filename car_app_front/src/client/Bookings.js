import React, { useEffect, useState } from 'react';
import GetAllBookings from "../api/GetAllBookings";

const Bookings = ({ clientId, connection, requests, setRequests }) => {
    const [bookings, setBookings] = useState([]);
    const [loading, setLoading] = useState(true);
    const [noServicesMessage, setNoServicesMessage] = useState(false);
    const userId = clientId;
    const mode = "client";

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
    }, [clientId]);


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


    return (
        <div>
            <div>
                {loading ? (
                    <p>Загрузка записей...</p>
                ) : bookings.filter(booking => booking.serviceOrg !== null).length > 0 ? (
                    bookings
                        .filter(booking => booking.serviceOrg !== null) // Фильтруем записи с непустым serviceOrg
                        .map(booking => (
                            <div key={booking.bookingId}>
                                <h3>{booking.serviceOrg.serviceName}</h3>
                                <p>Место: {booking.serviceOrg.organizationName}</p>
                                <p>
                                    Время:{" "}
                                    {booking.bookingTime.split("T")[0] + " " +
                                        booking.bookingTime.split("T")[1].split("Z")[0]}
                                </p>
                                <p>
                                    Статус:{" "}
                                    {booking.bookingStatus > 1
                                        ? "Отказ"
                                        : booking.bookingStatus > 0
                                            ? "Подтверждено"
                                            : "Заявка в обработке"}
                                </p>
                                <p>Цена: {booking.serviceOrg.price}</p>
                                <p>Комментарий: {booking.notes}</p>
                            </div>
                        ))
                ) : (
                    <p>У вас ещё нет записей</p>
                )}
            </div>
        </div>
    );

};

export default Bookings;
