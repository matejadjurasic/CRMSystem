import React, { useState, useEffect } from 'react';
import { useDispatch } from 'react-redux';
import { login } from '../../store/slices/authSlice';
import { AppDispatch } from '../../store';
import { useNavigate } from 'react-router-dom';
import Button from '../common/Button';
import { useAuth } from '../../hooks/useAuth';
import Input from '../common/Input';

export default function Login() {
  const dispatch = useDispatch<AppDispatch>();
  const navigate = useNavigate();
  const { loading, error, validationErrors, token,successMessage } = useAuth();
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');

  useEffect(() => {
    if (token) {
      navigate('/', { replace: true });
    }
  }, [token, navigate]);

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    dispatch(login({ email, password }));
  };

  return (
    <div className="flex items-center justify-center min-h-screen bg-gray-100">
      <div className="w-full max-w-md p-8 space-y-4 bg-white rounded shadow-md">
        <h2 className="text-2xl font-bold text-center text-gray-800">Login</h2>
        {error && <p className="text-red-500">{error}</p>}
        {validationErrors &&
          Object.entries(validationErrors).map(([field, messages]) => (
            <div key={field} className="text-red-500">
              <strong>{field}:</strong> {messages.join(', ')}
            </div>
          ))}
        {successMessage && <p className="text-green-500">{successMessage}</p>}
        <form onSubmit={handleSubmit} className="space-y-4">
          {/* Email */}
          <Input
            label="Email"
            type="email"
            id="email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            required
          />
          {/* Password */}
          <Input
            label="Password"
            type="password"
            id="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            required
          />
          {/* Submit */}
          <Button type="submit" isLoading={loading}>
            Login
          </Button>
          <p className="text-sm text-center text-gray-600">
            Don't have an account?{' '}
            <a
              href="/register"
              className="text-blue-600 hover:underline hover:text-blue-800"
            >
              Register
            </a>
          </p>
        </form>
      </div>
    </div>
  );
}
