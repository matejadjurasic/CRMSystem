import { useSelector } from 'react-redux';
import { RootState } from '../store';

export const useAuth = () => {
  const authState = useSelector((state: RootState) => state.auth);
  return authState;
};