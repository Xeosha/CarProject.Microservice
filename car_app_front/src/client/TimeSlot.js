import React, {useEffect} from "react";

function TimeSlot({ timeSlot, setTimeSlot, setConfirmationSlot,setSelectedOrganization, closeConfirmationModal }) {


    // Обработка выбора времени и подтверждения
    const handleTimeSlotClick = (date, timeSlot) => {
        setConfirmationSlot({ date, timeSlot });
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


    return(
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
    )
}

export default TimeSlot;