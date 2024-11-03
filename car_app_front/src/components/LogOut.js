import React from "react";

const ToggleSwitch = ({setIsAuthenticated, setUser, setMode}) => {

    // Функция для выхода из профиля
    const handleLogout = () => {
        localStorage.removeItem('user'); // Удаляем данные о пользователе из localStorage
        setIsAuthenticated(false); // Обновляем состояние авторизации
        setUser(null); // Удаляем информацию о пользователе
        setMode('client'); // Сбрасываем режим на клиентский
    };


    return (
        <div>
            <button onClick={handleLogout}>Выйти</button>
        </div>
    );
};

export default ToggleSwitch;