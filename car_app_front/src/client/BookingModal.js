import React, { useState } from 'react';

function BookingModal({ closeConfirmationModal, confirmationSlot, toBook }) {
    const [description, setdDescription] = useState('');

    const handleConfirm = () => {
        toBook(description); // Подтверждаем бронирование
    };

    return (
        <div className="confirmation-modal">
            <button className="close-modal" onClick={closeConfirmationModal}>✖</button>
            <h4>Подтвердите бронирование</h4>
            <p>Дата: {confirmationSlot.date.split("T")[0]}</p>
            <p>Время: {confirmationSlot.timeSlot}</p>

            <input
                type="text"
                placeholder="Введите комментарий"
                value={description}
                onChange={(e) => setdDescription(e.target.value)} // Обновляем comment
            />

            {/* Подтверждаем с использованием актуального значения comment */}
            <button onClick={handleConfirm}>Подтвердить</button>
            <button onClick={closeConfirmationModal}>Отмена</button>
        </div>
    );
}

export default BookingModal;
