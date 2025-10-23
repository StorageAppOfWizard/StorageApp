import { getProducts, getProductById, createProduct, updateProductStock, updateProduct, deleteProduct  } from "../services/productService";

export const productEndpoint = {

  ProductsGet: {
    fn: getProducts,
  },

  ProductGetById: {
    fn: getProductById,
  },

  ProductCreate: {
    fn: createProduct,
    isMutation: true,
  },
  
  ProductUpdateStock: {
    fn: updateProductStock,
    isMutation: true,
  },

  ProductUpdate: {
    fn: updateProduct,
    isMutation: true,
  },

  ProductDelete: {
    fn: deleteProduct,
    isMutation: true,
  },

};