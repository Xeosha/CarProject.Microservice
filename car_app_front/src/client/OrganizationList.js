import React, { useState } from 'react';

function OrganizationList({ serviceId }) {
    const [searchTerm, setSearchTerm] = useState('');

    const organizations = [
        // Mocked data with services
    ];

    const filteredOrganizations = organizations.filter(org =>
        org.name.includes(searchTerm) ||
        org.services.some(service => service.name.includes(searchTerm))
    );

    return (
        <div>
            <input
                type="text"
                placeholder="Search organizations or services"
                value={searchTerm}
                onChange={(e) => setSearchTerm(e.target.value)}
            />
            <div className="organization-list">
                {filteredOrganizations.map(org => (
                    <div key={org.id}>
                        <h3>{org.name}</h3>
                        <ul>
                            {org.services.map(service => (
                                <li key={service.id}>{service.name}</li>
                            ))}
                        </ul>
                    </div>
                ))}
            </div>
            <MapComponent organizations={filteredOrganizations} />
        </div>
    );
}
