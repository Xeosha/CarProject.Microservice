// GetServiceOrgIds.js
import GetServices from "./GetServices";

const GetServiceOrgIds = async ({ organizationID }) => {
    try {
        // Получаем услуги организации
        const response = await fetch(`https://localhost:6061/api/Organization/getServiceOrgIds?organizationId=${organizationID}`);
        const serviceOrgIds = await response.json();

        if (!Array.isArray(serviceOrgIds)) {
            console.error('Получены некорректные данные для услуг организации:', serviceOrgIds);
            return [];
        }

        // Получаем все услуги
        const allServices = await GetServices();

        // Фильтруем услуги
        const filteredServices = allServices.filter(service => serviceOrgIds.includes(service.id));
        return filteredServices;
    } catch (error) {
        console.error('Ошибка загрузки услуг или фильтрации:', error);
    }
    return [];
};

export default GetServiceOrgIds;
