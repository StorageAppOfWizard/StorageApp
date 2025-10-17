import axios from 'axios';

const API_URL = "http://localhost:5000/"

export const signIn = async(email, password) =>{
    try{
        const response = await axios.post(`${API_URL}/auth/login`,{
            email,
            password,
        });
        return response.data;
    } catch (error) {
        throw new Error(error.response?.data?.message || 'Erro ao efetuar o login')
    }
};