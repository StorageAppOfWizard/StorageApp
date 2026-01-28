import { useState } from "react";
import Tabs from "../../components/Tabs";
import { useFetchApi } from "../../hooks/useFetchApi";
import { useMutateApi } from "../../hooks/useMutateApi";
import styles from "../../styles/pages/criacao.module.css";
import stylesOrder from "../../styles/pages/order.module.css";

export default function UltimosPedidos() {


  const {
    data: orders, 
    loading: ordersLoading, 
    error: ordersError, 
    refetch: refetchOrders
  } = useFetchApi("Order.OrdersGet", {}, { pageQuantity: 10 });
  const { mutate, loading: creating } = useMutateApi("Order.OrdersCreate")

  return (
    <div style={{ marginTop: "60px", padding: "20px" }}>
      <Tabs
        tabs={[
          { value: "pedidos", label: "Criar Pedido", to: "/criar/pedido" },
          { value: "ordem", label: "Últimos Pedidos", to: "/criar/ultimos-pedidos" },
        ]}
        currentValue="ordem"
      />

        <div className={styles.cadastrado}>
          <h3 className={styles.subtitle}>Últimos Pedidos Cadastrados</h3>

          {ordersLoading && <p>Carregando...</p>}
          {ordersError && <p style={{ color: "red" }}>{ordersError}</p>}

          {!ordersLoading && (
            <div className={styles.tableContainer}>
              <table className={styles.table}>
                <thead>
                  <tr>
                    <th>Produto</th>
                    <th>Quantidade</th>
                    <th>Status</th>
                  </tr>
                </thead>
                <tbody>
                  {orders.items?.map((order) => (
                    ordersLoading ? (
                      <tr key={order.id}>
                        <td>Carregando...</td>
                      </tr>
                    ) : (
                      <tr key={order.id}>
                        <td>{order.productName}</td>
                        <td>{order.quantity}</td>
                        <td>
                          <span
                            className={stylesOrder.statusDot}
                            style={{
                              backgroundColor:
                                order.status === "Approved"
                                  ? "#4caf50"
                                  : order.status === "Pending"
                                    ? "#ffca28"
                                    : "#f44336",
                            }}
                          />
                        </td>
                      </tr>
                    )))}
                </tbody>
              </table>
            </div>
          )}
        </div>
      </div>
  );
}
