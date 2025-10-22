import axios from "axios";
import { apiMain as api } from "./api";

export const getProducts = async (signal) => {
  try {
    const response = await api.get("/product", { signal });
    console.log("Produtos buscados:", response.data);
    return response.data.value || response.data || [];
  } catch (error) {
    if (axios.isCancel(error)) {
      console.warn("Requisição de produtos cancelada");
      return null;
    }
    console.error("Erro ao buscar produtos:", error);
    throw error;
  }
};

export const getProductById = async (id, signal) => {
  try {
    const response = await api.get(`/product/${id}`, { signal });
    return response.data;
  } catch (error) {
    if (axios.isCancel(error)) {
      console.warn(`Requisição do produto ${id} cancelada`);
      return null;
    }
    console.error(`Erro ao buscar produto ${id}:`, error);
    throw error;
  }
};

export const createProduct = async (productData, signal) => {
  try {
    const response = await api.post("/product", productData, { signal });
    return response.data;
  } catch (error) {
    if (axios.isCancel(error)) {
      console.warn("Requisição de criação de produto cancelada");
      return null;
    }
    console.error("Erro ao criar produto:", error);
    throw error;
  }
};

export const updateProductStock = async (id, newStock, signal) => {
  try {
    const response = await api.patch(
      "/product/editQuantity",
      { id, quantity: newStock },
      { signal }
    );
    return response.data;
  } catch (error) {
    if (axios.isCancel(error)) {
      console.warn(`Requisição de atualização de estoque do produto ${id} cancelada`);
      return null;
    }
    console.error(`Erro ao atualizar estoque do produto ${id}:`, error);
    throw error;
  }
};

export const updateProduct = async (id, productData, signal) => {
  try {
    const response = await api.put("/product", productData, { signal });
    return response.data;
  } catch (error) {
    if (axios.isCancel(error)) {
      console.warn(`Requisição de atualização do produto ${id} cancelada`);
      return null;
    }
    console.error(`Erro ao atualizar produto ${id}:`, error);
    throw error;
  }
};

export const deleteProduct = async (id, signal) => {
  try {
    await api.delete(`/product/${id}`, { signal });
  } catch (error) {
    if (axios.isCancel(error)) {
      console.warn(`Requisição de exclusão do produto ${id} cancelada`);
      return null;
    }
    console.error(`Erro ao excluir produto ${id}:`, error);
    throw error;
  }
};
