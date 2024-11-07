import React from "react";

const GetNameServices = async () => {
    try {
        const response = await fetch('https://localhost:6061/api/Catalog/getNameServices');
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

export default GetNameServices;
