import React from 'react';
import { usePrescriptionContext } from '../context/PrescriptionContext';

// Props interface for PatientList (not used in this version, but useful for extension)
//type Patient = {
//    id: number;
//    fullName: string;
//    dateOfBirth: string;
//};
//interface PatientListProps {
//    patients: Patient[];
//    onSelect: (patient: Patient) => void;
//}

// PatientList component displays a list of patients from context
const PatientList: React.FC = () => {
    // Get patients array from PrescriptionContext
    const { patients } = usePrescriptionContext();
    return (
        <div className="bg-white rounded-lg shadow p-6">

            {/* Section title */}
            <h2 className="text-2xl font-bold mb-4 text-gray-800">Patients</h2>
            {/* Render each patient as a clickable list item */}
            <ul>
                {patients.map(p => (
                    <li
                        key={p.id}
                        className="p-3 mb-2 rounded hover:bg-blue-100 cursor-pointer transition"
                        //onClick={() => onselect(p)}
                    >
                        {/* Display patient's full name */}
                        <span className="font-medium text-gray-700">{p.fullName}</span>
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default PatientList;