import React from 'react';
import { useChangePassword } from '../../hooks/useChangePassword';
import { ChangePasswordForm } from '../common/ChangePasswordForm';
import ErrorDisplay from '../common/ErrorDisplay';

export default function ChangePassword() {
  const {
    newPassword,
    setNewPassword,
    confirmPassword,
    setConfirmPassword,
    loading,
    error,
    successMessage,
    handleSubmit,
  } = useChangePassword();

  return (
    <div className="max-w-md mx-auto mt-8">
      <h2 className="text-2xl font-bold mb-4">Change Password</h2>
      <ErrorDisplay error={error} />
      {successMessage && <p className="text-green-500 mb-4">{successMessage}</p>}
      <ChangePasswordForm
        newPassword={newPassword}
        setNewPassword={setNewPassword}
        confirmPassword={confirmPassword}
        setConfirmPassword={setConfirmPassword}
        onSubmit={handleSubmit}
        isLoading={loading}
      />
    </div>
  );
}