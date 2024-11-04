
const ShowServices = async ({ organizationID }) => {
    try {
        // Получаем услуги организации
        const response = await fetch(`https://localhost:6061/api/Organization/showServices?orgId=${organizationID}`);
        const services = await response.json();

        if (!Array.isArray(services)) {
            console.error('Получены некорректные данные для услуг организации:', services);
            return [];
        }

        return services;
    } catch (error) {
        console.error('Ошибка загрузки услуг или фильтрации:', error);
    }
    return [];
};

export default ShowServices;
