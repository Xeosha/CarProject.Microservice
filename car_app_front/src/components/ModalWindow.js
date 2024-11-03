import React from "react";

function ModalWindow({ modalMessage, setIsModalOpen }) {

    const closeModal = () => {
        setIsModalOpen(false);
    };

    return (
        <div className="confirmation-modal">
            <button className="close-modal" onClick={closeModal}>✖</button>
            <div className="modal-content">
                <p>{modalMessage}</p>
            </div>
        </div>
    );
}

export default ModalWindow;
