import { apiMain as api } from "./api";


export const getOrders = async ({ signal }) => {
    const response = await api.get("/order", { signal });
    return response.data.value || response.data || [];
};
export const getMyOrders = async ({ signal }) => {
    const response = await api.get("/order/my-orders", { signal });
    return response.data.value || response.data || [];
};

