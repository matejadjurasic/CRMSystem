import { useState, useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { AppDispatch, RootState } from '../store';
import { fetchProjects, fetchAllProjects, createProject, updateProject, deleteProject, clearErrors } from '../store/slices/projectSlice';
import { Project } from '../lib/types';

export const useProjects = (isAdmin = false) => {
  const dispatch = useDispatch<AppDispatch>();
  const { projects, allProjects, loading, error, validationErrors } = useSelector((state: RootState) => state.projects);
  const [newProject, setNewProject] = useState<Omit<Project, 'id'>>({
    title: '',
    description: '',
    deadline: new Date().toISOString().split('T')[0],
    status: 'Open'
  });
  const [editingProject, setEditingProject] = useState<Project | null>(null);

  useEffect(() => {
    dispatch(fetchProjects());
    if (isAdmin) {
      dispatch(fetchAllProjects());
    }
    return () => {
      dispatch(clearErrors());
    };
  }, [dispatch, isAdmin]);

  const handleCreateProject = () => {
    dispatch(createProject(newProject));
    setNewProject({ title: '', description: '', deadline: new Date().toISOString().split('T')[0], status: 'Open' });
  };

  const handleUpdateProject = () => {
    if (editingProject) {
      dispatch(updateProject({ id: editingProject.id, projectData: editingProject }));
      setEditingProject(null);
    }
  };

  const handleDeleteProject = (id: number) => {
    if (window.confirm('Are you sure you want to delete this project?')) {
      dispatch(deleteProject(id));
    }
  };

  const handleProjectChange = (field: keyof Omit<Project, 'id'>, value: string) => {
    setNewProject({ ...newProject, [field]: value });
  };

  const handleEditingProjectChange = (field: keyof Omit<Project, 'id'>, value: string) => {
    if (editingProject) {
      setEditingProject({ ...editingProject, [field]: value });
    }
  };

  return {
    projects,
    allProjects,
    loading,
    error,
    validationErrors,
    newProject,
    editingProject,
    setEditingProject,
    handleCreateProject,
    handleUpdateProject,
    handleDeleteProject,
    handleProjectChange,
    handleEditingProjectChange,
  };
};