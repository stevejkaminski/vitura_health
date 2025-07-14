import React, { useState } from 'react';
import { usePrescriptionContext } from '../context/PrescriptionContext';
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

// NewPrescriptionForm component allows users to create a new prescription
const NewPrescriptionForm: React.FC = () => {
    // Get patients and addPrescription function from context
    const { patients, addPrescription } = usePrescriptionContext();
    // Local state for form fields and error handling
    const [patientId, setPatientId] = useState<number>(0);
    const [drugName, setDrugName] = useState('');
    const [dosage, setDosage] = useState('');
    const [error, setError] = useState('');

    // Handles form submission and validation
    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        // Validate required fields
        if (!patientId || !drugName.trim() || !dosage.trim()) {
            setError('All fields are required.');
            return;
        }
        try {
            // Attempt to add prescription via context
            await addPrescription({ patientId: Number(patientId), drugName, dosage });
            toast.success('Prescription saved successfully!');
            // Reset form fields and error
            setPatientId(0);
            setDrugName('');
            setDosage('');
            setError('');
        } catch (error) {
            // Handle errors and show toast notification
            toast.error('Failed to save prescription. Please try again.');
        }
        
    };

    return (
        <>
        {/* Prescription creation form */}
        <form onSubmit={handleSubmit}
            className="bg-white rounded-lg shadow p-6 max-w-md mx-auto space-y-4"
        >
            <h2 className="text-2xl font-bold mb-4 text-gray-800">New Prescription</h2>
                <div>
                    {/* Patient selection dropdown */}
                    <label htmlFor="patientName" className="block text-gray-700 font-medium mb-1">
                        Patient Name
                    </label>
                    {error && <div style={{ color: 'red' }}>{error}</div>}
                    <select value={patientId} onChange={e => setPatientId(Number(e.target.value))}>
                        <option value={0}>Select Patient</option>
                        {patients.map(p => (
                            <option key={p.id} value={p.id}>{p.fullName}</option>
                        ))}
                    </select>
                </div>
                <div>
                    {/* Drug name input */}
                    <label htmlFor="medication" className="block text-gray-700 font-medium mb-1">
                        Medication
                    </label>
                    <input
                        type="text"
                        placeholder="Drug"
                        value={drugName}
                        onChange={e => setDrugName(e.target.value)}
                    />
                </div>
                <div>
                    {/* Dosage input */}
                    <label htmlFor="dosage" className="block text-gray-700 font-medium mb-1">
                        Dosage
                    </label>
                    <input
                        type="text"
                        placeholder="Dosage"
                        value={dosage}
                        onChange={e => setDosage(e.target.value)}
                    />
                </div>
                {/* Submit button */}
                <button 
                    type="submit"
                    className="w-full bg-blue-600 text-white font-semibold py-2 rounded hover:bg-blue-700 transition"
                >
                    Create Prescription
                </button>
            </form>
            {/* Toast notifications container */}
            <ToastContainer />
        </>
    );
};

export default NewPrescriptionForm;