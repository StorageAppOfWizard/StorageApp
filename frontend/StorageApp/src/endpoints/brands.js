import { getBrands, getBrandsById ,createBrand, updateBrand, deleteBrand } from "../services/brandService";

export const brandEndpoint = {
  BrandsGet:{
    fn: getBrands,
  },

  BrandsByidGet:{
    fn: getBrandsById,
  },
  
  BrandsCreate: {
    fn: createBrand,
    isMutation:true,
  },

  BrandsUpdate: {
    fn: updateBrand,
    isMutation:true,
  },

  BrandsDelete: {
    fn: deleteBrand,
    isMutation:true,
  },
  
};