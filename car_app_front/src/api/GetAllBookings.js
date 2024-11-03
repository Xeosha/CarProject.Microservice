import React from "react";

const GetAllBookings = async ({userId, mode}) => {
    try {
        let response;
        if (mode === 'organization') {
            response = await fetch(`https://localhost:6060/api/Organization/getAllBookings?organizationId=${userId}`);
        }
        else {
            response = await fetch(`https://localhost:6060/api/User/getAllBookings?userId=${userId}`);
        }

        console.log(response);

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

export default GetAllBookings;