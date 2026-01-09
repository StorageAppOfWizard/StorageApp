import { memo } from "react";
import styles from "../../styles/pages/order.module.css";
import { useFetchApi } from "../../hooks/useFetchApi";

const OrderRow = ({ order }) => {

  const { data: productId, loading } = useFetchApi(`Product.ProductGetById`, { id: order.productId });
  console.log(order);
  


  if (loading) { 
    return (
      <tr>
        <td colSpan="6">Carregando...</td>
      </tr>
    );
  }

  if (!productId) {
    return (
      <tr>
        <td colSpan="6">Produto não encontrado</td>
      </tr>
    );
  }

  return (
    <tr>
      <td>{productId.value.name}</td>
      <td>{order.userName || "Sem Usuário"}</td>
      <td>{order.quantity}</td>
      <td>
        <span
          className={styles.statusDot}
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
      <td>{new Date(order.creationDate).toLocaleDateString()}</td>
    </tr>
  );
};

export default memo(OrderRow);
