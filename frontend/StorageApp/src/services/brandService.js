import { apiMain as api } from "./api";


export const getBrands = async (signal) => {
  try {
    const { data } = await api.get("/brands", { signal });
    return data.brands || [];
  } catch (error) {
    throw error
  }
};

export const createBrand = async (brandData, signal) => {
  try {
    const { data } = await api.post("/brands/add", brandData, { signal });
    return data;
  } catch (error) {
    throw error
  }
};