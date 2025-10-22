import { productEndpoint } from "./products";
import { brandEndpoint } from "./brands";
import { categoryEndpoint } from "./categories";
import { authEndpoint } from "./auth";

export const endpointMap = {
  Product: productEndpoint,
  Brand: brandEndpoint,
  Category: categoryEndpoint,
  Auth: authEndpoint,
};