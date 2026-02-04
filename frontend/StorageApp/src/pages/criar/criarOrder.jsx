import { useState } from "react";
import Tabs from "../../components/Tabs";
import { useFetchApi } from "../../hooks/useFetchApi";
import { useMutateApi } from "../../hooks/useMutateApi";
import styles from "../../styles/pages/criacao.module.css";
import stylesOrder from "../../styles/pages/order.module.css";
import { useToast } from "../../hooks/useToast";

export default function CriarOrder() {


  const { data: produtos, loading} = useFetchApi("Product.ProductsGet");

  const {
    data: orders, 
    loading: ordersLoading, 
    error: ordersError, 
    refetch: refetchOrders
  } = useFetchApi("Order.OrdersGet", {}, { pageQuantity: 10 });
  const toast = useToast();
  const { mutate, loading: creating } = useMutateApi("Order.OrdersCreate")


  const [form, setForm] = useState({
    productId: "",
    quantity: 0,
  });

  function handleChange(e) {
    setForm({ ...form, [e.target.name]: e.target.value });
  }

  const handleSubmit = async (e) => {
    e.preventDefault();

    await mutate(
      {
        productId: form.productId,
        quantity: Number(form.quantity)
      },
      {
        onSuccess: () => {
          setForm({
            productId: "",
            quantity: 0,
          })
          toast.success("Pedido criado com sucesso!");
          refetchOrders();
        },

        onError: (err) => {
          toast.error(`Erro ao criar pedido: ${err.response.data.errors}`);
        }
      }
    );

  };

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
          <h2 className={styles.title}>Criar Pedido
          </h2>

          <form onSubmit={handleSubmit} className={styles.form}>
            <label className={styles.label}>Produto</label>
            <select
              className={styles.input}
              name="productId"
              value={form.productId}
              onChange={handleChange}
              required
            >
              <option value="">Selecione um produto</option>
              {produtos.items?.map((m) => (
                <option key={m.id} value={m.id}>{m.name}</option>
              ))}
            </select>

            <label className={styles.label}>Quantidade</label>
            <input
              className={styles.input}
              type="number"
              name="quantity"
              value={form.quantity}
              onChange={handleChange}
              required
            />


            <button className={styles.btn} disabled={creating}>
              {creating ? "Salvando..." : "Criar Pedido"}
            </button>
          </form>

        </div>
      </div>

    </div>
  );
}
