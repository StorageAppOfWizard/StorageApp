import { apiMain as api } from "./api";
import axios from "axios";

export const getBrands = async (signal) => {
  try {
    const { data } = await api.get("/brands", { signal });
    return data.brands || [];
  } catch (error) {
    if (axios.isCancel(error)) return null;
    console.error("Erro ao buscar marcas:", error);
    throw new Error(error.response?.data?.message || "Erro ao buscar marcas.");
  }
};

export const createBrand = async (brandData, signal) => {
  try {
    const { data } = await api.post("/brands/add", brandData, { signal });
    return data;
  } catch (error) {
    if (axios.isCancel(error)) return null;
    console.error("Erro ao criar marca:", error);
    throw new Error(error.response?.data?.message || "Erro ao criar marca.");
  }
};