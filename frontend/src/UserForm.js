import React, { useState } from 'react';
import { HubConnectionBuilder } from "@microsoft/signalr";

function UserForm(url) {
    const [connection, setConnection] = useState(null);
    const [service, setService] = useState([]);
    const [userId, setUserId] = useState([]);
    const [organizationId, setOrganizationId] = useState([]);
    const [notification, setNotification] = useState(null);



    const handleConnection = async () => {

        console.log(userId);

        const newConnection = new HubConnectionBuilder()
            .withUrl(`https://localhost:6060/notificationHub?userId=${userId}`)
            .withAutomaticReconnect()
            .build();

        newConnection.on('NotifyUser', (isConfirmed) => {
            setNotification(isConfirmed ? 'Booking confirmed!' : 'Booking declined.');
            console.log("da")
        });

        setConnection(newConnection);

        if (newConnection) {
            newConnection.start()
                .then(() => {
                    console.log('Connected as User');
                })
                .catch(e => console.log('Connection failed: ', e));
        }
    }

    const handleBookingRequest = async () => {
        if (connection) {
            console.log(connection, service, userId, organizationId);
            try {
                await connection.invoke(
                    'RequestBooking',
                    parseInt(organizationId),
                    service );
                setNotification('Booking request sent.');
            } catch (e) {
                console.error('Booking request failed: ', e);
            }
        }
    };

    return (
        <div>
            <button onClick={handleConnection}>Установить соединение</button>
            <h2>User Booking Form</h2>
            <input
                type="number"
                placeholder="userId"
                value={userId}
                onChange={(e) => setUserId(e.target.value)}
            />
            <input
                type="number"
                placeholder="Organization ID"
                value={organizationId}
                onChange={(e) => setOrganizationId(e.target.value)}
            />
            <input
                type="text"
                placeholder="Service"
                value={service}
                onChange={(e) => setService(e.target.value)}
            />
            <button onClick={handleBookingRequest}>Request Booking</button>

            {notification && <p>{notification}</p>}
        </div>
    );
}

export default UserForm;
