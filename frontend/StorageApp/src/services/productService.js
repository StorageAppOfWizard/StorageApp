import axios from "axios";
import { apiMain as api } from "./api";


export const getProducts = async ({ signal }) => {
  try {
    const response = await api.get("/product", { signal });
    console.log("Produtos buscados:", response.data);
    return response.data.value || response.data || [];
  } catch (error) {
    throw error;
  }
};


export const getProductById = async ({ id, signal }) => {
  try {
    const response = await api.get(`/product/${id}`, { signal });
    return response.data;
  } catch (error) {
    throw error;
  }
};


export const createProduct = async ({ productData, signal }) => {
  try {
    const response = await api.post("/product", productData, { signal });
    return response.data;
  } catch (error) {
    throw error;
  }
};


export const updateProductStock = async ({ id, newStock, signal }) => {
  try {
    const response = await api.patch(
      "/product/editQuantity",
      { id, quantity: newStock },
      { signal }
    );
    return response.data;
  } catch (error) {
    return null; 
  }
};


export const updateProduct = async ({ id, productData, signal }) => {
  try {
    const response = await api.put("/product", productData, { signal });
    return response.data;
  } catch (error) {
    return null;
  }
};


export const deleteProduct = async ({ id, signal }) => {
  try {
    await api.delete(`/product/${id}`, { signal });
  } catch (error) {
    return null;
  }
};
