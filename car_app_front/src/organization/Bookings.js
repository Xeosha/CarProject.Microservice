import React, { useEffect, useState } from 'react';
import GetAllBookings from "../api/GetAllBookings";

const Bookings = ({ organizationId, connection, requests, setRequests }) => {
    const [bookings, setBookings] = useState([]);
    const [loading, setLoading] = useState(true);
    const [noServicesMessage, setNoServicesMessage] = useState(false);

    // Initial fetch of all bookings
    useEffect(() => {
        const fetchServices = async () => {
            setLoading(true);
            setNoServicesMessage(false);

            const userId = organizationId;
            const mode = "organization";
            const filteredServices = await GetAllBookings({ userId, mode });

            if (filteredServices && filteredServices.length > 0) {
                setBookings(filteredServices);
            } else {
                setBookings([]);
                // Set a timer to show the "no services" message
                setTimeout(() => {
                    setNoServicesMessage(true);
                }, 5000);
            }

            setLoading(false);
        };

        fetchServices();
    }, [organizationId]);


    // Effect to handle changes in requests
    useEffect(() => {
        if (connection) {
            if (requests && requests.length > 0) {
                // Example: Process each request (you can modify the logic as needed)
                requests.forEach(request => {
                    console.log('Handling request: ', request);
                });
            }
        }
    }, [requests]); // Run effect when requests change


    // Handle booking confirmation
    const handleConfirmBooking = async (bookingId, isConfirmed) => {
        if (connection) {
            try {
                await connection.invoke('ConfirmBooking', bookingId, isConfirmed);
                // Remove the request from the list after confirmation
                setRequests(prevRequests => prevRequests.filter(req => req.bookingId !== bookingId));
            } catch (e) {
                console.error('Bookings.js confirmation failed: ', e);
            }
        }
    };

    return (
        <div>

            {/* Bookings section */}
            <div>
                {loading ? (
                    <p>Загрузка услуг...</p>
                ) : bookings.length > 0 ? (
                    bookings.map(service => (
                        <div key={service.bookingId}>
                            <h3>{service.userId}</h3>
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
        </div>
    );
};

export default Bookings;
