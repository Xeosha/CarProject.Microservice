// Services.js
import React, { useEffect, useState } from 'react';
import ShowServices from "../api/ShowServices";

const Services = ({ organizationID, servicesVersion }) => {
    const [services, setServices] = useState([]);
    const [loading, setLoading] = useState(true);
    const [noServicesMessage, setNoServicesMessage] = useState(false);

    useEffect(() => {
        // Функция для получения данных и обновления состояния
        const fetchServices = async () => {
            setLoading(true);
            setNoServicesMessage(false); // Сброс сообщения

            const filteredServices = await ShowServices({ organizationID });

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
    }, [servicesVersion]);

    return (
        <div>
            {loading ? (
                <p>Загрузка услуг...</p>
            ) : services.length > 0 ? (
                services.map(service => (
                    <div key={service.id}>
                        <h3>{service.serviceName}</h3>
                        <p>Описание: {service.description}</p>
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
