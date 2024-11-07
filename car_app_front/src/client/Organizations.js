import React, { useState, useEffect } from 'react';
import GetAvailableTimes from "../api/GetAvailableTimes";

function Organizations({ organizations, services, selectedService,setSelectedService, selectedOrganization, setSelectedOrganization, setTimeSlot }) {
    const [filteredOrganizations, setFilteredOrganizations] = useState(organizations);
    const [searchTerm, setSearchTerm] = useState('');
    const [minPrice, setMinPrice] = useState('');
    const [maxPrice, setMaxPrice] = useState('');
    const [sortOrder, setSortOrder] = useState('default');
    const [hoveredOrg, setHoveredOrg] = useState(null);

    useEffect(() => {
        filterAndSortOrganizations();
    }, [searchTerm, minPrice, maxPrice, sortOrder, organizations]);

    const handleSearchChange = (e) => {
        setSearchTerm(e.target.value.toLowerCase());
    };

    const handlePriceChange = (e, type) => {
        const value = e.target.value;
        type === 'min' ? setMinPrice(value) : setMaxPrice(value);
    };

    const handleSortChange = (e) => {
        setSortOrder(e.target.value);
    };

    const filterAndSortOrganizations = () => {
        let filtered = organizations.filter(org => {
            const matchesSearch = org.name.toLowerCase().includes(searchTerm) || org.address.toLowerCase().includes(searchTerm);
            const matchesMinPrice = !minPrice || org.price >= parseFloat(minPrice);
            const matchesMaxPrice = !maxPrice || org.price <= parseFloat(maxPrice);
            return matchesSearch && matchesMinPrice && matchesMaxPrice;
        });

        switch (sortOrder) {
            case 'priceAsc':
                filtered.sort((a, b) => a.price - b.price);
                break;
            case 'priceDesc':
                filtered.sort((a, b) => b.price - a.price);
                break;
            case 'nameAsc':
                filtered.sort((a, b) => a.name.localeCompare(b.name));
                break;
            case 'nameDesc':
                filtered.sort((a, b) => b.name.localeCompare(a.name));
                break;
            default:
                break;
        }

        setFilteredOrganizations(filtered);
    };

    const handleOrganizationSelect = async (organizationId) => {
        setSelectedOrganization(organizationId);

        const service = services.find(item =>
            item.idOrganization === organizationId && item.idService === selectedService.idService
        );
        setSelectedService(service);
        let data = [];
        if (service) {
            data = await GetAvailableTimes({ service });
        } else {
            console.warn('Сервис для выбранной организации и услуги не найден.');
        }

        setTimeSlot(data);
    };

    return (
        <div>
            <div style={{ display: 'flex', gap: '10px', marginTop: '10px' }}>
                <input
                    type="text"
                    placeholder="Поиск по названию или адресу"
                    value={searchTerm}
                    onChange={handleSearchChange}
                />
                <input
                    type="number"
                    placeholder="Мин. цена"
                    value={minPrice}
                    onChange={(e) => handlePriceChange(e, 'min')}
                />
                <input
                    type="number"
                    placeholder="Макс. цена"
                    value={maxPrice}
                    onChange={(e) => handlePriceChange(e, 'max')}
                />
                <select value={sortOrder} onChange={handleSortChange}>
                    <option value="default">По умолчанию</option>
                    <option value="priceAsc">По возрастанию цены</option>
                    <option value="priceDesc">По убыванию цены</option>
                    <option value="nameAsc">По названию (А-Я)</option>
                    <option value="nameDesc">По названию (Я-А)</option>
                </select>
            </div>
            <h3>Организации, предоставляющие {selectedService.serviceName}:</h3>
            <ul>
                <li className={`organization-item`} style={{background: "#9ef3a0"}}>
                    <div className="organization-item-content">
                        <span>Название</span>
                        <span>Цена</span>
                        <span>Адрес</span>
                    </div>
                </li>
                {filteredOrganizations.length > 0 ? (
                    filteredOrganizations.map((org) => (
                        <li
                            key={org.id}
                            onMouseEnter={() => setHoveredOrg(org.id)}
                            onMouseLeave={() => setHoveredOrg(null)}
                            onClick={() => handleOrganizationSelect(org.id)}
                            className={`organization-item ${selectedOrganization === org.id ? 'selected' : ''} ${hoveredOrg === org.id ? 'hovered' : ''}`}
                        >
                            <div className="organization-item-content">
                                <span>{org.name}</span>
                                <span>{org.price} руб.</span>
                                <span>{org.address}</span>
                            </div>
                        </li>
                    ))
                ) : (
                    <li key={0}>
                        <p>Не найдены подходящие организации</p>
                    </li>
                )}
            </ul>
        </div>
    );
}

export default Organizations;
