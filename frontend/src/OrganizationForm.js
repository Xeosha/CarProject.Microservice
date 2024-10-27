import React, { useState } from 'react';
import { HubConnectionBuilder } from "@microsoft/signalr";

function OrganizationForm() {
    const [connection, setConnection] = useState(null);
    const [requests, setRequests] = useState([]);
    const [organizationId, setOrganizationId] = useState("");

    const handleConnection = async () => {
        const newConnection = new HubConnectionBuilder()
            .withUrl(`https://localhost:6060/notificationHub?organizationId=${organizationId}`)
            .withAutomaticReconnect()
            .build();

        newConnection.on('NotifyOrganization', (userId, service) => {
            // Добавляем userId в состояние requests
            setRequests(prev => [...prev, { userId, service }]);
        });

        setConnection(newConnection);

        if (newConnection) {
            newConnection.start()
                .then(() => {
                    console.log('Connected as Organization');
                })
                .catch(e => console.log('Connection failed: ', e));
        }
    };

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
            <input
                type="number"
                placeholder="Organization Id"
                value={organizationId}
                onChange={(e) => setOrganizationId(e.target.value)}
            />
            <button onClick={handleConnection}>Установить соединение</button>
            <h2>Organization Booking Requests</h2>
            {requests.map((req, index) => (
                <div key={index}>
                    {/* Добавляем отображение userId рядом с сервисом */}
                    <p>User ID: {req.userId}, Service Requested: {req.service}</p>
                    <button onClick={() => handleConfirmBooking(req.userId, true)}>Confirm</button>
                    <button onClick={() => handleConfirmBooking(req.userId, false)}>Decline</button>
                </div>
            ))}
        </div>
    );
}

export default OrganizationForm;
