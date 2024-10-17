import { USER_ROLES } from "./constants";

export type User = {
    id: number;
    email: string;
    name: string;
    roles: USER_ROLES[];
    token: string;
}

export type CreateUser = {
    name: string;
    email: string;
}

export type UserState = {
    users : User[];
    loading: boolean;
    error: string | null;
    validationErrors: Record<string, string[]> | null;
}

export type Project = {
    id: number;
    title: string;
    description: string;
    deadline: string;
    status: 'Open' | 'InProgress' | 'Completed' | 'Cancelled';
}

export type ProjectState = {
    projects: Project[];
    allProjects: Project[];
    loading: boolean;
    error: string | null;
    validationErrors: Record<string, string[]> | null;
}

export type AuthState = {
    user: User | null;
    token: string | null;
    loading: boolean;
    error: string | null;
    validationErrors: Record<string, string[]> | null;
    successMessage: string | null;
}
  
export type LoginCredentials = {
    email: string;
    password: string;
}
  
export type APIError = {
    errorMessage: string;
    validationErrors?: Record<string, string[]>;
}