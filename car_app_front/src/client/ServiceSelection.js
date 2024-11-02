function ServiceSelection({ onSelect }) {
    const services = [
        { id: 1, name: 'Service 1', imageUrl: 'service1.jpg' },
        { id: 2, name: 'Service 2', imageUrl: 'service2.jpg' },
        // Add more services
    ];

    return (
        <div className="service-selection">
            {services.map(service => (
                <div key={service.id} onClick={() => onSelect(service.id)}>
                    <img src={service.imageUrl} alt={service.name} />
                    <p>{service.name}</p>
                </div>
            ))}
        </div>
    );
}
