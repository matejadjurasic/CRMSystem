import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';
import apiClient from '../../api/axios';
import { APIError, ProjectState, Project } from '../../lib/types';

const initialState: ProjectState = {
    allProjects: [],
    projects: [],
    loading: false,
    error: null,
    validationErrors: null,
};

export const fetchProjects = createAsyncThunk<
    Project[],
    void,
    { rejectValue: APIError }
>('projects/fetchProjects', async (_, { rejectWithValue }) => {
    try {
        const response = await apiClient.get<Project[]>('/projects');
        return response.data;
    } catch (error: any) {
        return rejectWithValue(error as APIError);
    }
});

export const fetchAllProjects = createAsyncThunk<
    Project[],
    void,
    { rejectValue: APIError }
>('projects/fetchAllProjects', async (_, { rejectWithValue }) => {
    try {
        const response = await apiClient.get<Project[]>('/projects/all');
        return response.data;
    } catch (error: any) {
        return rejectWithValue(error as APIError);
    }
});

export const createProject = createAsyncThunk<
    number,
    Omit<Project, 'id'>,
    { rejectValue: APIError }
>('projects/createProject', async (projectData, { rejectWithValue }) => {
    try {
        const response = await apiClient.post<number>('/projects', projectData);
        return response.data;
    } catch (error: any) {
        return rejectWithValue(error as APIError);
    }
});

export const updateProject = createAsyncThunk<
{ id: number; projectData: Partial<Project> },
    { id: number; projectData: Partial<Project> },
    { rejectValue: APIError }
>('projects/updateProject', async ({ id, projectData }, { rejectWithValue }) => {
    try {
        await apiClient.put(`/projects/${id}`, projectData);
        return {id,projectData};
    } catch (error: any) {
        return rejectWithValue(error as APIError);
    }
});

export const deleteProject = createAsyncThunk<
    number,
    number,
    { rejectValue: APIError }
>('projects/deleteProject', async (id: number, { rejectWithValue }) => {
    try {
        await apiClient.delete(`/projects/${id}`);
        return id;
    } catch (error: any) {
        return rejectWithValue(error as APIError);
    }
});

const projectSlice = createSlice({
    name: 'projects',
    initialState,
    reducers: {
        clearErrors: (state) => {
            state.error = null;
            state.validationErrors = null;
        },
    },
    extraReducers: (builder) => {
        builder
            .addCase(fetchProjects.pending, (state) => {
                state.loading = true;
                state.error = null;
                state.validationErrors = null;
            })
            .addCase(fetchProjects.fulfilled, (state, action) => {
                state.projects = action.payload;
                state.loading = false;
            })
            .addCase(fetchProjects.rejected, (state, action) => {
                state.loading = false;
                state.error = action.payload?.errorMessage || 'An error occurred while fetching projects.';
                state.validationErrors = action.payload?.validationErrors || null;
            })
            .addCase(fetchAllProjects.pending, (state) => {
                state.loading = true;
                state.error = null;
                state.validationErrors = null;
            })
            .addCase(fetchAllProjects.fulfilled, (state, action) => {
                state.allProjects = action.payload;
                state.loading = false;
            })
            .addCase(fetchAllProjects.rejected, (state, action) => {
                state.loading = false;
                state.error = action.payload?.errorMessage || 'An error occurred while fetching projects.';
                state.validationErrors = action.payload?.validationErrors || null;
            })
            .addCase(createProject.pending, (state) => {
                state.loading = true;
                state.error = null;
                state.validationErrors = null;
            })
            .addCase(createProject.fulfilled, (state, action) => {
                const newProject: Project = {
                    id: action.payload, 
                    ...action.meta.arg,  
                };
                state.projects.push(newProject);
                state.allProjects.push(newProject);
                state.loading = false;
            })
            .addCase(createProject.rejected, (state, action) => {
                state.loading = false;
                state.error = action.payload?.errorMessage || 'An error occurred while creating the project.';
                state.validationErrors = action.payload?.validationErrors || null;
            })
            .addCase(updateProject.pending, (state) => {
                state.loading = true;
                state.error = null;
                state.validationErrors = null;
            })
            .addCase(updateProject.fulfilled, (state, action) => {
                const { id, projectData } = action.payload;
                const updateProject = (projectList: Project[]) => {
                    const index = projectList.findIndex(project => project.id === id);
                    if (index !== -1) {
                        projectList[index] = { ...projectList[index], ...projectData };
                    }
                };
                updateProject(state.projects);
                updateProject(state.allProjects);
                state.loading = false;
            })
            .addCase(updateProject.rejected, (state, action) => {
                state.loading = false;
                state.error = action.payload?.errorMessage || 'An error occurred while updating the project.';
                state.validationErrors = action.payload?.validationErrors || null;
            })
            .addCase(deleteProject.pending, (state) => {
                state.loading = true;
                state.error = null;
                state.validationErrors = null;
            })
            .addCase(deleteProject.fulfilled, (state, action) => {
                state.projects = state.projects.filter(project => project.id !== action.payload);
                state.allProjects = state.allProjects.filter(project => project.id !== action.payload);
                state.loading = false;
            })
            .addCase(deleteProject.rejected, (state, action) => {
                state.loading = false;
                state.error = action.payload?.errorMessage || 'An error occurred while deleting the project.';
                state.validationErrors = action.payload?.validationErrors || null;
            });
    },
});

export const { clearErrors } = projectSlice.actions;
export default projectSlice.reducer;
