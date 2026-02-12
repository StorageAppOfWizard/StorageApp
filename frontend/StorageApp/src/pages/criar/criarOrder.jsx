import { useState, useEffect } from "react";
import { useLocation } from "react-router-dom";
import Tabs from "../../components/Tabs";
import { useFetchApi } from "../../hooks/useFetchApi";
import { useMutateApi } from "../../hooks/useMutateApi";
import { useToast } from "../../hooks/useToast";
import { useAuth } from "../../contexts/AuthContext";
import styles from "../../styles/pages/criacao.module.css";

export default function CriarOrder() {
  const location = useLocation();
  const toast = useToast();
  const { user } = useAuth();

  const productIdFromState = location.state?.productId;
  const profileName = user?.unique_name || user?.name || "";

  const { mutate, loading: creating } = useMutateApi("Order.OrdersCreate");

  const { data: todosProdutos } = useFetchApi("Product.ProductsList", {});

  const { data: produtoSelecionadoNav } = useFetchApi(
    "Product.ProductsGet",
    { id: productIdFromState },
    { enabled: !!productIdFromState }
  );

  const [selectedProductId, setSelectedProductId] = useState("");
  const [form, setForm] = useState({
    productId: "",
    quantity: 1,
    deliveryDate: "",
    requester: "",
    orderDate: "",
    category: "",
    brand: "",
    stock: 0
  });

  useEffect(() => {
    const produto = produtoSelecionadoNav?.items?.[0];
    
    if (!produto) return;

    setSelectedProductId(produto.id);
    setForm((prev) => ({
      ...prev,
      productId: produto.id,
      category: produto.categoryName,
      brand: produto.brandName,
      stock: produto.quantity,
      requester: profileName,
      orderDate: new Date().toISOString().split("T")[0]
    }));
  }, [produtoSelecionadoNav, profileName]);

  useEffect(() => {
    if (!selectedProductId || !todosProdutos?.items) return;

    const produtoSelecionado = todosProdutos.items.find(
      (p) => p.id === selectedProductId
    );

    if (!produtoSelecionado) return;

    setForm((prev) => ({
      ...prev,
      productId: produtoSelecionado.id,
      category: produtoSelecionado.categoryName,
      brand: produtoSelecionado.brandName,
      stock: produtoSelecionado.quantity,
      requester: profileName,
      orderDate: new Date().toISOString().split("T")[0]
    }));
  }, [selectedProductId, todosProdutos, profileName]);

  function handleProductSelect(e) {
    setSelectedProductId(e.target.value);
  }

  function handleChange(e) {
    const { name, value } = e.target;
    setForm((prev) => ({ ...prev, [name]: value }));
  }

  async function handleSubmit(e) {
    e.preventDefault();

    try {
      await mutate({
        productId: form.productId,
        quantity: Number(form.quantity),
        deliveryDate: form.deliveryDate
      });

      toast.success("Pedido criado com sucesso!");
      

      setSelectedProductId("");
      setForm({
        productId: "",
        quantity: 1,
        deliveryDate: "",
        requester: profileName,
        orderDate: new Date().toISOString().split("T")[0],
        category: "",
        brand: "",
        stock: 0
      });
    } catch (err) {
      toast.error("Erro ao criar pedido.");
    }
  }

  const produtoAtual = todosProdutos?.items?.find(
    (p) => p.id === selectedProductId
  );

  return (
    <div style={{ marginTop: "60px", padding: "20px" }}>
      <Tabs
        tabs={[
          { value: "pedidos", label: "Criar Pedido", to: "/criar/pedido" },
          { value: "ordem", label: "Últimos Pedidos", to: "/criar/ultimos-pedidos" }
        ]}
        currentValue="pedidos"
      />

      <div className={styles.container}>
        <div className={styles.criar}>
          <h2 className={styles.title}>Criar Pedido</h2>

          <form onSubmit={handleSubmit} className={styles.formGrid}>
            <div className={styles.left}>
              <label className={styles.label}>Produto</label>
              <select
                className={styles.input}
                value={selectedProductId}
                onChange={handleProductSelect}
                required
              >
                <option value="">Selecione um produto</option>
                {todosProdutos?.items?.map((produto) => (
                  <option key={produto.id} value={produto.id}>
                    {produto.name}
                  </option>
                ))}
              </select>

              <label className={styles.label}>Solicitante</label>
              <input
                className={styles.input}
                type="text"
                value={form.requester}
                readOnly
              />

              <label className={styles.label}>Quantidade</label>
              <input
                className={styles.input}
                type="number"
                name="quantity"
                value={form.quantity}
                min={1}
                max={form.stock || 999}
                onChange={handleChange}
                required
              />

              <label className={styles.label}>Data do Pedido</label>
              <input
                className={styles.input}
                type="date"
                value={form.orderDate}
                readOnly
              />
            </div>

            <div className={styles.right}>
              <label className={styles.label}>Data de Entrega</label>
              <input
                className={styles.input}
                type="date"
                name="deliveryDate"
                value={form.deliveryDate}
                onChange={handleChange}
                required
              />

              <label className={styles.label}>Categoria</label>
              <input 
                className={styles.input} 
                value={form.category} 
                readOnly 
              />

              <label className={styles.label}>Marca</label>
              <input 
                className={styles.input} 
                value={form.brand} 
                readOnly 
              />

              <label className={styles.label}>Estoque</label>
              <input 
                className={styles.input} 
                value={form.stock} 
                readOnly 
              />

              <button 
                className={styles.btn} 
                disabled={creating || !selectedProductId}
              >
                {creating ? "Salvando..." : "Criar Pedido"}
              </button>
            </div>
          </form>
        </div>
      </div>
    </div>
  );
}