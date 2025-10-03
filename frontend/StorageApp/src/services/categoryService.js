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
        const response = await api.get(`/category`, { signal });
        return response.data;
    } catch (error) {
        if (axios.isCancel(error)) return null;
        console.error("Erro ao buscar categorias:", error);
        throw error;
    }
};

export const createCategory = async (categoryData, signal) => {
    try {
        const response = await api.post(`/category`, categoryData, { signal });
        return response.data;
    } catch (error) {
        if (axios.isCancel(error)) return null;
        console.error("Erro ao criar categoria:", error);
        throw error;
    }
};

export const updateCategory = async (id, categoryData, signal) => {
    try {
        const response = await api.put(`/category`, categoryData, { signal });
        return response.data;
    } catch (error) {
        if (axios.isCancel(error)) {
            console.log(`Requisição de atualização de categoria ${id} cancelada`, error);
            return null;
        }
        console.error(`Erro ao atualizar o categoria ${id}`, error);
        throw error;
    }
};

export const deleteCategory = async (id, signal) => {
    try {
        await api.delete(`/category/${id}`, { signal });
        return;
    } catch (error) {
        if (axios.isCancel(error)) {
            console.log(`Requisição de exclusão de categoria ${id} cancelada`, error);
            return null;
        }
        console.error(`Erro ao excluir o categoria ${id}`, error);
        throw error;
    }
};

//Criar exceção de não deixar apagar brand/category estando atrelado a um produto  