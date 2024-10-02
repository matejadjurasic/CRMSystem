import React from 'react';
import { Navigate } from 'react-router-dom';
import { USER_ROLES, ROUTES } from '../../lib/constants';
import { User } from '../../lib/types';

interface DashboardRedirectProps {
    token: string | null;
    user: User | null;
  }

export const DashboardRedirect = ({ token, user }: DashboardRedirectProps): JSX.Element => {

  if (!token) return <Navigate to={ROUTES.LOGIN} replace />;
  if (user?.roles.includes(USER_ROLES.ADMIN)) {
    return <Navigate to={ROUTES.ADMIN_DASHBOARD} replace />;
  }
  if (user?.roles.includes(USER_ROLES.CLIENT)) {
    return <Navigate to={ROUTES.USER_DASHBOARD} replace />;
  }
  return <Navigate to={ROUTES.UNAUTHORIZED} replace />;
};