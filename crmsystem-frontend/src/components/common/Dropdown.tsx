import React, { ReactNode } from 'react';

interface DropdownProps {
  isOpen: boolean;
  onClose: () => void;
  children: ReactNode;
  direction?: 'left' | 'right';
}

const Dropdown: React.FC<DropdownProps> = ({ isOpen, onClose, children, direction = 'right' }) => {
  if (!isOpen) return null;

  return (
    <div 
        className={`absolute mt-2 w-48 bg-white border rounded shadow-lg z-10 ${direction === 'left' ? 'left-0' : 'right-0'}`} 
        onClick={onClose}>
        {children}
    </div>
  );
};

export default Dropdown;