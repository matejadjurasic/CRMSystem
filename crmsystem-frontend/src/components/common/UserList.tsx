import React from 'react';
import UserCard from './UserCard';
import { User } from '../../lib/types';

interface UserListProps {
  users: User[];
  onEdit: (user: User) => void;
  onDelete: (id: number) => void;
}

const UserList: React.FC<UserListProps> = ({ users, onEdit, onDelete }) => (
  <div className="max-h-screen overflow-y-auto">
    {users.map((user) => (
      <UserCard
        key={user.id}
        user={user}
        onEdit={() => onEdit(user)}
        onDelete={() => onDelete(user.id)}
      />
    ))}
  </div>
);

export default UserList;