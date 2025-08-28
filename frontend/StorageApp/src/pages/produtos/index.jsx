import { useState } from "react";
import Header from "../../components/Header";
import { useApi } from "../../hooks/useApi";

import "react-loading-skeleton/dist/skeleton.css"
import Skeleton from "react-loading-skeleton"
import styles from "../../styles/produtos.module.css";
import { Edit } from "lucide-react";

export default function Produtos() {
  const { data: products, loading, error } = useApi("products", 10);
  const [editableStock, setEditableStock] = useState(null);

  const handleStockEdit = (productId, newStock) => {
    setEditableStock(null);
    console.log(`Novo estoque para produto ${productId}: ${newStock}`);
  };

  if (loading) return (
    <div style={{ marginTop: "60px", padding: "20px" }}>
      <div className={styles.skeletonContainer}>
        <table className={styles.productTable}>
          <thead>
            <tr>
              <th><Skeleton height={20} /></th>
              <th><Skeleton height={20} /></th>
              <th><Skeleton height={20} /></th>
              <th><Skeleton height={20} /></th>
              <th><Skeleton height={20} /></th>
              <th><Skeleton height={20} /></th>
              <th><Skeleton height={20} /></th>
            </tr>
          </thead>
          <tbody>
            {Array(5).fill().map((_, index) => (
              <tr key={index}>
                <td><Skeleton height={50} width={50} /></td>
                <td><Skeleton height={20} width={150} /></td>
                <td><Skeleton height={20} width={120} /></td>
                <td><Skeleton height={20} width={120} /></td>
                <td><Skeleton height={20} width={80} /></td>
                <td><Skeleton height={20} width={50} /></td>
                <td><Skeleton height={20} width={80} /></td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
  if (error) return (
    <div style={{ marginTop: "60px", padding: "20px" }}>
      <p className={styles.error}>{error}</p>
    </div>
  );

  return (
    <>
      <Header profileName="Luiz" />
      <div style={{ marginTop: "60px", padding: "20px" }}>
        <div className={styles.container}>
          <div className={styles.header}>
            <h2>
              <span className={styles.itemCount}>{products.length} itens cadastrados</span>
            </h2>
            <div className={styles.actions}>
              <input type="text" placeholder="Item, valor ou código" className={styles.search} />
              <select className={styles.filter}>
                <option>Filtrar</option>
              </select>
              <button className={styles.export}>Exportar</button>
              <button className={styles.addProduct}>+ Produto</button>
            </div>
          </div>
          <table className={styles.productTable}>
            <thead>
              <tr>
                <th>Imagem</th>
                <th>Nome</th>
                <th>Categoria</th>
                <th>Marca</th>
                <th>Estoque</th>
                <th>Status</th>
                <th>Ações</th>
              </tr>
            </thead>
            <tbody>
              {products.map((product) => (
                <tr key={product.id}>
                  <td>
                    <img
                      src={product.thumbnail}
                      alt={product.title}
                      className={styles.productImage}
                    />
                  </td>
                  <td>{product.title}</td>
                  <td>{product.category || "Sem categoria"}</td>
                  <td>{product.brand}</td>
                  <td>
                    {editableStock === product.id ? (
                      <input
                        type="number"
                        defaultValue={product.stock}
                        onBlur={(e) => handleStockEdit(product.id, e.target.value)}
                        onKeyPress={(e) => e.key === "Enter" && handleStockEdit(product.id, e.target.value)}
                        onInput={(e) => {
                          if (e.target.value.length > 8) {
                            e.target.value = e.target.value.slice(0, 8);
                          }
                        }}
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
                          product.stock > 150 ? "#4caf50" :
                            product.stock > 50 ? "#ffca28" :
                              "#f44336",
                      }}
                    ></span>
                  </td>
                  <td>
                    <span className={styles.actionIcon}>✏️</span>
                    <span className={styles.actionIcon}>🗑️</span>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      </div>
    </>
  );
}