import React from 'react';
import Input from '../common/Input';
import Button from '../common/Button';
import { Project } from '../../lib/types';

interface ProjectFormProps {
  project: Omit<Project, 'id'>;
  onChange: (field: keyof Omit<Project, 'id'>, value: string) => void;
  onSubmit: () => void;
  isLoading: boolean;
  submitText: string;
}

const ProjectForm: React.FC<ProjectFormProps> = ({ project, onChange, onSubmit, isLoading, submitText }) => (
  <>
    <Input
      label="Title"
      value={project.title}
      onChange={(e) => onChange('title', e.target.value)}
    />
    <Input
      label="Description"
      value={project.description}
      onChange={(e) => onChange('description', e.target.value)}
    />
    <Input
      label="Deadline"
      type="date"
      value={project.deadline.split('T')[0]}
      onChange={(e) =>{
        const value = e.target.value;
        onChange('deadline', value ? value : new Date().toISOString().split('T')[0]);
      } }
      required
    />
    <select
      className="border rounded px-2 py-1 w-full mt-2"
      value={project.status}
      onChange={(e) => onChange('status', e.target.value as Project['status'])}
    >
      <option value="Open">Open</option>
      <option value="InProgress">In Progress</option>
      <option value="Completed">Completed</option>
      <option value="Cancelled">Cancelled</option>
    </select>
    <Button onClick={onSubmit} disabled={isLoading} variant='primary'>
      {isLoading ? 'Loading...' : submitText}
    </Button>
  </>
);

export default ProjectForm;