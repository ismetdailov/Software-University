import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { IUser, IUserBase } from '../../components/shared/interfaces/user';

export interface IUserState {
  users: {
    userList: IUserBase[] | null;
    userListIsLoading: boolean;
    userListErrorMessage: string | null;
    userListCount: number;
  };
  user: {
    currentUser: IUser | null;
    currentUserIsLoading: boolean;
    currentUserErrorMessage: string | null;
  };
}

export const initialUserState: IUserState = {
  users: {
    userList: null,
    userListIsLoading: false,
    userListErrorMessage: null,
    userListCount: 0,
  },
  user: {
    currentUser: null,
    currentUserIsLoading: false,
    currentUserErrorMessage: null,
  },
};

const userSlice = createSlice({
  initialState: initialUserState,
  name: 'user',
  reducers: {
    getUsers: (
      state: IUserState,
      { payload: { users, count } }: PayloadAction<{ users: IUserBase[]; count: number }>
    ) => ({
      ...state,
      users: {
        ...state.users,
        userList: users,
        userListCount: count,
      },
    }),
    usersIsLoading: (
      state: IUserState,
      { payload: { isLoading } }: PayloadAction<{ isLoading: boolean }>
    ) => ({
      ...state,
      users: {
        ...state.users,
        userListIsLoading: isLoading,
      },
    }),
    usersErrorMessage: (
      state: IUserState,
      { payload: { message } }: PayloadAction<{ message: string | null }>
    ) => ({
      ...state,
      users: {
        ...state.users,
        userListErrorMessage: message,
      },
    }),
    clearUsers: (state: IUserState) => {
      return {
        ...state,
        users: {
          userList: null,
          userListCount: 0,
          userListIsLoading: false,
          userListErrorMessage: null,
        },
      };
    },
    getUser: (state: IUserState, { payload: { user } }: PayloadAction<{ user: IUser }>) => ({
      ...state,
      user: {
        ...state.user,
        currentUser: user,
      },
    }),
    userIsLoading: (
      state: IUserState,
      { payload: { isLoading } }: PayloadAction<{ isLoading: boolean }>
    ) => ({
      ...state,
      user: {
        ...state.user,
        currentUserIsLoading: isLoading,
      },
    }),
    userErrorMessage: (
      state: IUserState,
      { payload: { message } }: PayloadAction<{ message: string | null }>
    ) => ({
      ...state,
      user: {
        ...state.user,
        currentUserErrorMessage: message,
      },
    }),
    clearUser: (state: IUserState) => ({
      ...state,
      user: { currentUser: null, currentUserErrorMessage: null, currentUserIsLoading: false },
    }),
    addNewUser: (state: IUserState, { payload: { user } }: PayloadAction<{ user: IUserBase }>) => {
      return {
        ...state,
        users: {
          ...state.users,
          userList: (state.users.userList || [])?.concat(user),
        },
      };
    },
    updateUser: (state: IUserState, { payload: { user } }: PayloadAction<{ user: IUserBase }>) => {
      const existingUserId = (state?.users?.userList || []).findIndex((u) => u._id === user._id);
      if (existingUserId > -1 && state?.users?.userList) {
        const copiedUsers = [...state.users.userList];
        copiedUsers[existingUserId] = user;
        return {
          ...state,
          users: {
            ...state.users,
            userList: copiedUsers,
          },
        };
      }
      return state;
    },
    deleteUser: (state: IUserState, { payload: { userId } }: PayloadAction<{ userId: string }>) => {
      if (state?.users && state.users?.userList) {
        return {
          ...state,
          users: {
            ...state.users,
            userList: state?.users?.userList?.filter((u) => u._id !== userId),
            userListCount: state.users.userListCount - 1,
          },
        };
      }
      return state;
    },
  },
});

export const userReducer = userSlice.reducer;
export const {
  getUsers,
  usersIsLoading,
  usersErrorMessage,
  getUser,
  userIsLoading,
  userErrorMessage,
  clearUser,
  addNewUser,
  updateUser,
  deleteUser,
} = userSlice.actions;
