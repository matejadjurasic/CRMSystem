import { useState, useEffect } from 'react';
import { useNavigate, useLocation } from 'react-router-dom';
import { useDispatch } from 'react-redux';
import { AppDispatch } from '../store';
import { resetPassword, logout } from '../store/slices/authSlice';
import { useAuth } from './useAuth';
import { getParamValue } from '../lib/utils';

export const useChangePassword = () => {
  const [newPassword, setNewPassword] = useState('');
  const [confirmPassword, setConfirmPassword] = useState('');
  const [token, setToken] = useState<string | null>(null);
  const [email, setEmail] = useState<string | null>(null);

  const navigate = useNavigate();
  const location = useLocation();
  const dispatch = useDispatch<AppDispatch>();

  const { loading, error, successMessage } = useAuth();

  useEffect(() => {
    const search = location.search;
    const tokenParam = getParamValue(search, 'token');
    const emailParam = getParamValue(search, 'email');

    if (tokenParam && emailParam) {
      setToken(tokenParam);
      setEmail(emailParam);
    }
  }, [location]);

  useEffect(() => {
    if (successMessage) {
        dispatch(logout());
        navigate('/login');
    }
  }, [successMessage, dispatch, navigate]);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    if (newPassword !== confirmPassword) {
      alert('Passwords do not match');
      return;
    }

    if (!token || !email) {
      alert('Missing token or email');
      return;
    }

    dispatch(resetPassword({ token, email, newPassword }));
  };

  return {
    newPassword,
    setNewPassword,
    confirmPassword,
    setConfirmPassword,
    loading,
    error,
    successMessage,
    handleSubmit,
  };
};