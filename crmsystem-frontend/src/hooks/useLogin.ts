import { useState } from 'react';
import { useDispatch } from 'react-redux';
import { AppDispatch } from '../store';
import { login } from '../store/slices/authSlice';
import { useAuth } from './useAuth';

export const useLogin = () => {
  const dispatch = useDispatch<AppDispatch>();
  const { loading, error, validationErrors } = useAuth();
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    dispatch(login({ email, password }));
  };

  return {
    email,
    setEmail,
    password,
    setPassword,
    loading,
    error,
    validationErrors,
    handleSubmit,
  };
};