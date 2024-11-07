import React, {useEffect, useState} from "react";
import DeleteService from '../api/DeleteServices';
import ShowServices from "../api/ShowServices";
// Компонент формы
const FormDeleteService = ({ onFormSubmit, onCancel, organizationID }) => {
    const [serviceOrgId, setServiceOrgId] = useState();
    const [services, setServices] = useState([]);

    useEffect(() => {
        const fetchServices = async () => {
            const services = await ShowServices({ organizationID });
            setServices(services);
            setServiceOrgId(services[0].id)
        };
        fetchServices();
    }, [organizationID]);
    const handleSubmit = async (event) => {
        event.preventDefault();
        try {
            const response = await DeleteService(serviceOrgId);
            if (!response) {
                onFormSubmit();
            } else {
                console.error("Ошибка удаления услуги:", response.error);
                // Обработайте ошибку
            }
        } catch (error) {
            console.error("Error deleting service:", error);
            // Обработайте ошибку
        }
    };
    return(
        <form onSubmit={handleSubmit}>
            <div>
                <label htmlFor="serviceOrgId">ServiceOrg ID:</label>
                <select
                    id="serviceOrgId"
                    value={serviceOrgId}
                    onChange={(e) => setServiceOrgId(e.target.value)}
                >
                    {services.map((service) => (
                        <option key={service.id} value={service.id}>
                            {service.serviceName + " " + service.price + " " + service.description}
                        </option>
                    ))}
                </select>
            </div>
            <button type="submit">Удалить</button>
            <button type="button" onClick={onCancel}>Отмена</button>
        </form>
    );
};
export default FormDeleteService;