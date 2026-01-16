import { getAllUsers, getUserById, updateUser, deleteUser, getUserDataFromToken } from "../services/userService";

export const userEndpoint = {
  UserGetAll: {
    fn: getAllUsers,
  }, 
  UserGetById: {
    fn: getUserById,
  },
  UserUpdate: {
    fn: updateUser,
    isMutation: true,
  },
  UserDelete: {
    fn: deleteUser,
    isMutation: true,
  },
  UserDataFromToken: {
    fn: getUserDataFromToken,
  },
};