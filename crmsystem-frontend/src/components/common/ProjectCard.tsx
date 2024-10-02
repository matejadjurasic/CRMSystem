import React from 'react';
import Button from '../common/Button';
import { Project } from '../../lib/types';

interface ProjectCardProps {
  project: Project;
  onEdit: () => void;
  onDelete: () => void;
}

const ProjectCard: React.FC<ProjectCardProps> = ({ project, onEdit, onDelete }) => (
  <div className="bg-white p-4 rounded shadow">
    <h3 className="text-lg font-semibold">{project.title}</h3>
    <p className="text-sm text-gray-600 mb-2">{project.description}</p>
    <p className="text-sm">Deadline: {new Date(project.deadline).toLocaleDateString()}</p>
    <p className="text-sm">Status: {project.status}</p>
    <div className="mt-4">
      <Button onClick={onEdit} className="mr-2">Edit</Button>
      <Button onClick={onDelete}>Delete</Button>
    </div>
  </div>
);

export default ProjectCard;