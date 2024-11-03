import React from 'react';
import GetServiceOrgIds from "../api/GetServiceOrgIds";

const Services = ({ organizationID, services, setServices }) => {
    GetServiceOrgIds({organizationID, setServices});
    return (
        <div>
            {services.map(service => (
                <div key={service.id}>
                    <h3>{service.name}</h3>
                    <p>{service.description}</p>
                    <p>Время: {service.time}</p>
                    <p>Цена: {service.price}</p>
                </div>
            ))}
        </div>
    );
};

export default Services;
