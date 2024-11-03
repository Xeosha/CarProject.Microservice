import {HubConnectionBuilder} from "@microsoft/signalr";

const Connection = async (userId, mode, setConnection, setRequests) => {
    const newConnection = new HubConnectionBuilder()
        .withUrl(`https://localhost:6060/notificationHub?userId=${userId}`)
        .withAutomaticReconnect()
        .build();

    // Универсальный обработчик для события "Notify"
    newConnection.on('Notify', (booking) => {
        setRequests(booking);
        console.log(booking);
    });

    setConnection(newConnection);
    await newConnection.start();
};

export default Connection;
