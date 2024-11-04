import React, { useState } from 'react';
import GetAvailableTimes from "../api/GetAvailableTimes";

function Organizations({ organizations, services, selectedService, selectedOrganization, setSelectedOrganization, setTimeSlot }) {
    const [filteredOrganizations, setFilteredOrganizations] = useState(organizations);
    const [searchTerm, setSearchTerm] = useState('');
    const [hoveredOrg, setHoveredOrg] = useState(null); // Добавляем состояние для выделения организации


    // Фильтрация организаций по поисковому запросу
    const handleSearchChange = (e) => {
        const value = e.target.value.toLowerCase();
        setSearchTerm(value);

        const filtered = organizations.filter(org =>
            org.name.toLowerCase().includes(value) ||
            org.address.toLowerCase().includes(value)
        );

        setFilteredOrganizations(filtered);
    };


    // Запрос доступных временных слотов по выбранной организации
    const handleOrganizationSelect = async (organizationId) => {
        setSelectedOrganization(organizationId);

        // Находим элемент из services, соответствующий выбранной организации и услуге
        const service = services.find(item =>
            item.idOrganization === organizationId && item.idService === selectedService.idService
        );
        let data = [];
        if (service) {
            data = await GetAvailableTimes({service});
        } else {
            console.warn('Сервис для выбранной организации и услуги не найден.');
        }

        setTimeSlot(data);
    };

    return (
        <div>
            <input
                type="text"
                placeholder="Поиск по названию или адресу"
                value={searchTerm}
                onChange={handleSearchChange}
            />
            <h3>Организации, предоставляющие {selectedService.serviceName}:</h3>
            <ul>
                {filteredOrganizations.length > 0 ?
                    (filteredOrganizations.map((org) => (
                                <li
                                    key={org.id}
                                    onMouseEnter={() => setHoveredOrg(org.id)}
                                    onMouseLeave={() => setHoveredOrg(null)}
                                    onClick={() => handleOrganizationSelect(org.id)}
                                    className={`organization-item ${selectedOrganization === org.id ? 'selected' : ''} ${hoveredOrg === org.id ? 'hovered' : ''}`}
                                >
                                    <div className="organization-item-content">
                                        <span>{org.name}</span>
                                        <span>{org.address}</span>
                                    </div>
                                </li>
                            )
                        )
                    ) : (
                        <li
                            key={0}>
                            <p>Не найдены подходящие организации</p>
                        </li>
                    )
                }
            </ul>
        </div>
    );
}

export default Organizations;
