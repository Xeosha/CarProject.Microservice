import React, { useState, useEffect } from 'react';
import { HubConnectionBuilder } from "@microsoft/signalr";

function OrganizationForm() {
    const [connection, setConnection] = useState(null);
    const [requests, setRequests] = useState([]);
    const [userId, setUserId] = useState(null);

    const handleConnection = async () => {
        const newConnection = new HubConnectionBuilder()
            .withUrl('http://localhost:5128/notificationHub?userId=2')
            .withAutomaticReconnect()
            .build();



        newConnection.on('NotifyOrganization', (service) => {
            setRequests(prev => [...prev, { userId: 1, service }]);
        });

        setConnection(newConnection);

        if (newConnection) {
            newConnection.start()
                .then(() => {
                    console.log('Connected as Organization');
                })
                .catch(e => console.log('Connection failed: ', e));
        }
    }
    const handleConfirmBooking = async (userId, isConfirmed) => {
        if (connection) {
            try {
                await connection.invoke('ConfirmBooking', userId, 123, isConfirmed);
                setRequests(requests.filter(req => req.userId !== userId));
            } catch (e) {
                console.error('Booking confirmation failed: ', e);
            }
        }
    };

    return (
        <div>
            <button onClick={handleConnection}>Установить соединение</button>
            <h2>Organization Booking Requests</h2>
            {requests.map((req, index) => (
                <div key={index}>
                    <p>Service Requested: {req.service}</p>
                    <button onClick={() => handleConfirmBooking(req.userId, true)}>Confirm</button>
                    <button onClick={() => handleConfirmBooking(req.userId, false)}>Decline</button>
                </div>
            ))}
        </div>
    );
}

export default OrganizationForm;
