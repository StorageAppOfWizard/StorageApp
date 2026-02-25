import Tabs from "../../components/ui/Tabs";
import { useLocation } from "react-router-dom";

export default function Create() {
  const location = useLocation();

  const current = location.pathname.includes("marca")
    ? "brand"
    : location.pathname.includes("categoria")
    ? "category"
    : "product"
    ? "pedidos"
    : "ordem"
    ;

  return (
    <div style={{ marginTop: "60px", padding: "20px" }}>
      <Tabs
        tabs={[
          { value: "product", label: "Criar Produto", to: "/criar/produto" },
          { value: "brand", label: "Criar Marca", to: "/criar/marca" },
          { value: "category", label: "Criar Categoria", to: "/criar/categoria" },
          { value: "pedidos", label: "Criar Pedido", to: "/criar/pedido" },
          { value: "ordem", label: "Últimos Pedidos", to: "/criar/ultimos-pedidos" },
        ]}
        currentValue={current}
      />
    </div>
  );
}
