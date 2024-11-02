function BookingModal({ serviceId, onClose }) {
    const timeSlots = [
        { time: '10:00', available: true },
        { time: '11:00', available: false },
        // More slots
    ];

    return (
        <div className="modal">
            <button onClick={onClose}>Close</button>
            <div className="time-slots">
                {timeSlots.map(slot => (
                    <button
                        key={slot.time}
                        disabled={!slot.available}
                        onClick={() => slot.available && alert(`Booking at ${slot.time}`)}
                    >
                        {slot.time}
                    </button>
                ))}
            </div>
        </div>
    );
}
