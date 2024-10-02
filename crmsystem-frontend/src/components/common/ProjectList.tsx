import React from 'react';
import ProjectCard from './ProjectCard';
import { Project } from '../../lib/types';

interface ProjectListProps {
  projects: Project[];
  onEdit: (project: Project) => void;
  onDelete: (id: number) => void;
}

const ProjectList: React.FC<ProjectListProps> = ({ projects, onEdit, onDelete }) => (
  <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
    {projects.map((project) => (
      <ProjectCard
        key={project.id}
        project={project}
        onEdit={() => onEdit(project)}
        onDelete={() => onDelete(project.id)}
      />
    ))}
  </div>
);

export default ProjectList;