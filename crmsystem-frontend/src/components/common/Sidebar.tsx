import React, { useState } from 'react';
import Button from './Button';
import KeyboardArrowRightIcon from '@mui/icons-material/KeyboardArrowRight';
import KeyboardArrowDownIcon from '@mui/icons-material/KeyboardArrowDown';

interface SidebarItem {
  label: string;
  onClick: () => void;
  isActive?: boolean;
}

interface Category {
  label: string;
  members: SidebarItem[];
}

interface SidebarProps {
  title: string;
  categories: Category[];
}

const Sidebar: React.FC<SidebarProps> = ({ title, categories }) => {
  const [openCategories, setOpenCategories] = useState<number[]>([]); 

  const toggleCategory = (index: number) => {
    setOpenCategories((prev) =>
      prev.includes(index) ? prev.filter((i) => i !== index) : [...prev, index]
    );
  };

  return (
    <div className="flex flex-col h-full w-64 bg-white shadow-md">
      <div className="p-4">
        <h2 className="text-xl font-semibold">{title}</h2>
      </div>
      <nav className="mt-4">
        {categories.map((category, index) => (
          <div key={index}>
            <Button
              onClick={() => toggleCategory(index)}
              className="w-full ml-4 mb-2 text-left"
            >
              <span className="flex items-center">
                {openCategories.includes(index) ? (
                  <KeyboardArrowDownIcon/> 
                ) : (
                  <KeyboardArrowRightIcon/> 
                )}
              {category.label}
              </span>
            </Button>
            {openCategories.includes(index) && (
              <div className="flex flex-col ml-4 mr-2">
                {category.members.map((member, memberIndex) => (
                  <Button
                    key={memberIndex}
                    onClick={member.onClick}
                    className={`w-full mb-2 rounded-lg hover:bg-gray-200 ${member.isActive ? 'bg-green-300 text-green-900 hover:bg-green-400' : ''}`}
                  >
                    {member.label}
                  </Button>
                ))}
              </div>
            )}
          </div>
        ))}
      </nav>
    </div>
  );
};

export default Sidebar;