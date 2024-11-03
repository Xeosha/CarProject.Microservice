import React, { useState } from "react";
import UpdateService from '../api/UpdateServices';
import GetServices from '../api/GetServices';
// Компонент формы
const FormUpdateService = ({ onFormSubmit, onCancel }) => {
    const [serviceOrgId, setServiceOrgId] = useState('6408aaa9-d97f-4034-803f-4ceff1763fa9');
    const [orgId, setOrgId] = useState('3a6b5438-c8b3-4c0e-9ad4-8662347c30ac');
    const [serviceId, setServiceId] = useState('f909acb4-c7e0-49ec-8eb3-173396806840');
    const [price, setPrice] = useState('');
    const [description, setDescription] = useState('');
    const handleSubmit = async (event) => {
        event.preventDefault();
        try {
            const priceNumber = parseFloat(price); // parseFloat  для преобразования в  number
            const response = await UpdateService(serviceOrgId, orgId, serviceId, priceNumber, description);
            if (response.success) {
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
                <label htmlFor="serviceId">Service ID:</label>
                <input
                    type="text"
                    id="serviceId"
                    value={serviceId}
                    onChange={(e) => setServiceId(e.target.value)}
                />
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