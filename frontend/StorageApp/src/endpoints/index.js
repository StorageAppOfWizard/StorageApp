import { productEndpoint } from "./products";
import { brandEndpoint } from "./brands";
import { categoryEndpoint } from "./categories";
import { authEndpoint } from "./auth";
import { orderEndpoint } from "./orders";

export const endpointMap = {
  Product: productEndpoint,
  Order: orderEndpoint,
  Brand: brandEndpoint,
  Category: categoryEndpoint,
  Auth: authEndpoint,
};