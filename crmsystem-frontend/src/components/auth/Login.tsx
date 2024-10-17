import React, { useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { useLogin } from '../../hooks/useLogin';
import { useAuth } from '../../hooks/useAuth';
import { LoginForm } from '../common/LoginForm';
import ErrorDisplay from '../common/ErrorDisplay';
import { getDashboardRoute } from '../../lib/utils';

export default function Login() {
  const navigate = useNavigate();
  const { token, user, successMessage } = useAuth();
  const {
    email,
    setEmail,
    password,
    setPassword,
    loading,
    error,
    validationErrors,
    handleSubmit,
  } = useLogin();

  useEffect(() => {
    if (token) {
      const route = getDashboardRoute(user);
      navigate(route, { replace: true });
    }
  }, [token, navigate, user]);

  return (
    <div className="flex items-center justify-center min-h-screen bg-gray-100">
      <div className="w-full max-w-md p-8 space-y-4 bg-white rounded-lg shadow-lg">
        <h2 className="text-3xl font-semibold text-center text-gray-900">Welcome Back</h2>
        <ErrorDisplay error={error} validationErrors={validationErrors} />
        {successMessage && <p className="text-green-500">{successMessage}</p>}
        <LoginForm
          email={email}
          setEmail={setEmail}
          password={password}
          setPassword={setPassword}
          onSubmit={handleSubmit}
          isLoading={loading}
        />
      </div>
    </div>
  );
}