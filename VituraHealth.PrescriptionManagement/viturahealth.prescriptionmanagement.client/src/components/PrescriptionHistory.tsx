import React, { useState } from 'react';
import { usePrescriptionContext } from '../context/PrescriptionContext';

// PrescriptionHistory component displays a list of prescriptions with filtering and sorting
const PrescriptionHistory: React.FC = () => {
    // Get prescriptions and patients from context
    const { prescriptions, patients } = usePrescriptionContext();

    // State for filter input and sort order
    const [filter, setFilter] = useState('');
    const [sortAsc, setSortAsc] = useState(true);

    // Filter and sort prescriptions based on user input
    const filtered = prescriptions
        .filter(p => {
            // Find the patient for each prescription
            const patient = patients.find(pt => pt.id === p.patientId);
            // Filter by patient name or drug name
            return (
                patient?.fullName.toLowerCase().includes(filter.toLowerCase()) ||
                p.drugName.toLowerCase().includes(filter.toLowerCase())
            );
        })
        .sort((a, b) => sortAsc
            // Sort by prescription date ascending or descending
            ? new Date(a.datePrescribed).getTime() - new Date(b.datePrescribed).getTime()
            : new Date(b.datePrescribed).getTime() - new Date(a.datePrescribed).getTime()
        );

    return (
        <div className="bg-white rounded-lg shadow p-6 max-w-2xl mx-auto">
            {/* Title */}
            <h2 className="text-2xl font-bold mb-4 text-gray-800">Prescription History</h2>
            {/* Filter and sort controls */}
            <div className="flex flex-col sm:flex-row gap-2 mb-4">
                <input
                    type="text"
                    placeholder="Filter by patient or drug"
                    value={filter}
                    onChange={e => setFilter(e.target.value)}
                    className="flex-1 border border-gray-300 rounded px-3 py-2 focus:outline-none focus:ring-2 focus:ring-blue-400"
                />
                <button
                    onClick={() => setSortAsc(!sortAsc)}
                    className="bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700 transition"
                >
                    Sort by Date {sortAsc ? '▲' : '▼'}
                </button>
            </div>
            <ul className="divide-y divide-gray-200">
                {filtered.map(p => {
                    const patient = patients.find(pt => pt.id === p.patientId);
                    return (
                        <li key={p.id} className="py-3">
                            <span className="font-medium text-gray-700">{patient?.fullName}</span>
                            <span className="text-gray-500"> – {p.drugName} ({p.dosage})</span>
                            <span className="block text-sm text-gray-400">
                                {new Date(p.datePrescribed).toLocaleString()}
                            </span>
                        </li>
                    );
                })}
            </ul>
        </div>
    );
};

export default PrescriptionHistory;