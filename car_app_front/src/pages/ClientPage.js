import React, { useState } from 'react';
import Services from "../client/Services";
import Organizations from "../client/Organizations";
import TimeSlot from "../client/TimeSlot";
import BookingModal from "../client/BookingModal";
import Booking from "../api/Booking";
import ModalWindow from "../components/ModalWindow";
import Bookings from "../client/Bookings";



const ClientPage = ({ user, connection, requests, setRequests }) => {
    const [services, setServices] = useState([]);
    const [selectedService, setSelectedService] = useState(null);
    const [organizations, setOrganizations] = useState([]);
    const [selectedOrganization, setSelectedOrganization] = useState(null);
    const [confirmationSlot, setConfirmationSlot] = useState(null);
    const [timeSlot, setTimeSlot] = useState([]);
    const [isModalOpen, setIsModalOpen] = useState(false);
    const [modalMessage, setModalMessage] = useState('');



    const toBook = async (description) => {
        if (connection && selectedOrganization && confirmationSlot && selectedService) {
            try {
                // Call the Bookings.js function to send the booking request
                await Booking({
                    connection,
                    selectedOrganization,
                    confirmationSlot,
                    selectedService,
                    description
                });

                // Show success message in the modal
                setModalMessage('Запрос на бронирование успешно отправлен!');
                setIsModalOpen(true);
            } catch (error) {
                // Show error message in the modal
                setModalMessage('Не удалось отправить запрос на бронирование.');
                setIsModalOpen(true);
                console.error('Ошибка бронирования:', error);
            }

            // Clear selections and time slots
            setTimeSlot([]);
            setSelectedOrganization(null);
            setConfirmationSlot(null);
        } else {
            console.warn("Данные для бронирования неполные.");
        }
    };




    // Обработка закрытия модальных окон
    const closeConfirmationModal = () => {
        setConfirmationSlot(null);
    };




    return (
        <div>
            <h2>Добро пожаловать, {user.fullName}</h2>


            {/* Отображение списка услуг в виде прямоугольников */}
            <h3>Выберите услугу:</h3>
            <Services services={services}
                      setServices={setServices}
                      selectedService={selectedService}
                      setSelectedService={setSelectedService}
                      setOrganizations={setOrganizations}/>

            {/* Поиск организаций */}
            {selectedService && (
                <div>
                    <h3>Поиск организаций:</h3>
                    <Organizations organizations={organizations}
                                   services={services}
                                   selectedService={selectedService}
                                   selectedOrganization={selectedOrganization}
                                   setSelectedOrganization={setSelectedOrganization}
                                   setTimeSlot={setTimeSlot}/>
                </div>
            )}

            {/* Модальное окно для выбора временных слотов */}
            {selectedOrganization && (
                <TimeSlot timeSlot={timeSlot}
                          setTimeSlot={setTimeSlot}
                          selectedService={selectedService}
                          selectedOrganization={selectedOrganization}
                          setConfirmationSlot={setConfirmationSlot}
                          setSelectedOrganization={setSelectedOrganization}
                          closeConfirmationModal={closeConfirmationModal}/>
            )}

            {/* Окно подтверждения бронирования */}
            {confirmationSlot && (
                <BookingModal confirmationSlot={confirmationSlot}
                              closeConfirmationModal={closeConfirmationModal}
                              toBook={toBook}/>
            )}


            {/* Modal for booking status */}
            {isModalOpen && (
                <ModalWindow setIsModalOpen={setIsModalOpen}
                    modalMessage={modalMessage}/>
            )}
            <h2>Запросы на запись</h2>
            <Bookings clientId={user.id} connection={connection} requests={requests} setRequests={setRequests}/>
        </div>
    );
};

export default ClientPage;