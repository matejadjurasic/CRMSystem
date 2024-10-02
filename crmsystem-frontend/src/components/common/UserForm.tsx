import React from 'react';
import Input from './Input';
import Button from './Button';
import { User, CreateUser } from '../../lib/types';

interface UserFormProps {
  user: User | CreateUser;
  onChange: (field: keyof (User | CreateUser), value: string) => void;
  onSubmit: () => void;
  isLoading: boolean;
  submitText: string;
}

const UserForm: React.FC<UserFormProps> = ({ user, onChange, onSubmit, isLoading, submitText }) => (
  <form onSubmit={(e) => { e.preventDefault(); onSubmit(); }} className="space-y-4">
    <Input
      label="Name"
      value={user.name}
      onChange={(e) => onChange('name', e.target.value)}
      required
    />
    <Input
      label="Email"
      type="email"
      value={user.email}
      onChange={(e) => onChange('email', e.target.value)}
      required
    />
    <Button type="submit" disabled={isLoading} className="w-full">
      {isLoading ? 'Loading...' : submitText}
    </Button>
  </form>
);

export default UserForm;