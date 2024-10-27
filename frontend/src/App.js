import React, { useState } from 'react';
import UserForm from './UserForm';
import OrganizationForm from './OrganizationForm';

function App() {
    const [view, setView] = useState(null);

    return (
        <div className="App">
            <h1>Booking Service</h1>
            <button onClick={() => setView('user')}>User Form</button>
            <button onClick={() => setView('organization')}>Organization Form</button>

            {view === 'user' && <UserForm />}
            {view === 'organization' && <OrganizationForm />}
        </div>
    );
}

export default App;
