import React from "react";

const ToggleSwitch = ({mode, setMode}) => {
    return (
        <div className="flex-container">
            <button
                className={mode === 'client' ? 'active-button' : ''}
                onClick={() => setMode('client')}
                disabled={mode === 'client'} // Делаем кнопку неактивной
            >
                Клиент
            </button>
            <button
                className={mode === 'organization' ? 'active-button' : ''}
                onClick={() => setMode('organization')}
                disabled={mode === 'organization'} // Делаем кнопку неактивной
            >
                Организация
            </button>
        </div>
    );
};

export default ToggleSwitch;