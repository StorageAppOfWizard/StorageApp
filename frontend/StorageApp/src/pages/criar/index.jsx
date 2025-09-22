import { useState } from "react";
import { useApi } from "../../hooks/useApi";
import { createProduct } from "../../services/productService";
import { createBrand } from "../../services/brandService";
import { createCategory } from "../../services/categoryService";

import ProductForm from "../../components/ProductForm";
import BrandForm from "../../components/BrandForm";
import CategoryForm from "../../components/CategoryForm";
import Tabs from "../../components/Tabs";

import { Plus } from "lucide-react";
import { useNavigate } from "react-router-dom";

export default function CreateProduto() {
  const navigate = useNavigate();
  const { data: brands, loading: brandsLoading } = useApi("brands");
  const { data: categories, loading: categoriesLoading } = useApi("categories");
  const [mode, setMode] = useState("product");

  const handleCreateProduct = async (data) => {
    try {
      await createProduct(data);
      navigate("/");
    } catch (error) {
      console.error(error);
    }
  };

  const handleCreateBrand = async (data) => {
    try {
      await createBrand(data);
      navigate("/");
    } catch (error) {
      console.error(error);
    }
  };

  const handleCreateCategory = async (data) => {
    try {
      await createCategory(data);
      navigate("/");
    } catch (error) {
      console.error(error);
    }
  };

  if (brandsLoading || categoriesLoading) return <div>Carregando...</div>;

  return (
    <div style={{ marginTop: "60px", padding: "20px" }}>

      <Tabs
        tabs={[
          { value: "product", label: "Produto Completo" },
          { value: "brand", label: "Só Marca" },
          { value: "category", label: "Só Categoria" }
        ]}
        defaultValue="product"
        onChange={(val) => setMode(val)}
      />

      {mode === "product" && (
        <ProductForm 
          onSubmit={handleCreateProduct} 
          brands={brands} 
          categories={categories} 
        />
      )}

      {mode === "brand" && (
        <BrandForm onSubmit={handleCreateBrand} />
      )}

      {mode === "category" && (
        <CategoryForm onSubmit={handleCreateCategory} />
      )}
    </div>
  );
}
