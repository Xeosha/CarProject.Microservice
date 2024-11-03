import React, { useEffect, useState } from 'react';
import Services from '../organization/Services';
import Bookings from '../organization/Bookings';

const OrganizationPage = ({ organizationId, connection, requests }) => {
    const [services, setServices] = useState([]);
    const [reque, setReue] = useState(requests);


    const handleConfirmBooking = async (userId, isConfirmed) => {
        if (connection) {
            try {
                await connection.invoke('ConfirmBooking', reque.bookingId, isConfirmed);
                setReue(reque.filter(req => req.userId !== userId));
            } catch (e) {
                console.error('Booking confirmation failed: ', e);
            }
        }
    };

    const handleBookingRequests = async (userId, isConfirmed) => {
        if (connection) {
            try {
                await connection.invoke('ConfirmBooking', reque.bookingId, isConfirmed);
                setReue(reque.filter(req => req.userId !== userId));
            } catch (e) {
                console.error('Booking confirmation failed: ', e);
            }
        }
    }


    return (
        <div>
            <h1>Услуги вашей организации</h1>
            <Services organizationID={organizationId} services={services} setServices={setServices}/>
            <h2>Запросы на услуги</h2>
            {reque.map((req, index) => (
                <div key={index}>
                    <p>User ID: {req.userId}</p>
                    <button onClick={() => handleConfirmBooking(req.bookingId, true)}>Confirm</button>
                    <button onClick={() => handleConfirmBooking(req.bookingId, false)}>Decline</button>
                </div>
            ))}
            <Bookings organizationID={organizationId} services={services} setServices={setServices}/>
            <h2>Запросы на запись</h2>
            {reque.map((req, index) => (
                handleBookingRequests()
            ))}
        </div>
    );
};

export default OrganizationPage;
