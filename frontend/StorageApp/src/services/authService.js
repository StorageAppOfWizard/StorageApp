import { apiAuth as api } from "./api";


export const createUser = async (userData, signal) => {
  try {
    const { data } = await api.post("/auth/register", userData, { signal });
    return data;
  } catch (error) {
    throw error
  }
};


export const loginUser = async (userData, signal) => {
  try {
    const { data } = await api.post("/auth/login", userData, { signal });

    localStorage.setItem("token", data.token)
    return data;
  } catch (error) {
    throw error
  }
};


export const logoutUser = () => {
  localStorage.removeItem("token")
  localStorage.removeItem("user")
};

export const isAuthenticated = () => {
  return !!localStorage.getItem("token");
};

export const getToken = () => {
  return localStorage.getItem("token");
};


export const getAllUsers = async (signal) => {
  try {
    const { data } = await api.get("/admin/user", { signal });
    return data;
  } catch (error) {
    throw error
  }
};


export const getUserById = async (id, signal) => {
  try {
    const { data } = await api.get(`/admin/user/${id}`, { signal });
    return data;
  } catch (error) {
    throw error
  }
};


export const updateUser = async (id, userData, signal) => {
  try {
    const { data } = await api.put(`/admin/user/${id}`, userData, { signal });
    return data;
  } catch (error) {
    throw error
  }
};


export const deleteUser = async (id, signal) => {
  try {
    const { data } = await api.delete(`/admin/user/${id}`, { signal });
    return data;
  } catch (error) {
    throw error
  }
};
