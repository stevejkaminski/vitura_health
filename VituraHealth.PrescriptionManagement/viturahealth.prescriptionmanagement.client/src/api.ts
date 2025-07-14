// API utility functions for interacting with the backend
import type { Patient, Prescription } from './types';

// Base URL for API endpoints
const API_BASE = import.meta.env.VITE_API_BASE || 'http://localhost:5299/api';

// Function to handle API responses and throw errors for non-OK responses
async function handleResponse<T>(res: Response): Promise<T> {
    if (!res.ok) {
        // Try to parse error message from response
        let errorMsg = 'An error occurred';
        try {
            const data = await res.json();
            errorMsg = data.message || JSON.stringify(data);
        } catch {
            errorMsg = await res.text();
        }
        throw new Error(errorMsg);
    }
    return res.json();
}

// Fetch the list of patients from the backend
export async function getPatients(): Promise<Patient[]> {
    const res = await fetch(`${API_BASE}/patients`);
    return handleResponse<Patient[]>(res);
}

// Fetch the list of prescriptions from the backend
export async function getPrescriptions(): Promise<Prescription[]> {
    const res = await fetch(`${API_BASE}/prescriptions`);
    return handleResponse<Prescription[]>(res);
}

// Create a new prescription by sending a POST request to the backend
export async function createPrescription(prescription: Omit<Prescription, 'id' | 'datePrescribed'>): Promise<Prescription> {
    const res = await fetch(`${API_BASE}/prescriptions`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(prescription),
    });
    return handleResponse<Prescription>(res);
}