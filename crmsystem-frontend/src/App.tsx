import React from 'react';
import './App.css';
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import RequireAuth from './components/auth/RequireAuth';
import AdminDash from './components/dashboards/AdminDash';
import UserDash from './components/dashboards/UserDash';
import { DashboardRedirect } from './components/common/DashboardRedirect';
import { USER_ROLES, ROUTES } from './lib/constants';
import ChangePassword from './components/auth/ChangePassword';
import { useAuth } from './hooks/useAuth';
import Login from './components/auth/Login';

function App() {
  const { token, user } = useAuth();
  return (
    <Router>
      <Routes>
        {/* Protected Routes */}
        <Route
          path={ROUTES.ADMIN_DASHBOARD}
          element={
            <RequireAuth allowedRoles={[USER_ROLES.ADMIN]}>
              <AdminDash />
            </RequireAuth>
          }
        />
        <Route
          path={ROUTES.USER_DASHBOARD}
          element={
            <RequireAuth allowedRoles={[USER_ROLES.CLIENT]}>
              <UserDash />
            </RequireAuth>
          }
        />
        {/* Public Routes */}
        <Route
          path={ROUTES.LOGIN}
          element={!token ? <Login /> :<DashboardRedirect token={token} user={user} />}
        />
        <Route 
          path={ROUTES.CHANGE_PASSWORD} 
          element={<ChangePassword />} 
        />
        {/* Root path redirect */}
        <Route 
          path="/" 
          element={<DashboardRedirect token={token} user={user} />} 
        />
        {/* Catch-all Route */}
        <Route 
          path="*" 
          element={<Navigate to="/" replace />} 
        />
      </Routes>
    </Router>
  );
}

export default App;
