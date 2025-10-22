import { getBrands, createBrand } from "../services/brandService";

export const brandEndpoint = {
  BrandsGet:{
    fn: getBrands,
  },
  
  BrandsCreate: {
    fn: createBrand,
    IsMutating:true,
  },
  
};