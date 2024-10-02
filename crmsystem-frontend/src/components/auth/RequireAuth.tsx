import React from 'react';
import { Navigate, useLocation } from 'react-router-dom';
import { useAuth } from '../../hooks/useAuth';
import { USER_ROLES } from '../../lib/constants';

interface RequireAuthProps {
  children: React.ReactElement;
  allowedRoles?: USER_ROLES[];
}

export default function RequireAuth({ children ,allowedRoles}: RequireAuthProps) {
  const { token ,user} = useAuth();
  const location = useLocation();

  if (!token) {
    return <Navigate to="/login" state={{ from: location }} replace />;
  }

  if (allowedRoles && !allowedRoles.some(role => user?.roles.includes(role))) {
    return <Navigate to="/unauthorized" replace />;
  }

  return children;
}