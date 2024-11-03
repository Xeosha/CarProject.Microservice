import React, { useState } from "react";
import CreateService from '../api/CreateService';
// Компонент формы
const FormCreateService = ({ onFormSubmit, onCancel }) => {
    const [orgId, setOrgId] = useState('3a6b5438-c8b3-4c0e-9ad4-8662347c30ac');
    const [serviceId, setServiceId] = useState('');
    const [price, setPrice] = useState('');
    const [description, setDescription] = useState('');
    const handleSubmit = async (event) => {
        event.preventDefault();
        try {
            const priceNumber = parseFloat(price); // parseFloat  для преобразования в  number
            const response = await CreateService(orgId, serviceId, priceNumber, description);
            if (response.success) {
                console.log("Услуга успешно создана:", response);
                onFormSubmit();
            } else {
                console.error("Ошибка создания услуги:", response.error);
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
            <button type="submit">Создать</button>
            <button type="button" onClick={onCancel}>Отмена</button>
        </form>
    );
};
export default FormCreateService;