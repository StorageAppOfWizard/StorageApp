import { useLocation } from "react-router-dom";

export function useCreateTabs() {
  const location = useLocation();

  const current = location.pathname.includes("marca")
    ? "brand"
    : location.pathname.includes("categoria")
    ? "category"
    : "product";

  const tabs = [
    { value: "product", label: "Criar Produto", to: "/criar/produto" },
    { value: "brand", label: "Criar Marca", to: "/criar/marca" },
    { value: "category", label: "Criar Categoria", to: "/criar/categoria" }
  ];

  return { tabs, current };
}
