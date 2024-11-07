import React, { useState } from "react";
import CreateService from '../api/CreateService';


const FormCreateService = ({ orgId, onFormSubmit, onCancel, services }) => {
    const [serviceId, setServiceId] = useState(services[0].id); // Устанавливаем id первой услуги по умолчанию
    const [price, setPrice] = useState('');
    const [description, setDescription] = useState('');

    const handleSubmit = async (event) => {
        event.preventDefault();
        try {
            const priceNumber = parseFloat(price); // Преобразуем строку в число
            const response = await CreateService(orgId, serviceId, priceNumber, description);
            if (!response) {
                console.log("Услуга успешно создана:", response);
                onFormSubmit();
            } else {
                console.error("Ошибка создания услуги:", response.error);
            }
        } catch (error) {
            console.error("Error creating service:", error);
        }
    };

    return (
        <form onSubmit={handleSubmit}>
            <div>
                <label htmlFor="serviceId">Service:</label>
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
