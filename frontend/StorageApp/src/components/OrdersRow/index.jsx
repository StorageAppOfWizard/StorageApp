import { memo } from "react";
import styles from "../../styles/pages/order.module.css";
import { useFetchApi } from "../../hooks/useFetchApi";
import { CircleCheckBig, CircleOff } from "lucide-react";

const OrderRow = ({ order, onApprove, onReject }) => {

  const { data: productId, loading } = useFetchApi(`Product.ProductGetById`, { id: order.productId },{enabled: !!order.productId});

  return (
    loading ? (
      <tr>
        <td colSpan="6">Carregando...</td>
      </tr>
    ) :
      !productId ? (
        <tr>
          <td colSpan="6">Produto não encontrado</td>
        </tr>
      ) : (
        <tr>
          <td>{order.productName || "Nome não disponível"}</td>
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
          <td>
                <span className={styles.actionIcon} onClick={() => onApprove(order.id)}>
                    <CircleCheckBig size={16} />
                </span>
                <span className={styles.actionIcon} onClick={() => onReject(order.id)}>
                    <CircleOff size={16} />
                </span>
            </td>
        </tr>
      )
  )
}

export default memo(OrderRow);
