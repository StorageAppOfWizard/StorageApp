import { useState } from "react";
import { Plus } from "lucide-react";
import "./CategoryForm.css"

export default function CategoryForm({ onSubmit }) {
  const [categoryName, setCategoryName] = useState("");

  const handleSubmit = () => {
    if (!categoryName) return;
    onSubmit({ name: categoryName });
    setCategoryName("");
  };

  return (
    <div className="form">
      <input 
        className="inputMarca"
        placeholder="Nome da Categoria" 
        value={categoryName} 
        onChange={(e) => setCategoryName(e.target.value)} 
      />
      <button onClick={handleSubmit} className="btnSubmit">
        <Plus size={16} /> Criar Categoria
      </button>
    </div>
  );
}
