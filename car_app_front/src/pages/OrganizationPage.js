import React, { useEffect, useState } from 'react';
import Services from '../organization/Services';
import Bookings from '../organization/Bookings';
import FormUpdateService from "../organization/FormUpdateService";
import FormCreateService from "../organization/FormCreateService";
import FormDeleteService from "../organization/FormDeleteService";
import GetNameServices from "../api/GetNameServices";
import ModalWindow from "../components/ModalWindow";

const OrganizationPage = ({ user, connection }) => {
    const organizationId = user.id;
    const [isFormVisibleCreate, setIsFormVisibleCreate] = useState(false);
    const [isFormVisibleUpdate, setIsFormVisibleUpdate] = useState(false);
    const [isFormVisibleDelete, setIsFormVisibleDelete] = useState(false);
    const [allServices, setAllServices] = useState([]);
    const [isModalOpen, setIsModalOpen] = useState(false);
    const [servicesVersion, setServicesVersion] = useState(0); // состояние-триггер для обновления Services

    const fetchServices = async () => {

        const all = await GetNameServices();

        setAllServices(all);
        setServicesVersion(prev => prev + 1); // Обновляем servicesVersion
    };
    // Initial fetch of all bookings
    useEffect(() => {
        fetchServices();
    }, []);


    const handleShowFormCreate = () => {
        setIsFormVisibleCreate(true);
    };
    const handleHideFormCreate = () => {
        setIsFormVisibleCreate(false);
    };
    const handleFormSubmitCreate = () => {
        setIsModalOpen(true);
        handleHideFormCreate();
        fetchServices();
    };

    const handleShowFormUpdate = () => {
        setIsFormVisibleUpdate(true);
    };
    const handleHideFormUpdate = () => {
        setIsFormVisibleUpdate(false);
    };
    const handleFormSubmitUpdate = () => {
        setIsModalOpen(true);
        handleHideFormUpdate();
        fetchServices();
    };

    const handleShowFormDelete = () => {
        setIsFormVisibleDelete(true);
    };
    const handleHideFormDelete = () => {
        setIsFormVisibleDelete(false);
    };
    const handleFormSubmitDelete = () => {
        setIsModalOpen(true);
        handleHideFormDelete();
        fetchServices();
    };



    return (
        <div>
            <h1>Услуги вашей организации</h1>
            <Services organizationID={organizationId} servicesVersion={servicesVersion}/>
            <h2>Запросы на запись</h2>
            <Bookings organizationId={organizationId} connection={connection} servicesVersion={servicesVersion}/>
            <button onClick={handleShowFormCreate}>Добавить новую услугу</button>
            <button onClick={handleShowFormUpdate}>Редактировать услугу</button>
            <button onClick={handleShowFormDelete}>Удалить услугу</button>
            {isFormVisibleCreate && (
                <div className="confirmation-modal">
                    <div className="confirmation-modal button">
                        <FormCreateService
                            orgId={organizationId}
                            services={allServices}
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
                            orgId={organizationId}
                            services={allServices}
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
                            organizationID={organizationId}
                            onFormSubmit={handleFormSubmitDelete}
                            onCancel={handleHideFormDelete}
                        />
                    </div>
                </div>
            )}
            {isModalOpen &&
                (<ModalWindow modalMessage={"Форма успешно отправлена!"} setIsModalOpen={setIsModalOpen} />)
            }
        </div>
    );
};

export default OrganizationPage;
