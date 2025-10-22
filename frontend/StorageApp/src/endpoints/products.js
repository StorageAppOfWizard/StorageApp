import { getProducts } from "../services/productService";

export const productEndpoint = {

  ProductsGet: {
    fn: getProducts,
  }

};