import axios from 'axios';

const api = axios.create({
    baseURL: "https://dummyjson.com",
    timeout: 10000,
    headers:{
        "Content-Type": "application/json",
    },
});


// Endpoint fictício - Endpoint real para criar, ler, excluir ou mudar é apenas passar o protocolo (GET, POST...) no /[propriedade] (/Brands, /Product e /Category) é apenas dar um post em /Brands
//Ja começa a consumir a api real porque ja tem o backend rodando


export const getProducts = async (limit = 15, signal) =>{
    try{
        const response = await api.get(`/products?limit=${limit}`, {signal});
        return response.data;
    } catch (error) {
        if (axios.isCancel(error)){
            console.log(`Requisição dos produtos foi cancelada`, error);
            return null;
        }
        console.error("Erro ao buscar os produtos: ", error)
        throw error;
    }
};

export const getProductsById = async (id, signal) =>{
    try{
        const response = await api.get(`/products/${id}`, {signal});
        return response.data;
    } catch (error) {
        if (axios.isCancel(error)){
            console.log(`Requisição do produto ${id} cancelada`, error);
            return null;
        }
        console.error(`Erro ao buscar o produto ${id}`, error);
        throw error;
    }
};

export const createProduct = async (productData, signal) => {
    try{
        const response = await api.post(`/products/add`, productData, {signal});
        return response.data;
    } catch (error) {
        if (axios.isCancel(error)){
            console.log(`Requisição para criar produto cancelada`, error);
            return null;
        }
        console.error("Erro ao criar produto: ", error);
        throw error;
    }
}


export const getBrands = async (signal) => {
    try {
        const response = await api.get(`/brands`, { signal }); // Endpoint fictício
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
    } catch (error) {
        if (axios.isCancel(error)) return null;
        console.error("Erro ao criar marca:", error);
        throw error;
    }
};

export const getCategories = async (signal) => {
    try {
        const response = await api.get(`/products/categories`, { signal });
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


