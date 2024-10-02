export enum USER_ROLES {
    ADMIN = 'Admin',
    CLIENT = 'Client',
}
  
export enum PROJECT_STATUSES {
    OPEN = 'Open',
    IN_PROGRESS = 'In Progress',
    COMPLETED = 'Completed',
    CANCELLED = 'Cancelled',
}
  
export const ROUTES = {
    LOGIN: '/login',
    ADMIN_DASHBOARD: '/admin',
    USER_DASHBOARD: '/user',
    UNAUTHORIZED: '/unauthorized',
    CHANGE_PASSWORD: '/reset-password',
};