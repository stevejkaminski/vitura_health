// src/types.ts

// Prescription type definition
export interface Prescription {
  id: number;
  patientId: number;
  drugName: string;
  dosage: string;
  datePrescribed: string;
}

// Patient type definition
export interface Patient {
  id: number;
  fullName: string;
  dateOfBirth: string; // ISO date string
}

// API Response type for paginated lists
export interface PaginatedResponse<T> {
  items: T[];
  total: number;
  page: number;
  pageSize: number;
}   