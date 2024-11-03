import React, { useState } from 'react';

function BookingModal({ closeConfirmationModal, confirmationSlot, toBook }) {


    return (
        <div className="confirmation-modal">
            <button className="close-modal" onClick={closeConfirmationModal}>✖</button>
            <h4>Подтвердите бронирование</h4>
            <p>Дата: {confirmationSlot.date.split("T")[0]}</p>
            <p>Время: {confirmationSlot.timeSlot}</p>
            <button onClick={toBook}>Подтвердить</button>
            <button onClick={closeConfirmationModal}>Отмена</button>
        </div>
    );
}

export default BookingModal;
