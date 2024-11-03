// Services.js
import React, { useEffect, useState } from 'react';
import GetServiceOrgIds from "../api/GetServiceOrgIds";

const Services = ({ organizationID }) => {
    const [services, setServices] = useState([]);
    const [loading, setLoading] = useState(true);
    const [noServicesMessage, setNoServicesMessage] = useState(false);

    useEffect(() => {
        // Функция для получения данных и обновления состояния
        const fetchServices = async () => {
            setLoading(true);
            setNoServicesMessage(false); // Сброс сообщения

            const filteredServices = await GetServiceOrgIds({ organizationID });

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
    }, [organizationID]);

    return (
        <div>
            {loading ? (
                <p>Загрузка услуг...</p>
            ) : services.length > 0 ? (
                services.map(service => (
                    <div key={service.id}>
                        <h3>{service.serviceName}</h3>
                        <p>{service.description}</p>
                        <p>Время: {service.time}</p>
                        <p>Цена: {service.price}</p>
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

export default Services;
