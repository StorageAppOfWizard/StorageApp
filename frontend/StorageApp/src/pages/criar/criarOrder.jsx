import { useLocation } from "react-router-dom";
import Tabs from "../../components/ui/Tabs";
import { useFetchApi } from "../../hooks/useFetchApi";
import { useMutateApi } from "../../hooks/useMutateApi";
import { useAuth } from "../../contexts/AuthContext";
import styles from "../../styles/pages/criacao.module.css";
import OrderForm from "../../components/layout/OrderForm";

export default function CriarOrder() {
  const location = useLocation();
  const { user } = useAuth();

  const productIdFromState = location.state?.productId;
  const profileName = user?.unique_name || user?.name || "";

  const { mutate, loading: creating } = useMutateApi("Order.OrdersCreate");

  const { data: todosProdutos } = useFetchApi("Product.ProductsGet");

  const { data: produtoSelecionadoNav } = useFetchApi(
    "Product.ProductsGet",
    { id: productIdFromState },
    { enabled: !!productIdFromState }
  );

  return (
    <div style={{ marginTop: "60px", padding: "20px" }}>
      <Tabs
        tabs={[
          { value: "pedidos", label: "Criar Pedido", to: "/criar/pedido" },
          { value: "ordem", label: "Últimos Pedidos", to: "/criar/ultimos-pedidos" },
        ]}
        currentValue="pedidos"
      />

      <div className={styles.container}>
        <div className={styles.criar}>
          <h2 className={styles.title}>Criar Pedido</h2>

          <OrderForm
            todosProdutos={todosProdutos}
            produtoSelecionadoNav={produtoSelecionadoNav}
            profileName={profileName}
            createOrder={mutate}
            creating={creating}
          />
        </div>
      </div>
    </div>
  );
}

