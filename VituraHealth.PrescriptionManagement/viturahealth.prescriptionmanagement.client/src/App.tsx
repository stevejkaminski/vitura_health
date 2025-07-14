import React, { useState } from 'react';
import { PrescriptionProvider } from './context/PrescriptionContext';
import PatientList from './components/PatientList';
import NewPrescriptionForm from './components/NewPrescriptionForm';
import PrescriptionHistory from './components/PrescriptionHistory';

const App: React.FC = () => {
    const [view, setView] = useState('patients');
    return (
        <PrescriptionProvider>
            <header className="flex flex-col items-center py-4 bg-blue-800 shadow-sm">
                <img
                    src="/Vitura-White.png"
                    alt="Vitura Health"
                    className="h-12 w-auto mb-2 sm:h-16 bg-blue-800"
                    style={{ objectFit: 'contain' }}
                />
            </header>
            <nav className="bg-blue-600 p-4 flex gap-4 justify-center">
                <button
                    onClick={() => setView('patients')}
                    className={`text-white px-4 py-2 rounded ${view === 'patients' ? 'bg-blue-800' : 'hover:bg-blue-700'} transition`}
                >
                    Patient List
                </button>
                <button
                    onClick={() => setView('new')}
                    className={`text-white px-4 py-2 rounded ${view === 'new' ? 'bg-blue-800' : 'hover:bg-blue-700'} transition`}
                >
                    New Prescription
                </button>
                <button
                    onClick={() => setView('history')}
                    className={`text-white px-4 py-2 rounded ${view === 'history' ? 'bg-blue-800' : 'hover:bg-blue-700'} transition`}
                >
                    Prescription History
                </button>
            </nav>
            <main className="p-6 bg-gray-50 min-h-screen">
                {view === 'patients' && <PatientList />}
                {view === 'new' && <NewPrescriptionForm />}
                {view === 'history' && <PrescriptionHistory />}
            </main>
        </PrescriptionProvider>
    );
};

export default App;