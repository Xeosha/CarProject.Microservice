import React, {useEffect, useState} from "react";
import GetServices from "../api/GetServices";

function Services({ services, setServices, selectedService, setSelectedService,setOrganizations }) {

    const [loading, setLoading] = useState(true);
    const [noServicesMessage, setNoServicesMessage] = useState(false);

    useEffect(() => {
        // Функция для получения данных и обновления состояния
        const fetchServices = async () => {
            setLoading(true);
            setNoServicesMessage(false); // Сброс сообщения

            const services = await GetServices();

            if (services && services.length > 0) {
                setServices(services);
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
    }, []);

    // Обработка выбора услуги
    const handleServiceSelect = (service) => {
        setSelectedService(service);

        const orgForService = services
            .filter(item => item.serviceName === service.serviceName)
            .map(item => ({
                id: item.idOrganization,
                name: item.organizationName,
                address: item.location,
                price: item.price
            }));

        if (orgForService.length > 0) {
            setOrganizations(orgForService);
        } else {
            console.warn('Нет организаций для этой услуги:', service.nameServise);
            setOrganizations([]);
        }
    };


    return (
        <div className="services-container">
            {services.map(service => (
                <div
                    key={service.id}
                    className={`service-box ${selectedService?.id === service.id ? 'selected' : ''}`}
                    onClick={() => handleServiceSelect(service)}
                >
                    {service.serviceName}
                </div>
            ))}
        </div>
    );
}

export default Services;