import React from 'react';
import Button from '../common/Button';
import { Project } from '../../lib/types';

interface ProjectCardProps {
  project: Project;
  onEdit: () => void;
  onDelete: () => void;
}

const ProjectCard: React.FC<ProjectCardProps> = ({ project, onEdit, onDelete }) => (
  <div
    className="bg-white hover:bg-gray-200 p-4 rounded-lg shadow-md flex items-center justify-between cursor-pointer mt-4"
    onClick={onEdit}
  >
    <div className="flex flex-col">
      <h3 className="text-lg font-semibold">{project.title}</h3>
      <p className="text-sm text-gray-600 line-clamp-1">{project.description}</p>
    </div>
    <div className="flex items-center space-x-4">
      <span
        className={`text-xs font-semibold px-2 py-1 rounded-full ${
          project.status === 'Completed' ? 'bg-green-200 text-green-800' :
          project.status === 'Cancelled' ? 'bg-red-200 text-red-800' :
          project.status === 'InProgress' ? 'bg-yellow-200 text-yellow-800' :
          project.status === 'Open' ? 'bg-blue-200 text-blue-800' :
          'bg-gray-200 text-gray-800' 
        }`}
      >
        {project.status}
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

export default ProjectCard;