import { getAllUsers, getUserById, createUser, loginUser, updateUser, deleteUser, logoutUser, getUserDataFromToken } from "../services/authService";

export const authEndpoint = {
  UserGetAll: {
    fn: getAllUsers,
  }, 
  UserGetById: {
    fn: getUserById,
  },
  UserCreate: {
    fn: createUser,
    isMutation: true,
  },
  UserLogin: {
    fn: loginUser,
    isMutation: true,
  },
  UserUpdate: {
    fn: updateUser,
    isMutation: true,
  },
  UserDelete: {
    fn: deleteUser,
    isMutation: true,
  },
  UserLogout: {
    fn: logoutUser,
    isMutation: true,
  },
  UserDataFromToken: {
    fn: getUserDataFromToken,
  },
};