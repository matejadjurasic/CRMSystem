import { useState, useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { AppDispatch, RootState } from '../store';
import { fetchUsers, createUser, updateUser, deleteUser } from '../store/slices/userSlice';
import { User, CreateUser } from '../lib/types';

export const useUsers = () => {
  const dispatch = useDispatch<AppDispatch>();
  const { users, loading, error, validationErrors } = useSelector((state: RootState) => state.users);
  const [newUser, setNewUser] = useState<CreateUser>({ name: '', email: '' });
  const [editingUser, setEditingUser] = useState<User | null>(null);

  useEffect(() => {
    dispatch(fetchUsers());
  }, [dispatch]);

  const handleCreateUser = () => {
    dispatch(createUser(newUser));
    setNewUser({ name: '', email: '' });
  };

  const handleUpdateUser = () => {
    if (editingUser) {
      dispatch(updateUser({ id: editingUser.id, userData: editingUser }));
      setEditingUser(null);
    }
  };

  const handleDeleteUser = (id: number) => {
    if (window.confirm('Are you sure you want to delete this user?')) {
      dispatch(deleteUser(id));
    }
  };

  const handleUserChange = (field: keyof CreateUser, value: string) => {
    setNewUser({ ...newUser, [field]: value });
  };

  const handleEditingUserChange = (field: keyof User, value: string) => {
    if (editingUser) {
      setEditingUser({ ...editingUser, [field]: value });
    }
  };

  return {
    users,
    loading,
    error,
    validationErrors,
    newUser,
    editingUser,
    setEditingUser,
    handleCreateUser,
    handleUpdateUser,
    handleDeleteUser,
    handleUserChange,
    handleEditingUserChange,
  };
};