import axios from 'axios';
import { APIError } from '../lib/types';

const apiClient = axios.create({
    baseURL: process.env.REACT_APP_API_BASE_URL, 
    headers: {
      'Content-Type': 'application/json',
    },
});

apiClient.interceptors.request.use(
    (config) => {
      const token = localStorage.getItem('token');
      if (token) {
        config.headers.Authorization = `Bearer ${token}`;
      }
      return config;
    },
    (error) => Promise.reject(error)
);

apiClient.interceptors.response.use(
    (response) => response,
    (error) => {
      const errorResponse = error.response?.data;
      const apiError: APIError = {
        errorMessage: 'An error occurred.',
      };

      if (errorResponse) {
        if (errorResponse.errors) {
          apiError.errorMessage = errorResponse.title || 'Validation Error';
          apiError.validationErrors = errorResponse.errors;
        } else if (errorResponse.message) {
          apiError.errorMessage = errorResponse.message;
        }
      } else if (error.message) {
        apiError.errorMessage = error.message;
      }

        return Promise.reject(apiError);
    }
);

export default apiClient;