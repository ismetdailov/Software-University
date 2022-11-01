import { AppRootState } from '../store';

export const selectUserList = (state: AppRootState) => state.user.users.userList;
export const selectUserListCount = (state: AppRootState) => state.user.users.userListCount;
export const selectUserListIsLoading = (state: AppRootState) => state.user.users.userListIsLoading;
export const selectUserListErrorMessage = (state: AppRootState) => state.user.users.userListErrorMessage;

export const selectCurrentUser = (state: AppRootState) => state.user.user.currentUser;
export const selectCurrentUserIsLoading = (state: AppRootState) => state.user.user.currentUserIsLoading;
export const selectCurrentUserErrorMessage = (state: AppRootState) => state.user.user.currentUserErrorMessage;
