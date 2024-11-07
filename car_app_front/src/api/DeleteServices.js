import axios from 'axios';

const DeleteService = async (serviceOrgId) => {
    try {
        const response = await axios.post(
            "https://localhost:6061/api/Organization/deleteService",
            JSON.stringify(serviceOrgId), // Отправляем строку как тело
            {
                headers: {
                    'Content-Type': 'application/json', // Устанавливаем правильный заголовок
                },
            }
        );
        return response.data;
    } catch (error) {
        console.error('Login error:', error.response ? error.response.data : error.message);
        throw error;
    }
};

export default DeleteService;
