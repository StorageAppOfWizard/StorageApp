import styles from "../../styles/pages/order.module.css";
import { useFetchApi } from './../../hooks/useFetchApi';
import { useNavigate, Link } from "react-router-dom";
import { Plus } from "lucide-react";
import OrderRow from "../../components/OrdersRow";
import { useEffect, useState } from 'react';
import ProductTableSkeleton from "../../components/ProductTableSkeleton";


export default function Orders() {

  const navigate = useNavigate();
  
  const [localOrders, setLocalOrders] = useState([]);
  const [inputSearch, setInputSearch] = useState("");

  const {
    data: orders,
    loading,
    error,
  } = useFetchApi("Order.OrdersGet");

  useEffect(() => {
    if (Array.isArray(orders)) {
      setLocalOrders(orders);
    }
  }, [orders]);

  // const getStatusOrder = (status) => {
  //   if (status === "Pending") return "Pending";
  //   if (status === "Approved") return "Approved";
  //   return "Rejected";
  // };


  const filteredOrders = localOrders.filter((p) => {
    const s = inputSearch.trim().toLowerCase();
    if (!s) return true;

    return [p.name, p.categoryName, p.brand]
      .map((v) => (v ?? "").toString().toLowerCase())
      .some((field) => field.includes(s));
  });


  if (loading) return <ProductTableSkeleton />;
  if (error) {
    return (
      <div style={{ marginTop: "60px", padding: "20px" }}>
        <p className={styles.error}>{error}</p>
      </div>
    );
  }

  return (
    <div style={{ marginTop: "60px", padding: "20px" }}>
      <div className={styles.container}>
        <div className={styles.header}>
          <h2>
            <span className={styles.itemCount}>
              {filteredOrders.length} itens cadastrados
            </span>
          </h2>

          <div className={styles.actions}>
            <input
              type="text"
              placeholder="Item, Categoria ou Marca"
              className={styles.search}
              value={inputSearch}
              onChange={(e) => setInputSearch(e.target.value)}
            />

            <button className={styles.export}>Exportar</button>

            <Link to="/criar">
              <button className={styles.addProduct}>
                <Plus size={16} /> Produto
              </button>
            </Link>
          </div>
        </div>

        <table className={styles.productTable}>
          <thead>
            <tr>
              <th>Nome</th>
              <th>Categoria</th>
              <th>Marca</th>
              <th>Estoque</th>
              <th>Status</th>
              <th>Ações</th>
            </tr>
          </thead>

          <tbody>
            {filteredOrders.length === 0 ? (
              <tr>
                <td colSpan="6">Nenhum produto cadastrado</td>
              </tr>
            ) : (
              filteredOrders.map((order) => (
                <OrderRow
                  key={order.id}
                  order={order}
                />
              ))
            )}
          </tbody>
        </table>
      </div>
    </div>
  );
}