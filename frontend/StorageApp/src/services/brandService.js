import { apiMain as api } from "./api";

export const getBrands = async (signal) => {
    const response = await api.get("/brand", { signal });
    console.log("Produtos buscados:", response.data);
    return response.data.value || response.data || [];
};

export const createBrand = async (brandData, signal) => {
  try {
    const { data } = await api.post("/brand", brandData, { signal });
    return data;
  } catch (error) {
    throw error
  }
};