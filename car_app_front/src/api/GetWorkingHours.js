//rdtfgh
import React from "react";

const GetWorkingHours = async () => {
    try {
        const response = await fetch('https://localhost:6061/api/Catalog/getWorkingHours');
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

export default GetWorkingHours;