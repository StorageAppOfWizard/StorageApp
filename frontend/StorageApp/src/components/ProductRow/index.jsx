import { memo } from "react";
import { Edit, Trash2 } from "lucide-react";
import styles from "../../styles/produtos.module.css";

const ProductRow = ({ product, editableStock, onStockEdit, onEdit, onDelete, setEditableStock }) => {

    return (
        <tr>
            <td>{product.name}</td>
            <td>{product.categoryName || "Sem categoria"}</td>
            <td>{product.brand}</td>
            <td>
                {editableStock === product.id ? (
                    <input
                        type="number"
                        defaultValue={product.stock}
                        onBlur={(e) => onStockEdit(product.id, e.target.value)}
                        onKeyPress={(e) => e.key === "Enter" && onStockEdit(product.id, e.target.value)}
                        autoFocus
                        className={styles.stockInput}
                        min="0"
                    />
                ) : (
                    <span
                        className={styles.stockValue}
                        onClick={() => setEditableStock(product.id)}
                    >
                        {product.stock}
                        <Edit className={styles.editIcon} size={16} />
                    </span>
                )}
            </td>
            <td>
                <span
                    className={styles.statusDot}
                    style={{
                        backgroundColor:
                            product.status === "Available"
                                ? "#4caf50"
                                : product.status === "LowStock"
                                    ? "#ffca28"
                                    : "#f44336",
                    }}
                />
            </td>
            <td>
                <span className={styles.actionIcon} onClick={() => onEdit(product.id)}>
                    <Edit size={16} />
                </span>
                <span className={styles.actionIcon} onClick={() => onDelete(product.id)}>
                    <Trash2 size={16} />
                </span>
            </td>
        </tr>
    );
};

export default memo(ProductRow);