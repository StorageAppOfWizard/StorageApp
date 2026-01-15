import { apiMain as api } from "./api";


export const getOrders = async ({ signal }) => {
    const response = await api.get("/order", { signal });
    return response.data.value || response.data || [];
};
export const getMyOrders = async ({ signal }) => {
    const response = await api.get("/order/my-orders", { signal });
    return response.data.value || response.data || [];
};

export const createOrders = async(data,signal) => {
    const response = await api.post("/order/create-order", data, { signal });    
    return response.data;
};

export const approveOrder = async({id,signal}) => {
    const response = await api.patch(`/order/approve-order/${id}`,null, { signal });    
    return response.data;
};

export const rejectOrder = async({id,signal}) => {
    const response = await api.patch(`/order/reject-order/${id}`, null, { signal });    
    return response.data;
};

