import { getCategory, getCategoryById, createCategory, updateCategory, deleteCategory } from "../services/categoryService";

export const categoryEndpoint = {
  CategorysGet: {
    fn: getCategory,
  },

  CategoriesByidGet: {
    fn: getCategoryById,
  },

  CategoryCreate: {
    fn: createCategory,
    isMutation: true,
  },

  CategoryUpdate: {
    fn: updateCategory,
    isMutation: true,
  },

  CategoryDelete: {
    fn: deleteCategory,
    isMutation: true,
  },


};