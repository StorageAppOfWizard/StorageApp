//Ajustar a funções 
import axios from 'axios';

const api = axios.create({
    //baseURL: "https://dummyjson.com",
    baseURL: "https://localhost:7216", // Endpoint local para desenvolvimento
    timeout: 10000,
    headers: {
        "Content-Type": "application/json",
    },
});


export const getProducts = async (signal) => {
    try {
        const response = await api.get(`Product`, { signal });
        console.log("Produtos buscados:", response);
        return response.data.value;
    } catch (error) {
        if (axios.isCancel(error)) {
            console.log(`Requisição dos produtos foi cancelada`, error);
            return null;
        }
        console.error("Erro ao buscar os produtos:", error);
        throw error;
    }
};

export const getProductById = async (id, signal) => {
    try {
        const response = await api.get(`/products/${id}`, { signal });
        return response.data;
    } catch (error) {
        if (axios.isCancel(error)) {
            console.log(`Requisição do produto ${id} cancelada`, error);
            return null;
        }
        console.error(`Erro ao buscar o produto ${id}`, error);
        throw error;
    }
};

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

export const updateProductStock = async (id, newStock, signal) => {
    try {
        const response = await api.patch(`/Product/quantity`, {id: id, quantity: newStock }, { signal });
        return response.data;
    }
    catch (error) {
        if (axios.isCancel(error)) {
            console.log(`Requisição de atualização de estoque do produto ${id} cancelada`, error);
            return null;
        }
    }
}
export const updateProduct = async (id, productData, signal) => {
    try {
        const response = await api.put(`/Product/${id}`, productData, { signal });
        return response.data;
    } catch (error) {
        if (axios.isCancel(error)) {
            console.log(`Requisição de atualização de produto ${id} cancelada`, error);
            return null;
        }
        console.error(`Erro ao atualizar o produto ${id}`, error);
        throw error;
    }
};

export const deleteProduct = async (id, signal) => {
    try {
        const response = await api.delete(`/products/${id}`, { signal });
        return response.data;
    } catch (error) {
        if (axios.isCancel(error)) {
            console.log(`Requisição de exclusão de produto ${id} cancelada`, error);
            return null;
        }
        console.error(`Erro ao excluir o produto ${id}`, error);
        throw error;
    }
};

// Funções de marcas e categorias (manter como estão por agora)
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
        const response = await api.post(`/category/add`, categoryData, { signal });
        return response.data;
    } catch (error) {
        if (axios.isCancel(error)) return null;
        console.error("Erro ao criar categoria:", error);
        throw error;
    }
};