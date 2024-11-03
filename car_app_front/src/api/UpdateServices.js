import axios from 'axios';

const UpdateService = async (serviceOrgId, orgId, serviceId, Price, Description) => {
    try {
        // Формируем строку запроса
        const params = new URLSearchParams({
            serviceOrgId,
            orgId,
            serviceId,
            Price,
            Description,
        }).toString();

        // Делаем запрос с параметрами в строке запроса
        const response = await axios.post(
            `https://localhost:6061/api/Organization/updateService?${params}`,
            null, // Передаем `null`, так как тело запроса пустое
            {
                headers: {
                    'Content-Type': 'application/json', // Необязательно, но можно оставить
                },
            }
        );

        return response.data;
    } catch (error) {
        console.error('Ошибка при обновлении услуги:', error.response ? error.response.data : error.message);
        throw error;
    }
};

export default UpdateService;
