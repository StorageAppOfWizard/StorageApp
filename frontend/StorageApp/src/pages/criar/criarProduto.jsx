import { useState } from "react";
import Tabs from "../../components/Tabs";

export default function CriarProduto() {

  const [form, setForm] = useState({
    nome: "",
    preco: "",
    descricao: "",
  });

  function handleChange(e) {
    setForm({ ...form, [e.target.name]: e.target.value });
  }

  function handleSubmit(e) {
    e.preventDefault();
    alert("Produto criado!");
  }

  return (
    <div style={{ marginTop: "60px", padding: "20px" }}>
      <Tabs
        tabs={[
          { value: "product", label: "Criar Produto", to: "/criar/produto" },
          { value: "brand", label: "Criar Marca", to: "/criar/marca" },
          { value: "category", label: "Criar Categoria", to: "/criar/categoria" }
        ]}
        currentValue="product"
      />

      <h2>Criar Produto</h2>

      <form onSubmit={handleSubmit} className="form">
        <label>
          Nome do Produto:
          <input
            type="text"
            name="name"
            value={form.name}
            onChange={handleChange}
          />
        </label>

        <label>
          Quantidade:
          <input
            type="number"
            name="quantity"
            value={form.quantity}
            onChange={handleChange}
          />
        </label>

        <label>
          Marca:
          <textarea
            name="brand"
            value={form.brand}
            onChange={handleChange}
          />
        </label>

        <label>
          Categoria:
          <textarea
            name="category"
            value={form.category}
            onChange={handleChange}
          />
        </label>

        <label>
          Descrição:
          <textarea
            name="descricao"
            value={form.descricao}
            onChange={handleChange}
          />
        </label>

        <button className="btn" type="submit">Criar Produto</button>
      </form>
    </div>
  );
}
