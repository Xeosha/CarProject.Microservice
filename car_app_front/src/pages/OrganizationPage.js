import React, { useEffect, useState } from 'react';
import Services from '../organization/Services';
import Bookings from '../organization/Bookings';

const OrganizationPage = ({ user, connection, requests, setRequests }) => {
    const [services, setServices] = useState([]);
    const [bookings, setBookings] = useState([]);


    const handleConfirmBooking = async (userId, isConfirmed) => {
        if (connection) {
            try {
                await connection.invoke('ConfirmBooking', requests.bookingId, isConfirmed);
                setRequests(requests.filter(req => req.userId !== userId));
            } catch (e) {
                console.error('Booking confirmation failed: ', e);
            }
        }
    };




    return (
        <div>
            <h1>Услуги вашей организации</h1>
            <Services organizationID={user.id} services={services} setServices={setServices}/>
            <h2>Запросы на бронирование</h2>
            {reque.map((req, index) => (
                <div key={index}>
                    <p>User ID: {req.userId}</p>
                    <button onClick={() => handleConfirmBooking(req.bookingId, true)}>Confirm</button>
                    <button onClick={() => handleConfirmBooking(req.bookingId, false)}>Decline</button>
                </div>
            ))}
        </div>
    );
};

export default OrganizationPage;
