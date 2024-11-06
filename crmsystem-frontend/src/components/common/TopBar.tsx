import React, { useState, useEffect, useRef } from 'react';
import Button from './Button';
import Dropdown from './Dropdown';

interface TopBarProps {
  workspaceName: string;
  onEditUser: () => void;
  onLogout: () => void;
  onGraphOption: () => void;
  isGraphView: boolean; 
  onToggleView: () => void; 
}

const TopBar: React.FC<TopBarProps> = ({ workspaceName, onEditUser, onLogout, onGraphOption, isGraphView, onToggleView }) => {
  const [isUserMenuOpen, setUserMenuOpen] = useState(false);
  const [isWorkspaceMenuOpen, setWorkspaceMenuOpen] = useState(false);
  const userMenuRef = useRef<HTMLDivElement>(null);
  const workspaceMenuRef = useRef<HTMLDivElement>(null);

  const toggleUserMenu = () => setUserMenuOpen((prev) => !prev);
  const toggleWorkspaceMenu = () => setWorkspaceMenuOpen((prev) => !prev);

  const handleClickOutside = (event: MouseEvent) => {
    if (userMenuRef.current && !userMenuRef.current.contains(event.target as Node)) {
      setUserMenuOpen(false);
    }
    if (workspaceMenuRef.current && !workspaceMenuRef.current.contains(event.target as Node)) {
      setWorkspaceMenuOpen(false);
    }
  };

  useEffect(() => {
    document.addEventListener('mousedown', handleClickOutside);
    return () => {
      document.removeEventListener('mousedown', handleClickOutside);
    };
  }, []);

  return (
    <div className="flex justify-between items-center bg-gray-100 shadow-md p-4">
      <div className="relative" ref={workspaceMenuRef}>
        <Button 
            onClick={isGraphView ? onToggleView : toggleWorkspaceMenu} 
            className="text-lg font-semibold">
            {isGraphView ? 'Back to Projects' : workspaceName}
        </Button>
        <Dropdown isOpen={isWorkspaceMenuOpen && !isGraphView} onClose={() => setWorkspaceMenuOpen(false)} direction='left'>
            <Button 
                onClick={onGraphOption} 
                className="block w-full text-left px-4 py-2 hover:bg-gray-200">
                Graph
            </Button>
        </Dropdown>
      </div>

      <div className="relative" ref={userMenuRef}>
        <Button 
            onClick={toggleUserMenu} 
            className="flex items-center">
            <div className="w-8 h-8 rounded-full bg-gray-300 flex items-center justify-center text-white">
                U 
            </div>
        </Button>
        <Dropdown isOpen={isUserMenuOpen} onClose={() => setUserMenuOpen(false)} direction='right'>
            <Button 
                onClick={onEditUser} 
                className="block w-full text-left px-4 py-2 hover:bg-gray-200">
                Edit User
            </Button>
            <Button 
                onClick={onLogout} 
                className="text-red-600 block w-full text-left px-4 py-2 hover:bg-gray-200">
                Logout
            </Button>
        </Dropdown>

      </div>
    </div>
  );
};

export default TopBar;