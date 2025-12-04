import { apiMain as api } from "./api";


export const getCategory = async ({signal}) => {
    const response = await api.get("/category", { signal });
    console.log("Categorias buscadas:", response.data);
    return response.data.value || response.data || [];
};

export const getCategoryById = async ({id, signal}) => {
    const response = await api.get(`/category/${id}`, { signal });
    return response.data;
};

export const createCategory = async (categoryData, signal) => {
    const response = await api.post("/category", categoryData, { signal });
    return response.data;
};

export const updateCategory = async (categoryData, signal) => {
    const response = await api.put("/category", categoryData, { signal });
    return response.data;
};

export const deleteCategory = async ({ id, signal}) => {
  await api.put(`/category/${id}`, { signal });

};