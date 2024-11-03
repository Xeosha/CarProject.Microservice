import React, { useState } from "react";
import DeleteService from '../api/DeleteService';

// Компонент формы
const FormDeleteService = ({ onFormSubmit, onCancel }) => {
  const [serviceOrgId, setServiceOrgId] = useState('');

  const handleSubmit = async (event) => {
    event.preventDefault();

    try {
      const response = await DeleteService(serviceOrgId);
      if (response.success) {
        console.log("Услуга успешно удалена:", response);
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
      <input
        type="text"
        id="serviceOrgId"
        value={serviceOrgId}
        onChange={(e) => setServiceOrgId(e.target.value)}
      />
    </div>
    <button type="submit">Удалить</button>
    <button type="button" onClick={onCancel}>Отмена</button>
    </form>
  );
};

export default FormDeleteService;