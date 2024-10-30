import React, { useState } from 'react';
import { HubConnectionBuilder } from "@microsoft/signalr";

function UserForm(url) {
    const [connection, setConnection] = useState(null);
    const [service, setService] = useState([]);
    const [userId, setUserId] = useState([]);
    const [organizationId, setOrganizationId] = useState([]);
    const [notification, setNotification] = useState(null);
    const serviceOrgId = "6408aaa9-d97f-4034-803f-4ceff1763fa9";



    const handleConnection = async () => {

        console.log(userId);

        const newConnection = new HubConnectionBuilder()
            .withUrl(`https://localhost:6060/notificationHub?userId=${userId}`)
            .withAutomaticReconnect()
            .build();

        newConnection.on('Notify', (booking) => {
            console.log(booking.BookingStatus)
            setNotification(booking.BookingStatus === 1 ? 'Booking confirmed!' : 'Booking declined.');
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
            try {
                if (!/^[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}$/.test(userId) ||
                    !/^[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}$/.test(organizationId)) {
                    setNotification('Invalid GUID format for userId or organizationId');
                    return;
                }

                console.log("Текстfwefewfwfewfwfweewf: " + organizationId + " " + serviceOrgId + " " );

                await connection.invoke('RequestBooking', organizationId, serviceOrgId);

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
                type="text"
                placeholder="userId"
                value={userId}
                onChange={(e) => setUserId(e.target.value)}
            />
            <input
                type="text"
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
