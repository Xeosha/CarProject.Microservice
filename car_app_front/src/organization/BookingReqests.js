import React from 'react';

const BookingRequests = ({ requests, services }) => {
    const handleConfirm = (requestId) => {
        // Логика подтверждения запроса
        // Например, отправка запроса на сервер для подтверждения
    };

    const handleReject = (requestId) => {
        // Логика отклонения запроса
        // Например, отправка запроса на сервер для отклонения
    };

    const handleComplete = (requestId) => {
        // Логика завершения бронирования
        // Например, отправка запроса на сервер для изменения статуса на "выполнен"
    };

    return (
        <div>
            {requests.map(request => {
                // Поиск названия услуги по id
                const service = services.find(service => service.id === request.serviceId);
                return (
                    <div key={request.id}>
                        <p>Клиент: {request.clientName}</p>
                        <p>Услуга: {service ? service.name : 'Неизвестная услуга'}</p>
                        <p>Время: {request.time}</p>
                        <p>Комментарий: {request.comment}</p>
                        <p>Статус: {request.status}</p>
                        {request.status === 'ожидает подтверждения' && (
                            <div>
                                <button onClick={() => handleConfirm(request.id)}>Подтвердить</button>
                                <button onClick={() => handleReject(request.id)}>Отклонить</button>
                            </div>
                        )}
                        {request.status === 'одобрен' && (
                            <button onClick={() => handleComplete(request.id)}>Выполнить</button>
                        )}
                    </div>
                );
            })}
        </div>
    );
};

export default BookingRequests;
