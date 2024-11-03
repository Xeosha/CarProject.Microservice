import React, { useEffect, useState } from 'react';
import Services from '../organization/Services';
import Bookings from '../organization/Bookings';
import FormUpdateService from "../organization/FormUpdateService";
import FormCreateService from "../organization/FormCreateService";
import FormDeleteService from "../organization/FormDeleteService";

const OrganizationPage = ({ user, connection, requests, setRequests }) => {
    const [services, setServices] = useState([]);
    const organizationId = user.id;
    const [isFormVisibleCreate, setIsFormVisibleCreate] = useState(false);
    const [isFormVisibleUpdate, setIsFormVisibleUpdate] = useState(false);
    const [isFormVisibleDelete, setIsFormVisibleDelete] = useState(false);



    const handleShowFormCreate = () => {
        setIsFormVisibleCreate(true);
    };
    const handleHideFormCreate = () => {
        setIsFormVisibleCreate(false);
    };
    const handleFormSubmitCreate = () => {
        console.log("Форма успешно отправлена");
        handleHideFormCreate();
    };

    const handleShowFormUpdate = () => {
        setIsFormVisibleUpdate(true);
    };
    const handleHideFormUpdate = () => {
        setIsFormVisibleUpdate(false);
    };
    const handleFormSubmitUpdate = () => {
        console.log("Форма успешно отправлена");
        handleHideFormUpdate();
    };

    const handleShowFormDelete = () => {
        setIsFormVisibleDelete(true);
    };
    const handleHideFormDelete = () => {
        setIsFormVisibleDelete(false);
    };
    const handleFormSubmitDelete = () => {
        console.log("Форма успешно отправлена");
        handleHideFormDelete();
    };



    return (
        <div>
            <h1>Услуги вашей организации</h1>
            <Services organizationID={organizationId} services={services} setServices={setServices}/>
            <h2>Запросы на запись</h2>
            <Bookings organizationId={organizationId} connection={connection} requests={requests} setRequests={setRequests}/>
            <button onClick={handleShowFormCreate}>Добавить новую услугу</button>
            <button onClick={handleShowFormUpdate}>Редактировать услугу</button>
            <button onClick={handleShowFormDelete}>Удалить услугу</button>
            {isFormVisibleCreate && (
                <div className="confirmation-modal">
                    <div className="confirmation-modal button">
                        <FormCreateService
                            onFormSubmit={handleFormSubmitCreate}
                            onCancel={handleHideFormCreate}
                        />
                    </div>
                </div>
            )}
            {isFormVisibleUpdate && (
                <div className="confirmation-modal">
                    <div className="confirmation-modal button">
                        <FormUpdateService
                            onFormSubmit={handleFormSubmitUpdate}
                            onCancel={handleHideFormUpdate}
                        />
                    </div>
                </div>
            )}
            {isFormVisibleDelete && (
                <div className="confirmation-modal">
                    <div className="confirmation-modal button">
                        <FormDeleteService
                            onFormSubmit={handleFormSubmitDelete}
                            onCancel={handleHideFormDelete}
                        />
                    </div>
                </div>
            )}
        </div>
    );
};

export default OrganizationPage;
