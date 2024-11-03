import React, { useEffect, useState } from 'react';
import Services from '../organization/Services';
import Bookings from '../organization/Bookings';

const OrganizationPage = ({ user, connection, requests, setRequests }) => {
    const [services, setServices] = useState([]);
    const organizationId = user.id;


    return (
        <div>
            <h1>Услуги вашей организации</h1>
            <Services organizationID={organizationId} services={services} setServices={setServices}/>
            <h2>Запросы на запись</h2>
            <Bookings organizationId={organizationId} connection={connection} requests={requests} setRequests={setRequests}/>
        </div>
    );
};

export default OrganizationPage;
