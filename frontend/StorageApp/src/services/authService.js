import { apiAuth as api } from "./api";


export const createUser = async (userData, signal) => {
  const { data } = await api.post("/auth/register", userData, { signal });
  return data;
};

export const loginUser = async (userData, signal) => {
  const { data } = await api.post("/auth/login", userData, { signal });

  localStorage.setItem("token", data.token);
  return data;
};

export const logoutUser = () => {
  localStorage.removeItem("token");
  localStorage.removeItem("user");
};

export const isAuthenticated = () => {
  return !!localStorage.getItem("token");
};

export const getToken = () => {
  return localStorage.getItem("token");
};