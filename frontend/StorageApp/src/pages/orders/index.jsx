import styles from "../../styles/pages/order.module.css";
import { useFetchApi } from './../../hooks/useFetchApi';
import { useMutateApi } from "../../hooks/useMutateApi";
import { Link } from "react-router-dom";
import { Plus } from "lucide-react";
import OrderRow from "../../components/OrdersRow";
import { useEffect, useState } from 'react';
import ProductTableSkeleton from "../../components/ProductTableSkeleton";
import { useToast } from "../../hooks/useToast";

export default function Orders() {
  const [localOrders, setLocalOrders] = useState([]);
  const [inputSearch, setInputSearch] = useState("");
  
  const { data: orders, loading, error } = useFetchApi("Order.OrdersMyOrdersGet");
  const toast = useToast();
  const { 
    mutate: mutateReject, 
    mutationResult: rejectResult,
    error: rejectError 
  } = useMutateApi("Order.OrdersReject");
  
  const { 
    mutate: mutateApprove,
    mutationResult: approveResult,
    error: approveError
  } = useMutateApi("Order.OrdersApprove");


  const onReject = async (id) => {
    await mutateReject({ id }, {
      onSuccess: () => { toast.success("Pedido rejeitado com sucesso! ", rejectResult); },
      onError: () => { toast.error("Erro ao rejeitar pedido :", rejectError); }
    }
    );
  };

  const onApprove = async (id) => {
    await mutateApprove({ id }, {
      onSuccess: () => { toast.success("Pedido aprovado com sucesso! ", approveResult); },
      onError: () => { toast.error("Erro ao aprovar pedido :", approveError); }
    }
    );
  };


  useEffect(() => {
    if (Array.isArray(orders)) {
      setLocalOrders(orders);
    }
  }, [orders]);

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
              {localOrders.length} pedidos cadastrados
            </span>
          </h2>

          <div className={styles.actions}>
            <input
              type="text"
              placeholder="Solicitante, Nome Produto"
              className={styles.search}
              value={inputSearch}
              onChange={(e) => setInputSearch(e.target.value)}
            />

            <button className={styles.export}>Exportar</button>

            <Link to="/criar/pedido">
              <button className={styles.addProduct}>
                <Plus size={16} /> Pedido
              </button>
            </Link>
          </div>
        </div>

        <table className={styles.productTable}>
          <thead>
            <tr>
              <th>Nome do Produto</th>
              <th>Solicitante</th>
              <th>Quantidade</th>
              <th>Status Pedido</th>
              <th>Data do Pedido</th>
              <th>Ações</th>
            </tr>
          </thead>

          <tbody>
            {localOrders.length === 0 ? (
              <tr>
                <td colSpan="6">Nenhum produto cadastrado</td>
              </tr>
            ) : (
              localOrders.map((order) => (
                <OrderRow
                  key={order.id}
                  order={order}
                  onApprove={onApprove}
                  onReject={onReject}
                />
              ))
            )}
          </tbody>
        </table>
      </div>
    </div>
  );
}