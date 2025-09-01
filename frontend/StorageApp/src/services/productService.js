import axios from 'axios';

const api = axios.create({
    baseURL: "https://dummyjson.com",
    timeout: 10000,
    headers: {
        "Content-Type": "application/json",
    },
});

//Função para buscar o produto
export const getProducts = async (limit = 15, signal) => {
    try {
        const response = await api.get(`/products?limit=${limit}`, { signal });
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
export const getProductsById = async (id, signal) => {
    try {
        const response = await api.get(`/products/${id}`, { signal });
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

 
// Função para criar um produto
export const createProduct = async (productData, signal) => {
    try {
        const response = await api.post(`/products/add`, productData, { signal });
        return response.data;
    } catch (error) {
        if (axios.isCancel(error)) {
            console.log(`Requisição de criação de produto cancelada`, error);
            return null;
        }
        console.error("Erro ao criar produto:", error);
        throw error;
    }
};