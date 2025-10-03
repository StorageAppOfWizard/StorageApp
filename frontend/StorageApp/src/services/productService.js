//Ajustar a funções 
import axios from 'axios';

const api = axios.create({

    baseURL: "https://localhost:7216", 
    timeout: 10000,
    headers: {
        "Content-Type": "application/json",
    },
});

export const getProducts = async (signal) => {
    try {
        const response = await api.get(`/product`, { signal });
        console.log("Produtos buscados:", response);
        return response.data.value;
    } catch (error) {
        if (axios.isCancel(error)) {
            console.log(`Requisição dos produtos foi cancelada`, error);
            return null;
        }
        console.error("Erro ao buscar os produtos:", error.response.data.errors);
        throw error;
    }
};

export const getProductById = async (id, signal) => {
    try {
        const response = await api.get(`/product/${id}`, { signal });
        return response.data;
    } catch (error) {
        if (axios.isCancel(error)) {
            console.log(`Requisição do produto ${id} cancelada`, error);
            return null;
        }
        console.error(`Erro ao buscar o produto ${id}`, error.response.data.errors);
        throw error;
    }
};

export const createProduct = async (productData, signal) => {
    try {
        const response = await api.post(`/product`, productData, { signal });
        return response.data;
    } catch (error) {
        if (axios.isCancel(error)) {
            console.log(`Requisição de criação de produto cancelada`, error.response.data.errors);
            return null;
        }
        console.error("Erro ao criar produto:", error);
        throw error;
    }
};

export const updateProductStock = async (id, newStock, signal) => {
    try {
        const response = await api.patch(`/product/editQuantity`, {id: id, quantity: newStock, }, { signal });
        return response.data;
    }
    catch (error) {
        if (axios.isCancel(error)) {
            console.log(`Requisição de atualização de estoque do produto ${id} cancelada`, error.response.data.errors);
            return null;
        }
    }
}
export const updateProduct = async (id, productData, signal) => {
    try {
        const response = await api.put(`/product`, productData, { signal });
        return response.data;
    } catch (error) {
        if (axios.isCancel(error)) {
            console.log(`Requisição de atualização de produto ${id} cancelada`, error.response.data.errors);
            return null;
        }
        console.error(`Erro ao atualizar o produto ${id}`, error);
        throw error;
    }
};

export const deleteProduct = async (id, signal) => {
    try {
        await api.delete(`/product/${id}`, { signal });
        return;
    } catch (error) {
        if (axios.isCancel(error)) {
            console.log(`Requisição de exclusão de produto ${id} cancelada`, error.response.data.errors);
            return null;
        }
        console.error(`Erro ao excluir o produto ${id}`, error);
        throw error;
    }
};





