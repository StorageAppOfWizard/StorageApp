import {createUser, loginUser, logoutUser } from "../services/authService";

export const authEndpoint = {
  UserCreate: {
    fn: createUser,
    isMutation: true,
  },
  UserLogin: {
    fn: loginUser,
    isMutation: true,
  },
  UserLogout: {
    fn: logoutUser,
    isMutation: true,
  },
};