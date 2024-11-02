import React, { useState, useEffect } from 'react';
import AuthComponent from '../components/AuthComponent';
import ClientPage from '../pages/ClientPage';
import OrganizationPage from '../pages/OrganizationPage';
import { HubConnectionBuilder } from "@microsoft/signalr";

// Пример данных для авторизации
const clients = [
    { id: '6408aaa9-d97f-4034-803f-4ceff1763fa9', login: 'client1', password: 'pass1', fullName: 'John Doe' },
    { id: '6408aaa9-d97f-4034-803f-4ceff1763fb9', login: 'client2', password: 'pass2', fullName: 'Jane Smith' },
];

const organizations = [
    { id: '3a6b5438-c8b3-4c0e-9ad4-8662347c30ac', login: 'org1', password: 'orgpass1', name: 'Organization One' },
    { id: '6408aaa9-d97f-4034-803f-4ceff1763fd9', login: 'org2', password: 'orgpass2', name: 'Organization Two' },
];

const App = () => {
    const [isAuthenticated, setIsAuthenticated] = useState(false);
    const [mode, setMode] = useState('client'); // 'client' или 'organization'
    const [user, setUser] = useState(null);
    const [connection, setConnection] = useState(null);
    const [requests, setRequests] = useState([]);
    const [notification, setNotification] = useState(null);

    // Проверка авторизации при загрузке приложения
    useEffect(() => {
        const storedUser = localStorage.getItem('user');
        if (storedUser) {
            const userData = JSON.parse(storedUser);
            setIsAuthenticated(true);
            setUser(userData);
            setMode(userData.type || 'client'); // Установите режим в зависимости от типа пользователя, по умолчанию client
            handleConnection(userData.id); // устанавливаем соединение при перезагрузке
        }
    }, []);



    const handleConnection = async (userId) => {
        const newConnection = new HubConnectionBuilder()
            .withUrl(`https://localhost:6060/notificationHub?userId=${userId}`)
            .withAutomaticReconnect()
            .build();

        if (mode === 'client') {
            newConnection.on('Notify', (booking) => {
                console.log(booking.BookingStatus);
                setNotification(booking.BookingStatus === 1 ? 'Booking confirmed!' : 'Booking declined.');
            });
        } else {
            newConnection.on('Notify', (booking) => {
                console.log(`Booking request received for User: ${booking.userId}`);
                setRequests(prev => [...prev, { userId: booking.userId, serviceOrganizationId: booking.serviceOrganizationId, bookingId: booking.bookingId }]);
                console.log(requests);
            });
        }

        setConnection(newConnection);
        await newConnection.start();
    };




    // Функция для обработки логина
    const handleLogin = async (username, password) => {
        let authenticatedUser = null;

        if (mode === 'client') {
            // Проверка клиента
            authenticatedUser = clients.find(
                (c) => c.login === username && c.password === password
            );
        } else if (mode === 'organization') {
            // Проверка организации
            authenticatedUser = organizations.find(
                (org) => org.login === username && org.password === password
            );
        }
        console.log(authenticatedUser);
        if (authenticatedUser) {
            setIsAuthenticated(true);
            setUser(authenticatedUser);

            // Сохраняем данные пользователя в localStorage
            localStorage.setItem('user', JSON.stringify({ ...authenticatedUser, type: mode }));
            await handleConnection(authenticatedUser.id); // устанавливаем соединение
        } else {
            alert('Неправильные логин или пароль');
        }
    };

    // Функция для переключения режима
    const handleModeSwitch = (newMode) => {
        setMode(newMode);
        setIsAuthenticated(false); // сброс авторизации при смене режима
        setUser(null); // сброс текущего пользователя
    };

    // Функция для выхода из профиля
    const handleLogout = () => {
        localStorage.removeItem('user'); // Удаляем данные о пользователе из localStorage
        setIsAuthenticated(false); // Обновляем состояние авторизации
        setUser(null); // Удаляем информацию о пользователе
        setMode('client'); // Сбрасываем режим на клиентский
    };

    return (
        <div>
            <h1>Booking Service</h1>
            <header>
                {isAuthenticated && <button onClick={handleLogout}>Выйти</button>}
            </header>

            {!isAuthenticated ? (
                <div>
                    <div className="flex-container">
                        <button
                            className={mode === 'client' ? 'active-button' : ''}
                            onClick={() => handleModeSwitch('client')}
                            disabled={mode === 'client'} // Делаем кнопку неактивной
                        >
                            Клиент
                        </button>
                        <button
                            className={mode === 'organization' ? 'active-button' : ''}
                            onClick={() => handleModeSwitch('organization')}
                            disabled={mode === 'organization'} // Делаем кнопку неактивной
                        >
                            Организация
                        </button>
                    </div>
                    <AuthComponent onLogin={handleLogin} />
                </div>
            ) : mode === 'client' ? (
                <ClientPage user={user} connection={connection} notification={notification}/>
            ) : (
                <OrganizationPage user={user} connection={connection} requests={requests}/>
            )}
        </div>
    );
};

export default App;
