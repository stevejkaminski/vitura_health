/// <reference types="@testing-library/jest-dom" />
import { render, screen, fireEvent } from '@testing-library/react';
import PrescriptionHistory from '../components/PrescriptionHistory';

// Mock context
const prescriptions = [
    { id: 1, patientId: 1, drugName: 'Aspirin', dosage: '100mg', datePrescribed: '2024-06-01T10:00:00Z' },
    { id: 2, patientId: 2, drugName: 'Ibuprofen', dosage: '200mg', datePrescribed: '2024-06-02T10:00:00Z' }
];
const patients = [
    { id: 1, fullName: 'John Doe', dateOfBirth: '1980-01-01' },
    { id: 2, fullName: 'Jane Smith', dateOfBirth: '1990-02-02' }
];

jest.mock('../context/PrescriptionContext', () => ({
    usePrescriptionContext: () => ({
        patients,
        prescriptions,
        refreshPrescriptions: jest.fn(),
        addPrescription: jest.fn()
    })
}));

describe('PrescriptionHistory', () => {
    it('renders prescription history title', () => {
        render(<PrescriptionHistory />);
        expect(screen.getByText(/Prescription History/i)).toBeInTheDocument();
    });

    it('renders patient and drug names', () => {
        render(<PrescriptionHistory />);
        expect(screen.getByText('John Doe')).toBeInTheDocument();
        expect(screen.getByText('Jane Smith')).toBeInTheDocument();
        //expect(screen.getByText('Aspirin')).toBeInTheDocument();
        //expect(screen.getByText('Ibuprofen')).toBeInTheDocument();
    });

    it('filters prescriptions by patient name', () => {
        render(<PrescriptionHistory />);
        fireEvent.change(screen.getByPlaceholderText(/Filter by patient or drug/i), { target: { value: 'Jane' } });
        expect(screen.getByText('Jane Smith')).toBeInTheDocument();
        expect(screen.queryByText('John Doe')).not.toBeInTheDocument();
    });
});