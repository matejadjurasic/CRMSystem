import React, { useState, useEffect } from 'react';
import { useNavigate, useLocation } from 'react-router-dom';
import { useDispatch, useSelector } from 'react-redux';
import { AppDispatch, RootState } from '../../store';
import { resetPassword, logout } from '../../store/slices/authSlice';
import Input from '../common/Input';
import Button from '../common/Button';

const ChangePassword: React.FC = () => {
  const [newPassword, setNewPassword] = useState('');
  const [confirmPassword, setConfirmPassword] = useState('');
  const [token, setToken] = useState<string | null>(null);
  const [email, setEmail] = useState<string | null>(null);

  const navigate = useNavigate();
  const location = useLocation();
  const dispatch = useDispatch<AppDispatch>();

  const { loading, error, successMessage } = useSelector((state: RootState) => state.auth);

  useEffect(() => {
    const searchParams = new URLSearchParams(location.search);
    const tokenParam = searchParams.get('token');
    const emailParam = searchParams.get('email');

    if (tokenParam && emailParam) {
      setToken(tokenParam);
      setEmail(emailParam);
    }
  }, [location]);

  useEffect(() => {
    if (successMessage) {
      setTimeout(() => {
        dispatch(logout());
        navigate('/login');
      }, 3000);
    }
  }, [successMessage, dispatch, navigate]);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    if (newPassword !== confirmPassword) {
      // You might want to set this error in the Redux store instead
      alert('Passwords do not match');
      return;
    }

    if (!token || !email) {
      // You might want to set this error in the Redux store instead
      alert('Missing token or email');
      return;
    }

    dispatch(resetPassword({ token, email, newPassword }));
  };

  return (
    <div className="max-w-md mx-auto mt-8">
      <h2 className="text-2xl font-bold mb-4">Change Password</h2>
      {error && <p className="text-red-500 mb-4">{error}</p>}
      {successMessage && <p className="text-green-500 mb-4">{successMessage}</p>}
      <form onSubmit={handleSubmit}>
        <Input
          label="New Password"
          type="password"
          id="newPassword"
          value={newPassword}
          onChange={(e) => setNewPassword(e.target.value)}
          required
        />
        <Input
          label="Confirm New Password"
          type="password"
          id="confirmPassword"
          value={confirmPassword}
          onChange={(e) => setConfirmPassword(e.target.value)}
          required
        />
        <Button type="submit" isLoading={loading}>
          Change Password
        </Button>
      </form>
    </div>
  );
};

export default ChangePassword;
