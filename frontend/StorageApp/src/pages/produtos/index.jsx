//Diminuir a quantidade de itens nessa pagina

import { useState } from "react";
import { useNavigate, Link } from "react-router-dom";
import { useApi } from "../../hooks/useApi";
import ProductTableSkeleton from "../../components/ProductTableSkeleton";
import styles from "../../styles/produtos.module.css";
import { Edit, Plus, Trash2 } from "lucide-react";
import { toast } from "react-toastify";
import { deleteProduct, getProducts, updateProductStock } from "../../services/productService";

export default function Produtos() {
  const navigate = useNavigate();
  const [editableStock, setEditableStock] = useState(null);
  // const [showDeleteModal, setShowDeleteModal] = useState(false);
  // const [productToDelete, setProductToDelete] = useState(null);

  const { data: products, loading, error } = useApi("Product");
  
  const handleStockEdit = (productId, newStock) => {
    const stockNum = parseInt(newStock, 10);
    if (isNaN(stockNum) || stockNum < 0) {
      toast.error("Estoque inválido! Use um número positivo.");
      setEditableStock(false);
      return;
    }
    try {
      updateProductStock(productId, stockNum);
      const productIndex = products.findIndex((p) => p.id === productId);
      products[productIndex].stock = stockNum;

      setEditableStock(false);
      toast.success("Estoque atualizado com sucesso!");
    } catch (error) {
      toast.error("Erro ao atualizar estoque: " + error.message);
    }
  };

  const handleEdit = (productId) => {
    navigate(`/produtos/edit/${productId}`);
  };

  const handleDeleteConfirm = async (productId) => {
    try {
      await deleteProduct(productId);
      toast.success(`Produto ${products.title} excluído com sucesso!`);
      await getProducts();
      // setShowDeleteModal(false);
    } catch (error) {
      toast.error("Erro ao excluir produto: " + error.message);
      // setShowDeleteModal(false);
    }
  };

  // const handleDelete = (product) => {
  //   setProductToDelete(product);
  //   setShowDeleteModal(true);
  // };

  if (loading) return <ProductTableSkeleton />;
  if (error) return (
    <div style={{ marginTop: "60px", padding: "20px" }}>
      <p className={styles.error}>{error}</p>
    </div>
  );

  return (
    <div>
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
              {products.map((product) => (
                <tr key={product.id}>
                  <td>{product.name}</td>
                  <td>{product.categoryName || "Sem categoria"}</td>
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
                          product.status == "Available" ? "#4caf50" :
                            product.status == "LowStock" ? "#ffca28" :
                              "#f44336",
                      }}
                    ></span>
                  </td>
                  <td>
                    <span className={styles.actionIcon} onClick={() => handleEdit(product.id)}><Edit size={16} /></span>
                    <span className={styles.actionIcon} onClick={() => handleDeleteConfirm(product.id)}><Trash2 size={16} /></span>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      </div>
    </div>
  );
}