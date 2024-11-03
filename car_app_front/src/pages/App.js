import React, { useState, useEffect } from 'react';
import AuthComponent from '../components/AuthComponent';
import ClientPage from '../pages/ClientPage';
import OrganizationPage from '../pages/OrganizationPage';
import ToggleSwitch from '../components/ToggleSwitch'
import LogOut from "../components/LogOut";
import Connection from "../api/Connection";

const App = () => {
    const [isAuthenticated, setIsAuthenticated] = useState(false);
    const [mode, setMode] = useState('client'); // 'client' или 'organization'
    const [user, setUser] = useState(null);
    const [connection, setConnection] = useState(null);
    const [requests, setRequests] = useState([]);

    // Проверка авторизации при загрузке приложения
    useEffect(() => {
        const storedUser = localStorage.getItem('user');
        if (storedUser) {
            const userData = JSON.parse(storedUser);
            setIsAuthenticated(true);
            setUser(userData);
            setMode(userData.type || 'client'); // Установите режим в зависимости от типа пользователя, по умолчанию client
            Connection(userData.id, mode, setConnection, setRequests); // устанавливаем соединение при перезагрузке
            console.log(requests);
        }
    }, []);


    return (
        <div>
            <h1>Booking Service</h1>
            {isAuthenticated ? (
                <>
                    <LogOut
                        setIsAuthenticated={setIsAuthenticated}
                        setUser={setUser}
                        setMode={setMode}
                    />
                    {mode === 'client' ? (
                        <ClientPage
                            user={user}
                            connection={connection}
                            requests={requests}
                            setRequests={setRequests}
                        />
                    ) : (
                        <OrganizationPage
                            user={user}
                            connection={connection}
                            requests={requests}
                            setRequests={setRequests}
                        />
                    )}
                </>
            ) : (
                <>
                    <ToggleSwitch mode={mode} setMode={setMode} />
                    <AuthComponent mode={mode}
                                   setIsAuthenticated={setIsAuthenticated}
                                   setUser={setUser}
                                   Connection={Connection}
                                   setRequests={setRequests}
                                   setConnection={setConnection} />
                </>
            )}
        </div>
    );

};

export default App;
