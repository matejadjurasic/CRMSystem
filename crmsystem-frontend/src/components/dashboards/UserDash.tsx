import React from 'react'
import { useDispatch } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../../hooks/useAuth';
import { logout } from '../../store/slices/authSlice';

export default function UserDash() {

  const dispatch = useDispatch();
  const navigate = useNavigate();
  const { user } = useAuth();

  const handleLogout = () => {
    dispatch(logout());
    navigate('/login', { replace: true });
  };
  return (
    <div>UserDash</div>
  )
}
