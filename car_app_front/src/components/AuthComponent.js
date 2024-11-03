import React, { useState } from 'react';
import clients from "../constant/clients";
import organizations from "../constant/organizations";

const AuthComponent = ({ mode, setIsAuthenticated, setUser, Connection, setRequests, setConnection }) => {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');

    // Функция для обработки логина
    const onLogin = (event) => {
        event.preventDefault(); // предотвращаем перезагрузку страницы

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

        if (authenticatedUser) {
            setIsAuthenticated(true);
            setUser(authenticatedUser);

            // Сохраняем данные пользователя в localStorage
            localStorage.setItem('user', JSON.stringify({ ...authenticatedUser, type: mode }));
            Connection(authenticatedUser.id, mode, setConnection, setRequests); // устанавливаем соединение
        } else {
            alert('Неправильные логин или пароль');
        }
    };

    return (
        <div>
            <h2>Login</h2>
            <form onSubmit={onLogin}>
                <div>
                    <label>Username:</label>
                    <input
                        type="text"
                        value={username}
                        onChange={(e) => setUsername(e.target.value)}
                    />
                </div>
                <div>
                    <label>Password:</label>
                    <input
                        type="password"
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                    />
                </div>
                <button type="submit">Login</button>
            </form>
        </div>
    );
};


export default AuthComponent;
