import { apiMain as api } from "./api";

export const getBrands = async ({signal}) => {
    const response = await api.get("/brand", { signal });
    console.log("Marcas buscadas:", response.data);
    return response.data.value || response.data || [];
};

export const getBrandsById = async ({id, signal}) => {
    const response = await api.get(`/brand/${id}`, { signal });
    return response.data;
};

export const createBrand = async (brandData, signal) => {
    const response = await api.post("/brand", brandData, { signal });
    return response.data;
};

export const updateBrand = async (brandData, signal) => {
    const response = await api.put("/brand", brandData, { signal });
    return response.data;
};

export const deleteBrand = async ({ id, signal}) => {
  await api.put(`/brand/${id}`, { signal });

};