import React from 'react';
import Button from './Button';
import { User } from '../../lib/types';
import { USER_ROLES } from '../../lib/constants';

interface UserCardProps {
  user: User;
  onEdit: () => void;
  onDelete: () => void;
}

const UserCard: React.FC<UserCardProps> = ({ user, onEdit, onDelete }) => (
  <div
    className="bg-white hover:bg-gray-200 p-4 rounded-lg shadow-md flex items-center justify-between cursor-pointer mt-4"
    onClick={onEdit}
  >
    <div className="flex flex-col">
      <h3 className="text-lg font-semibold">{user.name}</h3>
      <p className="text-sm text-gray-600 line-clamp-1">{user.email}</p>
    </div>
    <div className="flex items-center space-x-4">
      <span
        className={`text-xs font-semibold px-2 py-1 rounded-full ${
          user.roles[0] === USER_ROLES.ADMIN ? 'bg-red-200 text-red-800' :
          user.roles[0] === USER_ROLES.CLIENT ? 'bg-blue-200 text-blue-800' :
          'bg-gray-200 text-gray-800' 
        }`}
      >
        {user.roles ? user.roles.join(', ') : 'No assigned roles'}
      </span>
      <Button 
        onClick={(event) => {
          event.stopPropagation(); 
          onDelete(); 
        }} 
        variant='danger'
      >
        Delete
      </Button>
    </div>
  </div>
);

export default UserCard;