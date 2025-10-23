import { getCategories, createCategory, updateCategory, deleteCategory } from "../services/categoryService";

export const categoryEndpoint = {
  CategoriesGet :{
    fn: getCategories,
  },

  CategoryCreate: {
    fn: createCategory,
  },

  CategoryUpdate: {
    fn: updateCategory,
  },

  CategoryDelete: {
    fn: deleteCategory,
  },


};