import React, { useEffect, useState } from 'react';
import GetAllBookings from "../api/GetAllBookings";

const Bookings = ({ serviceOrganizationID }) => {
    const [services, setServices] = useState([]);
    const [loading, setLoading] = useState(true);
    const [noServicesMessage, setNoServicesMessage] = useState(false);

    useEffect(() => {
        // Функция для получения данных и обновления состояния
        const fetchServices = async () => {
            setLoading(true);
            setNoServicesMessage(false); // Сброс сообщения

            const filteredServices = await GetAllBookings({ serviceOrganizationID });

            if (filteredServices && filteredServices.length > 0) {
                setServices(filteredServices);
            } else {
                setServices([]);
                // Устанавливаем таймер для показа сообщения об отсутствии услуг
                setTimeout(() => {
                    setNoServicesMessage(true);
                }, 5000); // 5000 мс = 5 секунд
            }

            setLoading(false);
        };

        fetchServices();
    }, [serviceOrganizationID]);

    return (
        <div>
            {loading ? (
                <p>Загрузка услуг...</p>
            ) : services.length > 0 ? (
                services.map(service => (
                    <div key={service.serviceOrganizationID}>
                        <h3>{service.userID}</h3>
                        <p>Время: {service.bookingTime}</p>
                        <p>Статус: {service.bookingStatus}</p>
                        <p>Записи: {service.notes}</p>
                    </div>
                ))
            ) : noServicesMessage ? (
                <p>У вашей организации ещё нет услуг</p>
            ) : (
                <p>Загрузка услуг...</p>
            )}
        </div>
    );
};

export default Bookings;
