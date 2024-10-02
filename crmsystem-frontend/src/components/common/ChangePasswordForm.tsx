import React from 'react';
import Input from '../common/Input';
import Button from '../common/Button';

interface ChangePasswordFormProps {
  newPassword: string;
  setNewPassword: (value: string) => void;
  confirmPassword: string;
  setConfirmPassword: (value: string) => void;
  onSubmit: (e: React.FormEvent) => void;
  isLoading: boolean;
}

export const ChangePasswordForm: React.FC<ChangePasswordFormProps> = ({
  newPassword,
  setNewPassword,
  confirmPassword,
  setConfirmPassword,
  onSubmit,
  isLoading,
}) => (
  <form onSubmit={onSubmit}>
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
    <Button type="submit" isLoading={isLoading}>
      Change Password
    </Button>
  </form>
);