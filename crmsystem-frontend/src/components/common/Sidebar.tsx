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
  bottomButtons?: SidebarItem[];
}

const Sidebar: React.FC<SidebarProps> = ({ title, items, bottomButtons }) => (
  <div className="flex flex-col justify-between h-full w-64 bg-white shadow-md">
    <div className="p-4">
      <h2 className="text-xl font-semibold">{title}</h2>
    </div>
    <nav className="mt-4">
      {items.map((item, index) => (
        <Button
          key={index}
          onClick={item.onClick}
          className={`w-full mb-2 ${item.isActive ? 'bg-gray-300' : ''}`}
        >
          {item.label}
        </Button>
      ))}
    </nav>
    {bottomButtons && (
      <div className="mt-auto p-4 border-t">
        {bottomButtons.map((button, index) => (
          <Button
            key={index}
            onClick={button.onClick}
            variant='danger'
          >
            {button.label}
          </Button>
        ))}
      </div>
    )}
  </div>
);

export default Sidebar;