//asdf
import React from "react";

const GetAvailableTimes = async () => {
    try {
        const response = await fetch('https://localhost:6060/api/User/getAvailableTimes');
        const data = await response.json();

        if (Array.isArray(data)) {
            return data;
        } else {
            console.error('Получены некорректные данные:', data);
        }
    } catch (error) {
        console.error('Ошибка загрузки услуг:', error);
    }
    return [];
};

export default GetAvailableTimes;