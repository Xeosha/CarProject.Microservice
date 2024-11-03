import React, { useState, useEffect } from 'react';



const ClientPage = ({ user, connection, notification }) => {
    const [services, setServices] = useState([]);
    const [selectedService, setSelectedService] = useState(null);
    const [organizations, setOrganizations] = useState([]);
    const [filteredOrganizations, setFilteredOrganizations] = useState([]);
    const [selectedOrganization, setSelectedOrganization] = useState(null);
    const [timeSlot, setTimeSlot] = useState([]);
    const [confirmationSlot, setConfirmationSlot] = useState(null);
    const [searchTerm, setSearchTerm] = useState('');
    const [hoveredOrg, setHoveredOrg] = useState(null); // Добавляем состояние для выделения организации


    const handleBookingRequest = async () => {
        if (connection) {
            try {
                if (!/^[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}$/.test(user.id) ||
                    !/^[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}$/.test(selectedOrganization)) {
                    //setNotific('Invalid GUID format for userId or organizationId');
                    return;
                }

                const { date, timeSlot } = confirmationSlot;

                // Создаем объект для bookingTime, объединяя дату и время начала
                const bookingDateTime = new Date(
                    `${date.split("T")[0]}T${timeSlot}`
                ).toISOString();

                console.log("Текстfwefewfwfewfwfweewf: " + selectedOrganization + " " + selectedService.id + " " + bookingDateTime);

                await connection.invoke('RequestBooking', selectedOrganization, selectedService.id, bookingDateTime);

                //setNotific('Booking request sent.');
            } catch (e) {
                console.error('Booking request failed: ', e);
            }
            // Очищаем слоты и сбрасываем выбор
            setTimeSlot([]);
            setSelectedOrganization(null);
            setConfirmationSlot(null);
        }
    };





    // Обработка выбора услуги
    const handleServiceSelect = (service) => {
        setSelectedService(service);

        const filteredOrganizations = services
            .filter(item => item.serviceName === service.serviceName)
            .map(item => ({
                id: item.idOrganization,
                name: item.organizationName,
                address: item.location
            }));

        if (filteredOrganizations.length > 0) {
            setOrganizations(filteredOrganizations);
            setFilteredOrganizations(filteredOrganizations);
        } else {
            console.warn('Нет организаций для этой услуги:', service.nameServise);
            setOrganizations([]);
        }
    };

    // Фильтрация организаций по поисковому запросу
    const handleSearchChange = (e) => {
        const value = e.target.value.toLowerCase();
        setSearchTerm(value);

        const filtered = organizations.filter(org =>
            org.name.toLowerCase().includes(value) ||
            org.address.toLowerCase().includes(value)
        );

        setFilteredOrganizations(filtered);
    };

    // Запрос доступных временных слотов по выбранной организации
    const handleOrganizationSelect = async (organizationId) => {
        setSelectedOrganization(organizationId);

        // Находим элемент из services, соответствующий выбранной организации и услуге
        const service = services.find(item =>
            item.idOrganization === organizationId && item.idService === selectedService.idService
        );

        if (service) {
            try {
                const response = await fetch(`https://localhost:6060/api/User/getAvailableTimes?organizationServiceId=${service.id}`);
                const data = await response.json();
                setTimeSlot(data);
            } catch (error) {
                console.error('Ошибка загрузки временных слотов:', error);
            }
        } else {
            console.warn('Сервис для выбранной организации и услуги не найден.');
        }
    };


    // Обработка выбора времени и подтверждения
    const handleTimeSlotClick = (date, timeSlot) => {
        setConfirmationSlot({ date, timeSlot });
    };

    const confirmBooking = async () => {
        console.log(confirmationSlot);
        console.log(selectedOrganization);
        console.log(selectedService);
        if (!confirmationSlot || !selectedOrganization || !selectedService) {
            console.warn("Данные для бронирования неполные.");
            return;
        }

        const { date, timeSlot } = confirmationSlot;

        // Создаем объект для bookingTime, объединяя дату и время начала
        const bookingDateTime = new Date(
            `${date.split("T")[0]}T${timeSlot}`
        ).toISOString();

        try {
            const response = await fetch('https://localhost:6060/api/User/request', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({
                    organizationId: selectedOrganization,
                    serviceOrganizationId: selectedService.id,
                    bookingTime: bookingDateTime
                })
            });
            console.log(response);
            if (response.ok) {
                alert('Бронирование подтверждено');
            } else {
                console.error("Ошибка при бронировании:", await response.text());
            }

            // Очищаем слоты и сбрасываем выбор
            setTimeSlot([]);
            setSelectedOrganization(null);
            setConfirmationSlot(null);
        } catch (error) {
            console.error('Ошибка подтверждения бронирования:', error);
        }
    };


    // Обработка закрытия модальных окон
    const closeConfirmationModal = () => {
        setConfirmationSlot(null);
    };

    const closeTimeSlotsModal = () => {
        setTimeSlot([]);
        setSelectedOrganization(null);
    };

    // Обработка нажатия клавиши Escape
    useEffect(() => {
        const handleKeyDown = (event) => {
            if (event.key === 'Escape') {
                closeConfirmationModal();
                closeTimeSlotsModal();
            }
        };

        window.addEventListener('keydown', handleKeyDown);
        return () => {
            window.removeEventListener('keydown', handleKeyDown);
        };
    }, []);

    return (
        <div>
            <h2>Добро пожаловать, {user.fullName}</h2>

            {/* Отображение списка услуг в виде прямоугольников */}
            <div>
                <h3>Выберите услугу:</h3>
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
            </div>

            {/* Поиск организаций */}
            {selectedService && (
                <div>
                    <h3>Поиск организаций:</h3>
                    <input
                        type="text"
                        placeholder="Поиск по названию или адресу"
                        value={searchTerm}
                        onChange={handleSearchChange}
                    />
                    <h3>Организации, предоставляющие {selectedService.serviceName}:</h3>
                    <ul>
                        {filteredOrganizations.map((org) => (
                            <li
                                key={org.id}
                                onMouseEnter={() => setHoveredOrg(org.id)}
                                onMouseLeave={() => setHoveredOrg(null)}
                                onClick={() => handleOrganizationSelect(org.id)}
                                className={`organization-item ${selectedOrganization === org.id ? 'selected' : ''} ${hoveredOrg === org.id ? 'hovered' : ''}`}
                            >
                                <div className="organization-item-content">
                                    <span>{org.name}</span>
                                    <span>{org.address}</span>
                                </div>
                            </li>
                        ))}
                    </ul>
                </div>
            )}

            {/* Модальное окно для выбора временных слотов */}
            {timeSlot.length > 0 && (
                <div className="time-slots-modal">
                    <button className="close-modal" onClick={closeTimeSlotsModal}>✖</button>
                    <div>
                        <h3>Доступные временные слоты:</h3>
                        <table>
                            <thead>
                            <tr>
                                {timeSlot.map(slot => (
                                    <th key={slot.date}>{slot.date.split("T")[0]}</th>
                                ))}
                            </tr>
                            </thead>
                            <tbody>
                            <tr>
                                {timeSlot.map(slot => (
                                    <td key={slot.date}>
                                        {slot.timeSlots.map((time, index) => (
                                            <div key={index} style={{ margin: '5px 0' }}> {/* Добавляем отступы между временными слотами */}
                                                <button
                                                    key={index}
                                                    disabled={time.isBooked} // Деактивируем кнопку, если временной слот занят
                                                    onClick={() => handleTimeSlotClick(slot.date, time.startTime)}
                                                    className={time.isBooked ? 'disabled' : ''} // Применяем класс disabled
                                                >
                                                    {time.startTime}
                                                </button>
                                            </div>
                                        ))}
                                    </td>
                                ))}
                            </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            )}

            {/* Окно подтверждения бронирования */}
            {confirmationSlot && (
                <div className="confirmation-modal">
                    <button className="close-modal" onClick={closeConfirmationModal}>✖</button>
                    <h4>Подтвердите бронирование</h4>
                    <p>Дата: {confirmationSlot.date.split("T")[0]}</p>
                    <p>Время: {confirmationSlot.timeSlot}</p>
                    <button onClick={handleBookingRequest}>Подтвердить</button>
                    <button onClick={closeConfirmationModal}>Отмена</button>
                </div>
            )}
        </div>
    );
};

export default ClientPage;