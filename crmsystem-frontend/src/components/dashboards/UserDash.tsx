import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { useDispatch } from 'react-redux';
import { AppDispatch } from '../../store';
import { logout } from '../../store/slices/authSlice';
import { useProjects } from '../../hooks/useProjects';
import Modal from '../common/Modal';
import Sidebar from '../common/Sidebar';
import ProjectList from '../common/ProjectList';
import ProjectForm from '../common/ProjectForm';
import ErrorDisplay from '../common/ErrorDisplay';
import { useAuth } from '../../hooks/useAuth';

export default function UserDash() {
  const dispatch = useDispatch<AppDispatch>();
  const navigate = useNavigate();
  const { user } = useAuth();
  const {
    projects,
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
  } = useProjects();

  const [isAddModalOpen, setIsAddModalOpen] = useState(false);
  const [isEditModalOpen, setIsEditModalOpen] = useState(false);

  const handleLogout = () => {
    dispatch(logout());
    navigate('/login');
  };

  const sidebarItems = [
    { label: 'Add Project', onClick: () => setIsAddModalOpen(true) },
    { label: 'Logout', onClick: handleLogout },
  ];


  return (
    <div className="flex h-screen bg-gray-100">
      <Sidebar title={`Welcome, ${user?.name || ''}!`} items={sidebarItems} />

      <div className="flex-1 p-8 overflow-auto">
        <h1 className="text-2xl font-bold mb-4">Your Projects</h1>
        <ErrorDisplay error={error} validationErrors={validationErrors} />
        <ProjectList
          projects={projects}
          onEdit={(project) => { setEditingProject(project); setIsEditModalOpen(true); }}
          onDelete={handleDeleteProject}
        />
      </div>

      <Modal isOpen={isAddModalOpen} onClose={() => setIsAddModalOpen(false)} title="Add New Project">
        <ProjectForm
          project={newProject}
          onChange={handleProjectChange}
          onSubmit={() => { handleCreateProject(); setIsAddModalOpen(false); }}
          isLoading={loading}
          submitText="Create Project"
        />
      </Modal>

      <Modal isOpen={isEditModalOpen} onClose={() => setIsEditModalOpen(false)} title="Edit Project">
        {editingProject && (
          <ProjectForm
            project={editingProject}
            onChange={handleEditingProjectChange}
            onSubmit={() => { handleUpdateProject(); setIsEditModalOpen(false); }}
            isLoading={loading}
            submitText="Update Project"
          />
        )}
      </Modal>
    </div>
  );
}