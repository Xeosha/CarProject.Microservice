import React from "react";
import GetServices from "./GetServices";

const GetServiceOrgIds = ({organizationID}) => {
    const getServiceOrgIds = async () => {
        try {
            const response = await fetch(`https://localhost:6061/api/Organization/getServiceOrgIds?organizationId=${organizationID}`);
            const data = await response.json();

            if (Array.isArray(data)) {
                return(data);
            } else {
                console.error('Получены некорректные данные:', data);
            }
        } catch (error) {
            console.error('Ошибка загрузки услуг:', error);
        }
    };

    const allServices = GetServices();
    const servicesOrg = getServiceOrgIds();
    return allServices.filter(service => servicesOrg.includes(service.id));;
}
export default GetServiceOrgIds;