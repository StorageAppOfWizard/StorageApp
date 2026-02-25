import { useState } from "react";
import { Plus } from "lucide-react";
import './BrandForm.css'

export default function BrandForm({ onSubmit }) {
  const [brandName, setBrandName] = useState("");

  const handleSubmit = () => {
    if (!brandName) return;
    onSubmit({ name: brandName });
    setBrandName("");
  };

  return (
    <div className="form">
      <input 
        className="inputMarca"
        placeholder="Nome da Marca" 
        value={brandName} 
        onChange={(e) => setBrandName(e.target.value)} 
      />
      <button onClick={handleSubmit} className="btnSubmit">
        <Plus size={16} /> Criar Marca
      </button>
    </div>
  );
}
