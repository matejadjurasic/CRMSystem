import React from 'react';
import Button from './Button';
import { User } from '../../lib/types';

interface UserCardProps {
  user: User;
  onEdit: () => void;
  onDelete: () => void;
}

const UserCard: React.FC<UserCardProps> = ({ user, onEdit, onDelete }) => (
  <div className="bg-white p-4 rounded shadow">
    <h3 className="text-lg font-semibold">{user.name}</h3>
    <p className="text-sm text-gray-600 mb-2">{user.email}</p>
    <p className="text-sm">Roles: {user.roles ? user.roles.join(', ') : 'No assigned roles'}</p>
    <div className="mt-4">
      <Button onClick={onEdit} className="mr-2">Edit</Button>
      <Button onClick={onDelete}>Delete</Button>
    </div>
  </div>
);

export default UserCard;