import Tabs from "../../components/Tabs";
import { useLocation } from "react-router-dom";

export default function Create() {
  const location = useLocation();

  const current = location.pathname.includes("marca")
    ? "brand"
    : location.pathname.includes("categoria")
    ? "category"
    : "product";

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
    </div>
  );
}
