import React, {useEffect, useState} from "react";
import UpdateService from '../api/UpdateServices';
import ShowServices from "../api/ShowServices";
// Компонент формы
const FormUpdateService = ({ onFormSubmit, onCancel, orgId, services }) => {
    const [price, setPrice] = useState('');
    const [description, setDescription] = useState('');
    const [serviceOrgId, setServiceOrgId] = useState();
    const [serviceId, setServiceId] = useState(services[0].id); // Устанавливаем id первой услуги по умолчанию
    const [servicesOrg, setServicesOrg] = useState([]);

    useEffect(() => {
        const fetchServices = async () => {
            const organizationID = orgId;
            const services = await ShowServices({ organizationID });
            setServicesOrg(services);
            setServiceOrgId(services[0].id)
        };
        fetchServices();
    }, [orgId]);
    const handleSubmit = async (event) => {
        event.preventDefault();
        try {
            const priceNumber = parseFloat(price); // parseFloat  для преобразования в  number
            const response = await UpdateService(serviceOrgId, orgId, serviceId, priceNumber, description);
            if (!response) {
                console.log("Услуга успешно обновлена:", response);
                onFormSubmit();
            } else {
                console.error("Ошибка обновления услуги:", response.error);
                // Обработайте ошибку
            }
        } catch (error) {
            console.error("Error creating service:", error);
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
                    {servicesOrg.map((service) => (
                        <option key={service.id} value={service.id}>
                            {service.serviceName + " " + service.price + " " + service.description}
                        </option>
                    ))}
                </select>
            </div>
            <div>
                <label htmlFor="serviceId">Service ID:</label>
                <select
                    id="serviceId"
                    value={serviceId}
                    onChange={(e) => setServiceId(e.target.value)}
                >
                    {services.map((service) => (
                        <option key={service.id} value={service.id}>
                            {service.name}
                        </option>
                    ))}
                </select>
            </div>
            <div>
                <label htmlFor="price">Price:</label>
                <input
                    type="number"
                    id="price"
                    value={price}
                    onChange={(e) => setPrice(e.target.value)}
                />
            </div>
            <div>
                <label htmlFor="description">Description:</label>
                <textarea
                    type="text"
                    id="description"
                    value={description}
                    onChange={(e) => setDescription(e.target.value)}
                />
            </div>
            <button type="submit">Редактировать</button>
            <button type="button" onClick={onCancel}>Отмена</button>
        </form>
    );
};
export default FormUpdateService;