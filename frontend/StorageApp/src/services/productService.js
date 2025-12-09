import { apiMain as api } from "./api";


export const getProducts = async ({ signal }) => {
    const response = await api.get("/product", { signal });
    return response.data.value || response.data || [];
};


export const getProductById = async ({ id, signal }) => {
    const response = await api.get(`/product/${id}`, { signal });
    return response.data;

};


export const createProduct = async (data, signal) => {
  const response = await api.post("/product", data, { signal });
  return response.data;
};



export const updateProductStock = async ({ id, newStock, signal }) => {
    const response = await api.patch(
      "/product/editQuantity",
      { id, quantity: newStock },
      { signal }
    );
    return response.data;
};


export const updateProduct = async (data) => {
  const response = await api.put("/product", data);
  return response.data;
};



export const deleteProduct = async ({ id, signal }) => {
    await api.delete(`/product/${id}`, { signal });
};
