/// <reference types="@testing-library/jest-dom" />
import { render, screen } from '@testing-library/react';
import PatientList from '../components/PatientList';

// Mock context
const patients = [
    { id: 1, fullName: 'John Doe', dateOfBirth: '1990-01-02' },
    { id: 2, fullName: 'Jane Smith', dateOfBirth: '1990-02-02' }
];

jest.mock('../context/PrescriptionContext', () => ({
    usePrescriptionContext: () => ({
        patients,
        prescriptions: [],
        refreshPrescriptions: jest.fn(),
        addPrescription: jest.fn()
    })
}));

describe('PatientList', () => {
    it('renders patient names', () => {
        render(<PatientList />);
        expect(screen.getByText('John Doe')).toBeInTheDocument();
        expect(screen.getByText('Jane Smith')).toBeInTheDocument();
    });
});