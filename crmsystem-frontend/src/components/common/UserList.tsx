import React from 'react';
import UserCard from './UserCard';
import { User } from '../../lib/types';

interface UserListProps {
  users: User[];
  onEdit: (user: User) => void;
  onDelete: (id: number) => void;
}

const UserList: React.FC<UserListProps> = ({ users, onEdit, onDelete }) => (
  <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
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