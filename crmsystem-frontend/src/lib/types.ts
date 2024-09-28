export type User = {
    id: number;
    email: string;
    name: string;
    roles: string[];
    token: string;
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