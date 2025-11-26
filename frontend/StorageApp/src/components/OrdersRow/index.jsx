import { memo } from "react";
import styles from "../../styles/pages/order.module.css";
import axios from "axios";



const OrderRow = ({ order }) => {

   
    const getProductName = async (productId) => {
        const { data } = await axios.get(`/product/${productId}`);
  return `Product ${data.name}`;
    }

    return (
        <tr>
            <td>{getProductName(order.productId)}</td>
            <td>{order.userId || "Sem Usuário"}</td>
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