import React from 'react';
import './App.css';
import { BrowserRouter as Router, Routes, Route, Link, Navigate } from 'react-router-dom';
import RequireAuth from './components/auth/RequireAuth';
import Login from './components/auth/Login';
import { useAuth } from './hooks/useAuth';
import AdminDash from './components/dashboards/AdminDash';

function App() {
  const { token } = useAuth();
  return (
    <Router>
      <Routes>
        {/* Protected Route */}
        <Route
          path="/"
          element={
            <RequireAuth>
              <AdminDash />
            </RequireAuth>
          }
      />
      {/* Public Routes */}
      <Route
        path="/login"
        element={!token ? <Login /> : <Navigate to="/" replace />}
      />
      {/* Catch-all Route */}
      <Route path="*" element={<Navigate to="/" replace />} />
      </Routes>
    </Router>
  );
}

export default App;
