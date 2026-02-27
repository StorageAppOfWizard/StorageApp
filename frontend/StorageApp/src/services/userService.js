import { apiAuth as api } from "./api";
import { jwtDecode } from "jwt-decode";


export const getUserDataFromToken = async ({ token, signal } = {}) => {
  const decoded = jwtDecode(token);
  const userId = decoded.nameid;

  const { data } = await api.get(`/admin/user/${userId}`, { signal });
  return data;
};

export const getAllUsers = async ({ signal, queryParams = {} } = {}) => {
  const { data } = await api.get("/admin/user", { signal, params: queryParams });
  return data;
};

export const getUserById = async ({ id, signal } = {}) => {
  const { data } = await api.get(`/admin/user/${id}`, { signal });
  return data;
};

export const updateUser = async ({ id, userData, signal } = {}) => {
  const { data } = await api.put(`/admin/user/${id}`, userData, { signal });
  return data;
};

export const deleteUser = async ({ id, signal } = {}) => {
  const { data } = await api.delete(`/admin/user/${id}`, { signal });
  return data;
};
