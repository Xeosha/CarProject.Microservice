import React from 'react';
import ReactDOM from 'react-dom/client'; // измените импорт
import App from './pages/App';
import './styles/index.css';

const root = ReactDOM.createRoot(document.getElementById('root')); // используем createRoot
root.render(
    <React.StrictMode>
        <App />
    </React.StrictMode>
);
