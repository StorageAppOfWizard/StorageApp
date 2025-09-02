//Arrumar essa pagina toda


import { useState } from "react";
import { useApi } from "../../hooks/useApi";
import { createProduct, createBrand, createCategory } from "../../services/productService";
import ProductForm from "../../components/ProductForm";
import styles from "../../styles/criar.module.css"; 
import { Plus } from "lucide-react";
import { useNavigate } from "react-router-dom"; 

export default function CreateProduto() {
  const navigate = useNavigate();
  const { data: brands, loading: brandsLoading } = useApi("brands");
  const { data: categories, loading: categoriesLoading } = useApi("categories");
  const [mode, setMode] = useState("product"); 
  const [brandName, setBrandName] = useState("");
  const [categoryName, setCategoryName] = useState("");

  const handleCreateProduct = async (data) => {
    try {
      await createProduct(data);
      navigate("/"); 
    } catch (error) {
      console.error(error);
    }
  };

  const handleCreateBrand = async () => {
    try {
      await createBrand({ name: brandName });
      navigate("/");
    } catch (error) {
      console.error(error);
    }
  };

  const handleCreateCategory = async () => {
    try {
      await createCategory({ name: categoryName });
      navigate("/");
    } catch (error) {
      console.error(error);
    }
  };

  if (brandsLoading || categoriesLoading) return <div>Carregando...</div>;

  return (
    <>
      <div style={{ marginTop: "60px", padding: "20px" }}>
        <h1>Criar Novo <Plus size={20} /></h1>
        <select value={mode} onChange={(e) => setMode(e.target.value)}>
          <option value="product">Produto Completo</option>
          <option value="brand">Só Marca</option>
          <option value="category">Só Categoria</option>
        </select>

        {mode === "product" && (
          <ProductForm 
            onSubmit={handleCreateProduct} 
            brands={brands} 
            categories={categories} 
          />
        )}

        {mode === "brand" && (
          <div>
            <input 
              placeholder="Nome da Marca" 
              value={brandName} 
              onChange={(e) => setBrandName(e.target.value)} 
            />
            <button onClick={handleCreateBrand}><Plus size={16} /> Criar Marca</button>
          </div>
        )}

        {mode === "category" && (
          <div>
            <input 
              placeholder="Nome da Categoria" 
              value={categoryName} 
              onChange={(e) => setCategoryName(e.target.value)} 
            />
            <button onClick={handleCreateCategory}><Plus size={16} /> Criar Categoria</button>
          </div>
        )}
      </div>
    </>
  );
}