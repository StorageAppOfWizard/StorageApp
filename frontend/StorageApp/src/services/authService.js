import { apiAuth as api } from "./api";
import axios from "axios";


export const createUser = async (userData, signal) => {
  try {
    const { data } = await api.post("/auth/register", userData, { signal });
    return data;
  } catch (error) {
    if (axios.isCancel(error)) return null;

    console.error("Erro ao criar usuário:", error);
    throw new Error(
      error.response?.data?.message || "Erro ao criar usuário. Tente novamente."
    );
  }
};


export const loginUser = async (userData, signal) => {
  try {
    const { data } = await api.post("/auth/login", userData, { signal });
    return data;
  } catch (error) {
    if (axios.isCancel(error)) return null;

    console.error("Erro ao fazer login:", error);
    throw new Error(
      error.response?.data?.message ||
        "Erro ao efetuar login do usuário. Tente novamente."
    );
  }
};


export const getAllUsers = async (signal) => {
  try {
    const { data } = await api.get("/admin/user", { signal });
    return data;
  } catch (error) {
    if (axios.isCancel(error)) return null;

    console.error("Erro ao buscar usuários:", error);
    throw new Error(
      error.response?.data?.message || "Erro ao buscar usuários. Tente novamente."
    );
  }
};


export const getUserById = async (id, signal) => {
  try {
    const { data } = await api.get(`/admin/user/${id}`, { signal });
    return data;
  } catch (error) {
    if (axios.isCancel(error)) return null;

    console.error(`Erro ao buscar usuário ${id}:`, error);
    throw new Error(
      error.response?.data?.message || "Erro ao buscar o usuário. Tente novamente."
    );
  }
};


export const updateUser = async (id, userData, signal) => {
  try {
    const { data } = await api.put(`/admin/user/${id}`, userData, { signal });
    return data;
  } catch (error) {
    if (axios.isCancel(error)) return null;

    console.error(`Erro ao atualizar usuário ${id}:`, error);
    throw new Error(
      error.response?.data?.message ||
        "Erro ao atualizar o usuário. Tente novamente."
    );
  }
};


export const deleteUser = async (id, signal) => {
  try {
    const { data } = await api.delete(`/admin/user/${id}`, { signal });
    return data;
  } catch (error) {
    if (axios.isCancel(error)) return null;

    console.error(`Erro ao excluir usuário ${id}:`, error);
    throw new Error(
      error.response?.data?.message ||
        "Erro ao excluir o usuário. Tente novamente."
    );
  }
};
