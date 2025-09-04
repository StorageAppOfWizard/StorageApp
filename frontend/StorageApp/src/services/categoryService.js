import axios from "axios";

const api = axios.create({

    baseURL: "https://localhost:7216",
    timeout: 10000,
    headers: {
        "Content-Type": "application/json",
    },
}); 


export const getCategories = async (signal) => {
    try {
        const response = await api.get(`/Category`, { signal });
        return response.data;
    } catch (error) {
        if (axios.isCancel(error)) return null;
        console.error("Erro ao buscar categorias:", error);
        throw error;
    }
};

export const createCategory = async (categoryData, signal) => {
    try {
        const response = await api.post(`/categories/add`, categoryData, { signal });
        return response.data;
    } catch (error) {
        if (axios.isCancel(error)) return null;
        console.error("Erro ao criar categoria:", error);
        throw error;
    }
};