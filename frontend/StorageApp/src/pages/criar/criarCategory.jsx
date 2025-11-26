import { useState } from "react";
import Tabs from "../../components/Tabs";
import { useCreateTabs } from "../../hooks/components/useCreateTabs"

export default function CriarCategory() {
  const { tabs, current } = useCreateTabs();

  const [nome, setNome] = useState("");

  function handleSubmit(e) {
    e.preventDefault();
    alert("Categoria criada!");
  }

  return (
    <div style={{ marginTop: "60px", padding: "20px" }}>
      <Tabs
        tabs={[
          { value: "product", label: "Criar Produto", to: "/criar/produto" },
          { value: "brand", label: "Criar Marca", to: "/criar/marca" },
          { value: "category", label: "Criar Categoria", to: "/criar/categoria" }
        ]}
        currentValue={current}
      />

      <h2>Criar Categoria</h2>

      <form onSubmit={handleSubmit} className="form">
        <label>
          Nome da Categoria:
          <input
            type="text"
            value={nome}
            onChange={(e) => setNome(e.target.value)}
          />
        </label>

        <button className="btn" type="submit">Criar Categoria</button>
      </form>
    </div>
  );
}
