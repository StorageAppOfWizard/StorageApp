import { useState } from "react";
import { Plus, Save, X } from "lucide-react";

const ProductForm = ({ onsubmit, brands, categories, initialData = {} }) => {
    const [formData, setFormData] = useState({
        title: initialData.title || "",
        category: initialData.category || "",
        brand: initialData.brand || "",
        stock: initialData.stock || "",
        thumbnail: initialData.thumbnail || "",
    });

    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData((prev) => ({ ...prev, [name]: value }));
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        onsubmit(formData);
    };


    return (
        <form onSubmit={handleSubmit}>
            <input name="title" placeholder="Nome" value={formData.title} onChange={handleChange} required />
            <select name="category" value={formData.category} onChange={handleChange} required>
                <option value="">Selecione Categoria</option>
                {categories.map((cat) => <option key={cat} value={cat}>{cat}</option>)}
            </select>
            <select name="brand" value={formData.brand} onChange={handleChange} required>
                <option value="">Selecione Marca</option>
                {brands.map((brand) => <option key={brand} value={brand}>{brand}</option>)}
            </select>
            <input type="number" name="stock" placeholder="Estoque" value={formData.stock} onChange={handleChange} required min="0" />
            <input name="thumbnail" placeholder="URL da Imagem" value={formData.thumbnail} onChange={handleChange} required />
            <button type="submit"><Save size={16} /> Salvar</button>
            <button type="button" onClick={() => {}}><X size={16} /> Cancelar</button>
        </form>
    );
};

export default ProductForm;