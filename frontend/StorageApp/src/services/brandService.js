import axios from 'axios';
const api = axios.create({

    baseURL: "https://localhost:7216",
    timeout: 10000,
    headers: {
        "Content-Type": "application/json",
    },
});

export const getBrands = async (signal) => {
    try {
        const response = await api.get(`/Brand`, { signal });
        return response.data.brands || [];
    } catch (error) {
        if (axios.isCancel(error)) return null;
        console.error("Erro ao buscar marcas:", error);
        throw error;
    }
};

export const createBrand = async (brandData, signal) => {
    try {
        const response = await api.post(`/brands/add`, brandData, { signal });
        return response.data;
    } catch (error) {
        if (axios.isCancel(error)) return null;
        console.error("Erro ao criar marca:", error);
        throw error;
    }
};