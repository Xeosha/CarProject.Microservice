import React, { useEffect, useState } from 'react';
import Services from '../organization/Services';

const OrganizationPage = ({ organizationId, connection, requests }) => {
    const [services, setServices] = useState([]);
    console.log(requests);
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











    useEffect(() => {
        const fetchServices = async () => {
            const data = [
                {
                    "id": "1",
                    "name": "Автомойка",
                    "description": "Моем машину чисто",
                    "time": "12:00-15:00",
                    "price": "1000 руб."
                },
                {
                    "id": "2",
                    "name": "Замена масла",
                    "description": "Быстро, четко",
                    "time": "18:00-21:00",
                    "price": "500 руб."
                }
            ]
            setServices(data);
        };


        fetchServices();
    }, [organizationId]);

    return (
        <div>
            <h1>Услуги вашей организации</h1>
            <Services services={services} />
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
