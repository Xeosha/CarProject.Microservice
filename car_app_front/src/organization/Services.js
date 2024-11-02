import React from 'react';

const Services = ({ services }) => {
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
