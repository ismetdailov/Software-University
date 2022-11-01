import {
  usersIsLoading,
  usersErrorMessage,
  getUsers,
  userIsLoading,
  userErrorMessage,
  getUser,
  deleteUser,
  addNewUser,
  updateUser,
} from './slice';
import { http } from '../../utils/http-request';
import { ICreateOrUpdateUser, IUser, IUserBase } from '../../components/shared/interfaces/user';
import { AppDispatch } from '../store';

export const getUsersAction = (path: string) => async (dispatch: AppDispatch) => {
  dispatch(usersIsLoading({ isLoading: true }));
  dispatch(usersErrorMessage({ message: null }));
  try {
    const { users, count } = await http.get<{ users: IUserBase[]; count: number }>(path);
    dispatch(getUsers({ users, count }));
  } catch (error: any) {
    dispatch(usersErrorMessage({ message: error.message }));
  } finally {
    dispatch(usersIsLoading({ isLoading: false }));
  }
};

export const getUserAction = (userId: string) => async (dispatch: AppDispatch) => {
  dispatch(userIsLoading({ isLoading: true }));
  dispatch(userErrorMessage({ message: null }));
  try {
    const { user } = await http.get<{ user: IUser }>(`users/${userId}`);
    dispatch(getUser({ user }));
  } catch (error: any) {
    dispatch(userErrorMessage({ message: error.message }));
  } finally {
    dispatch(userIsLoading({ isLoading: false }));
  }
};

export const addNewUserAction =
  (userData: ICreateOrUpdateUser, onClose: () => void) => async (dispatch: AppDispatch) => {
    dispatch(userIsLoading({ isLoading: true }));
    dispatch(userErrorMessage({ message: null }));
    try {
      const { user } = await http.post<{ user: IUserBase }>('users', userData);
      dispatch(addNewUser({ user }));
      onClose();
    } catch (error: any) {
      dispatch(userErrorMessage({ message: error.message }));
    } finally {
      dispatch(userIsLoading({ isLoading: false }));
    }
  };

export const updateUserAction =
  (userId: string, userData: ICreateOrUpdateUser, onClose: () => void) =>
  async (dispatch: AppDispatch) => {
    dispatch(userIsLoading({ isLoading: true }));
    dispatch(userErrorMessage({ message: null }));
    try {
      const { user } = await http.put<{ user: IUserBase }>(`users/${userId}`, userData);
      dispatch(updateUser({ user }));
      onClose();
    } catch (error: any) {
      dispatch(userErrorMessage({ message: error.message }));
    } finally {
      dispatch(userIsLoading({ isLoading: false }));
    }
  };

export const deleteUserAction =
  (userId: string, onClose: () => void) => async (dispatch: AppDispatch) => {
    dispatch(userIsLoading({ isLoading: true }));
    dispatch(userErrorMessage({ message: null }));
    try {
      await http.del<{ userId: string }>(`users/${userId}`);
      dispatch(deleteUser({ userId }));
      onClose();
    } catch (error: any) {
      dispatch(userErrorMessage({ message: error.message }));
    } finally {
      dispatch(userIsLoading({ isLoading: false }));
    }
  };
