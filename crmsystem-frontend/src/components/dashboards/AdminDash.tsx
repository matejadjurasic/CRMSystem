import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { useDispatch } from 'react-redux';
import { AppDispatch } from '../../store';
import { logout } from '../../store/slices/authSlice';
import { useUsers } from '../../hooks/useUsers';
import { useProjects } from '../../hooks/useProjects';
import Sidebar from '../common/Sidebar';
import ProjectList from '../common/ProjectList';
import UserList from '../common/UserList';
import Modal from '../common/Modal';
import ProjectForm from '../common/ProjectForm';
import UserForm from '../common/UserForm';
import ErrorDisplay from '../common/ErrorDisplay';

export default function AdminDash() {
  const dispatch = useDispatch<AppDispatch>();
  const navigate = useNavigate();
  const [activeView, setActiveView] = useState<'adminProjects' | 'allProjects' | 'users'>('adminProjects');
  const [isAddUserModalOpen, setIsAddUserModalOpen] = useState(false);
  const [isAddProjectModalOpen, setIsAddProjectModalOpen] = useState(false);
  const [isEditUserModalOpen, setIsEditUserModalOpen] = useState(false);
  const [isEditProjectModalOpen, setIsEditProjectModalOpen] = useState(false);

  const {
    users,
    loading: userLoading,
    error: userError,
    validationErrors: userValidationErrors,
    newUser,
    editingUser,
    setEditingUser,
    handleCreateUser,
    handleUpdateUser,
    handleDeleteUser,
    handleUserChange,
    handleEditingUserChange,
  } = useUsers();

  const {
    projects,
    allProjects,
    loading: projectLoading,
    error: projectError,
    validationErrors: projectValidationErrors,
    newProject,
    editingProject,
    setEditingProject,
    handleCreateProject,
    handleUpdateProject,
    handleDeleteProject,
    handleProjectChange,
    handleEditingProjectChange,
  } = useProjects(true); 

  const handleLogout = () => {
    dispatch(logout());
    navigate('/login');
  };

  const sidebarItems = [
    {
      label: 'Admin Projects',
      onClick: () => setActiveView('adminProjects'),
      isActive: activeView === 'adminProjects',
    },
    {
      label: 'All Projects',
      onClick: () => setActiveView('allProjects'),
      isActive: activeView === 'allProjects',
    },
    {
      label: 'Users',
      onClick: () => setActiveView('users'),
      isActive: activeView === 'users',
    },
    { label: 'Create User', onClick: () => setIsAddUserModalOpen(true) },
    { label: 'Create Project', onClick: () => setIsAddProjectModalOpen(true) },
  ];

  const sidebarBottomItems = [
    { label: 'Logout', onClick: handleLogout },
  ];

  const displayProjects = activeView === 'adminProjects' ? projects : allProjects;

  return (
    <div className="flex h-screen bg-gray-100">
      <Sidebar title="Admin Dashboard" items={sidebarItems} bottomButtons={sidebarBottomItems} />

      <div className="flex-1 p-8 overflow-auto">
        <h1 className="text-2xl font-bold mb-4">
          {activeView === 'adminProjects' ? 'Admin Projects' :
           activeView === 'allProjects' ? 'All Projects' : 'Users'}
        </h1>
        
        <ErrorDisplay
          error={activeView === 'users' ? userError : projectError}
          validationErrors={activeView === 'users' ? userValidationErrors : projectValidationErrors}
        />

        {activeView === 'users' ? (
          <UserList
            users={users}
            onEdit={(user) => { setEditingUser(user); setIsEditUserModalOpen(true); }}
            onDelete={handleDeleteUser}
          />
        ) : (
          <ProjectList
            projects={displayProjects}
            onEdit={(project) => { setEditingProject(project); setIsEditProjectModalOpen(true); }}
            onDelete={handleDeleteProject}
          />
        )}
      </div>

      <Modal isOpen={isAddUserModalOpen} onClose={() => setIsAddUserModalOpen(false)} title="Add New User">
        <UserForm
          user={newUser}
          onChange={handleUserChange}
          onSubmit={() => { handleCreateUser(); setIsAddUserModalOpen(false); }}
          isLoading={userLoading}
          submitText="Create User"
        />
      </Modal>

      <Modal isOpen={isAddProjectModalOpen} onClose={() => setIsAddProjectModalOpen(false)} title="Add New Project">
        <ProjectForm
          project={newProject}
          onChange={handleProjectChange}
          onSubmit={() => { handleCreateProject(); setIsAddProjectModalOpen(false); }}
          isLoading={projectLoading}
          submitText="Create Project"
        />
      </Modal>

      <Modal isOpen={isEditUserModalOpen} onClose={() => setIsEditUserModalOpen(false)} title="Edit User">
        {editingUser && (
          <UserForm
            user={editingUser}
            onChange={handleEditingUserChange}
            onSubmit={() => { handleUpdateUser(); setIsEditUserModalOpen(false); }}
            isLoading={userLoading}
            submitText="Update User"
          />
        )}
      </Modal>

      <Modal isOpen={isEditProjectModalOpen} onClose={() => setIsEditProjectModalOpen(false)} title="Edit Project">
        {editingProject && (
          <ProjectForm
            project={editingProject}
            onChange={handleEditingProjectChange}
            onSubmit={() => { handleUpdateProject(); setIsEditProjectModalOpen(false); }}
            isLoading={projectLoading}
            submitText="Update Project"
          />
        )}
      </Modal>
    </div>
  );
}