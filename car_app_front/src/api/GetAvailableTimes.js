//asdf
import React from "react";

const GetAvailableTimes = async ({service}) => {

        try {
            const response = await fetch(`https://localhost:6060/api/User/getAvailableTimes?organizationServiceId=${service.id}`);
            const data = await response.json();
            if (Array.isArray(data)) {
                return data;
            } else {
                return ["empty"];
            }
        } catch (error) {
            console.error('Ошибка загрузки временных слотов:', error);
        }
    return [];
};

export default GetAvailableTimes;