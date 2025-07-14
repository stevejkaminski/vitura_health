import React, { useContext, useState, useEffect } from 'react';
import type { Patient, Prescription } from '../types';
import { getPatients, getPrescriptions, createPrescription } from '../api';

// Context value interface for prescription-related data and actions
interface PrescriptionContextProps {
    patients: Patient[];
    prescriptions: Prescription[];
    refreshPrescriptions: () => void;
    addPrescription: (data: Omit<Prescription, 'id' | 'datePrescribed'>) => Promise<void>;
}

// Create the context for prescription management
export const PrescriptionContext = React.createContext<PrescriptionContextProps | undefined>(undefined);

// Provider component to supply prescription data and actions to children
export const PrescriptionProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
    const [patients, setPatients] = useState<Patient[]>([]);
    const [prescriptions, setPrescriptions] = useState<Prescription[]>([]);

    // Load initial data for patients and prescriptions on mount
    useEffect(() => {
        getPatients().then(setPatients);
        refreshPrescriptions();
    }, []);

    // Fetch and update the prescriptions list
    const refreshPrescriptions = () => {
        getPrescriptions().then(setPrescriptions);
    };

    // Add a new prescription and refresh the list
    const addPrescription = async (data: Omit<Prescription, 'id' | 'datePrescribed'>) => {
        await createPrescription(data);
        refreshPrescriptions();
    };

    return (
        <PrescriptionContext.Provider value={{ patients, prescriptions, refreshPrescriptions, addPrescription }}>
            {children}
        </PrescriptionContext.Provider>
    );
};

// Custom hook to access the prescription context
export const usePrescriptionContext = () => {
    const ctx = useContext(PrescriptionContext);
    if (!ctx) throw new Error('usePrescriptionContext must be used within PrescriptionProvider');
    return ctx;
};