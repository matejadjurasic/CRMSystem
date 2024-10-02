import React from 'react';
import Button from './Button';

interface SidebarItem {
  label: string;
  onClick: () => void;
  isActive?: boolean;
}

interface SidebarProps {
  title: string;
  items: SidebarItem[];
}

const Sidebar: React.FC<SidebarProps> = ({ title, items }) => (
  <div className="w-64 bg-white shadow-md">
    <div className="p-4">
      <h2 className="text-xl font-semibold">{title}</h2>
    </div>
    <nav className="mt-4">
      {items.map((item, index) => (
        <Button
          key={index}
          onClick={item.onClick}
          className={`w-full mb-2 ${item.isActive ? 'bg-blue-600' : ''}`}
        >
          {item.label}
        </Button>
      ))}
    </nav>
  </div>
);

export default Sidebar;