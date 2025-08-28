import axios from 'axios';

const api = axios.create({
    baseURL: "https://dummyjson.com",
    timeout: 10000,
    headers: {
        "Content-Type": "application/json",
    },
});

//Função para buscar o produto
export const getProducts = async (limit = 15) => {
    try {
        const response = await api.get(`/products?limit=${limit}`);
        return response.data;
    } catch (error) {
        if (axios.isCancel(error)) {
            console.log(`Requisição dos produtos foram canceladas`, error);
            return (null);
        }
        console.error("Erro ao buscar os produtos:", error);
        throw error;
    }
};

//Função para buscar um produto por ID
export const getProductsById = async (id) => {
    try {
        const response = await api.get(`/products/${id}`);
        return response.data;
    } catch (error) {
        if (axios.isCancel(error)) {
            console.log(`Requisição do produto ${id} cancelada`, error);
            return (null);
        }
        console.error(`Erro ao buscar o produto ${id}`, error);
        throw error;
    }
}