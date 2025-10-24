import { apiMain as api } from "./api";
import axios from "axios";


export const getCategories = async (signal) => {
  try {
    const { data } = await api.get("/category", { signal });
    return data.categories || data; 
  } catch (error) {
    if (axios.isCancel(error)) return null;
    console.error("Erro ao buscar categorias:", error);
    throw new Error(error.response?.data?.message || "Erro ao buscar categorias.");
  }
};


export const createCategory = async (categoryData, signal) => {
  try {
    const { data } = await api.post("/category", categoryData, { signal });
    return data;
  } catch (error) {
    if (axios.isCancel(error)) return null;
    console.error("Erro ao criar categoria:", error);
    throw new Error(error.response?.data?.message || "Erro ao criar categoria.");
  }
};

export const updateCategory = async (id, categoryData, signal) => {
  try {
    const { data } = await api.put(`/category/${id}`, categoryData, { signal });
    return data;
  } catch (error) {
    if (axios.isCancel(error)) {
      console.warn(`Requisição de atualização da categoria ${id} cancelada.`);
      return null;
    }
    console.error(`Erro ao atualizar a categoria ${id}:`, error);
    throw new Error(error.response?.data?.message || "Erro ao atualizar categoria.");
  }
};


export const deleteCategory = async (id, signal) => {
  try {
    await api.delete(`/category/${id}`, { signal });
    return true;
  } catch (error) {
    if (axios.isCancel(error)) {
      console.warn(`Requisição de exclusão da categoria ${id} cancelada.`);
      return null;
    }

    if (error.response?.status === 409) {
      console.error("Não é possível excluir esta categoria pois está vinculada a um produto.");
      throw new Error("Não é possível excluir esta categoria pois está vinculada a um produto.");
    }

    console.error(`Erro ao excluir a categoria ${id}:`, error);
    throw new Error(error.response?.data?.message || "Erro ao excluir categoria.");
  }
};
