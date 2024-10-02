import React from 'react';
import Input from '../common/Input';
import Button from '../common/Button';

interface LoginFormProps {
  email: string;
  setEmail: (value: string) => void;
  password: string;
  setPassword: (value: string) => void;
  onSubmit: (e: React.FormEvent) => void;
  isLoading: boolean;
}

export const LoginForm: React.FC<LoginFormProps> = ({
  email,
  setEmail,
  password,
  setPassword,
  onSubmit,
  isLoading,
}) => (
  <form onSubmit={onSubmit} className="space-y-4">
    <Input
      label="Email"
      type="email"
      id="email"
      value={email}
      onChange={(e) => setEmail(e.target.value)}
      required
    />
    <Input
      label="Password"
      type="password"
      id="password"
      value={password}
      onChange={(e) => setPassword(e.target.value)}
      required
    />
    <Button type="submit" isLoading={isLoading}>
      Login
    </Button>
  </form>
);