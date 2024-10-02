import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';
import apiClient from '../../api/axios';
import { APIError , UserState, User, CreateUser} from '../../lib/types';
import { USER_ROLES } from '../../lib/constants';

const initialState: UserState = {
    users: [],
    loading: false,
    error: null,
    validationErrors: null,
  };

  export const fetchUsers = createAsyncThunk<
  User[],
  void,
  { rejectValue: APIError }
>('users/fetchUsers', async (_, { rejectWithValue }) => {
  try {
    const response = await apiClient.get<User[]>('/users');
    return response.data;
  } catch (error: any) {
    return rejectWithValue(error as APIError);
  }
});

export const createUser = createAsyncThunk<
  number,
  CreateUser,
  { rejectValue: APIError }
>('users/createUser', async (userData, { rejectWithValue }) => {
  try {
    const response = await apiClient.post('/users', userData);
    return response.data;
  } catch (error: any) {
    return rejectWithValue(error as APIError);
  }
});

export const updateUser = createAsyncThunk<
  { id: number; userData: Partial<User> },
  { id: number; userData: Partial<User> },
  { rejectValue: APIError }
>('users/updateUser', async ({ id, userData }, { rejectWithValue }) => {
  try {
    await apiClient.put(`/users/${id}`, userData);
    return {id,userData};
  } catch (error: any) {
    return rejectWithValue(error as APIError);
  }
});

export const deleteUser = createAsyncThunk<
  number,
  number,
  { rejectValue: APIError }
>('users/deleteUser', async (id, { rejectWithValue }) => {
  try {
    await apiClient.delete(`/users/${id}`);
    return id;
  } catch (error: any) {
    return rejectWithValue(error as APIError);
  }
});

const userSlice = createSlice({
    name: 'users',
    initialState,
    reducers: {
      clearErrors: (state) => {
        state.error = null;
        state.validationErrors = null;
      },
    },
    extraReducers: (builder) => {
      builder
        .addCase(fetchUsers.pending, (state) => {
          state.loading = true;
          state.error = null;
          state.validationErrors = null;
        })
        .addCase(fetchUsers.fulfilled, (state, action) => {
          state.users = action.payload;
          state.loading = false;
        })
        .addCase(fetchUsers.rejected, (state, action) => {
          state.loading = false;
          state.error = action.payload?.errorMessage || 'An error occurred while fetching users.';
          state.validationErrors = action.payload?.validationErrors || null;
        })
        .addCase(createUser.pending, (state) => {
          state.loading = true;
          state.error = null;
          state.validationErrors = null;
        })
        .addCase(createUser.fulfilled, (state, action) => {
            const newUser: User = {
              id: action.payload, 
              ...action.meta.arg,
              roles: [USER_ROLES.CLIENT], 
              token: '',
            };
            state.users.push(newUser);
            state.loading = false;
          })
          .addCase(createUser.rejected, (state, action) => {
            state.loading = false;
            state.error = action.payload?.errorMessage || 'An error occurred while creating the user.';
            state.validationErrors = action.payload?.validationErrors || null;
          })
          .addCase(updateUser.pending, (state) => {
            state.loading = true;
            state.error = null;
            state.validationErrors = null;
          })
          .addCase(updateUser.fulfilled, (state, action) => {
            const {id, userData} = action.payload;
            const updateUser = (userList: User[]) => {
              const index = userList.findIndex(user => user.id === id);
              if (index !== -1) {
                  userList[index] = { ...userList[index], ...userData };
              }
            };
            updateUser(state.users);
            state.loading = false;
          })
          .addCase(updateUser.rejected, (state, action) => {
            state.loading = false;
            state.error = action.payload?.errorMessage || 'An error occurred while updating the user.';
            state.validationErrors = action.payload?.validationErrors || null;
          })
          .addCase(deleteUser.pending, (state) => {
            state.loading = true;
            state.error = null;
            state.validationErrors = null;
          })
          .addCase(deleteUser.fulfilled, (state, action) => {
            state.users = state.users.filter(user => user.id !== action.payload);
            state.loading = false;
          })
          .addCase(deleteUser.rejected, (state, action) => {
            state.loading = false;
            state.error = action.payload?.errorMessage || 'An error occurred while deleting the user.';
            state.validationErrors = action.payload?.validationErrors || null;
          });
      },
    });
    
    export const { clearErrors } = userSlice.actions;
    export default userSlice.reducer;