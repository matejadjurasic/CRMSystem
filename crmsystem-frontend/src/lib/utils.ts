import { USER_ROLES } from './constants';
import { User } from './types';

export const getDashboardRoute = (user: User | null): string => {
  if (!user) return '/login';
  return user.roles.includes(USER_ROLES.ADMIN) ? '/admin' : '/user';
};

export const getParamValue = (searchString : string, paramName: string) => {
  const query = searchString.substring(1);
  const params = query.split('&');

  for (let param of params) {
    const [key, value] = param.split('=');
    if (key === paramName) {
      return value; 
    }
  }
  return null;
};
