import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { useDispatch } from 'react-redux';
import { AppDispatch } from '../../store';
import { logout } from '../../store/slices/authSlice';
import { useProjects } from '../../hooks/useProjects';
import { useUsers } from '../../hooks/useUsers';
import Modal from '../common/Modal';
import Sidebar from '../common/Sidebar';
import ProjectList from '../common/ProjectList';
import ProjectForm from '../common/ProjectForm';
import ErrorDisplay from '../common/ErrorDisplay';
import { useAuth } from '../../hooks/useAuth';
import TopBar from '../common/TopBar';
import UserForm from '../common/UserForm';
import ProjectGraph from '../common/ProjectGraph';

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
  const{
    handleUpdateUser,
    setEditingUser,
    handleEditingUserChange,
    editingUser,
    loading: userLoading,
  } = useUsers();

  const [isAddModalOpen, setIsAddModalOpen] = useState(false);
  const [isEditModalOpen, setIsEditModalOpen] = useState(false);
  const [isEditUserModalOpen, setIsEditUserModalOpen] = useState(false);
  const [isGraphView, setIsGraphView] = useState(false);

  const handleLogout = () => {
    dispatch(logout());
    navigate('/login');
  };

  const handleGraphOption = () => {
    setIsGraphView(true);
  };

  const toggleView = () => {
    setIsGraphView(false);
  };

  const sidebarItems = [
    {
      label: 'Projects',
      members: [
        { 
          label: 'Add Project', 
          onClick: () => setIsAddModalOpen(true) 
        },
      ],
    },
  ];


  return (
    <div className="flex flex-col h-screen bg-gray-100">
      <TopBar 
        workspaceName="Current Workspace" 
        onEditUser={() => { setEditingUser(user); setIsEditUserModalOpen(true); }}
        onLogout={handleLogout}
        onGraphOption={handleGraphOption}
        isGraphView={isGraphView} 
        onToggleView={toggleView} 
      />
      <div className="flex h-full">
      {!isGraphView && <Sidebar title={`Welcome, ${user?.name || ''}!`} categories={sidebarItems} />}
        <div className="flex-1 p-8 overflow-auto">
          <h1 className="text-2xl font-bold mb-4">Your Projects</h1>
          <ErrorDisplay error={error} validationErrors={validationErrors} />
          {isGraphView ? (
            <ProjectGraph projects={projects} />
          ) : (
            <ProjectList
              projects={projects}
              onEdit={(project) => { setEditingProject(project); setIsEditModalOpen(true); }}
              onDelete={handleDeleteProject}
            />
          )}
        </div>
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
    </div>
  );
}