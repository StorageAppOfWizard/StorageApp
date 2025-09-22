import { useState, useCallback } from "react";
import { useNavigate, Link } from "react-router-dom";
import { useApi } from "../../hooks/useApi";
import ProductTableSkeleton from "../../components/ProductTableSkeleton";
import ProductRow from "../../components/ProductRow";
import styles from "../../styles/produtos.module.css";
import { Plus } from "lucide-react";
import { toast } from "react-toastify";
import { deleteProduct, getProducts, updateProductStock } from "../../services/productService";

export default function Produtos() {
  const navigate = useNavigate();
  const [editableStock, setEditableStock] = useState(null);
  const { data: categories } = useApi("Category");

  const { data: products, loading, error } = useApi("Product");

  const handleStockEdit = useCallback(async (productId, newStock) => {
    const stockNum = parseInt(newStock, 10);
    if (isNaN(stockNum) || stockNum < 0) {
      toast.error("Estoque inválido! Use um número positivo.");
      setEditableStock(false);
      return;
    }
    try {
      await updateProductStock(productId, stockNum);
      const productIndex = products.findIndex((p) => p.id === productId);
      products[productIndex].stock = stockNum;

      setEditableStock(false);
      toast.success("Estoque atualizado com sucesso!");
    } catch (error) {
      toast.error("Erro ao atualizar estoque: " + error.message);
    }
  }, [products]);

  const handleEdit = useCallback((productId) => {
    navigate(`/produtos/edit/${productId}`);
  }, [navigate]);

  const handleDeleteConfirm = useCallback(async (productId) => {
    try {
      await deleteProduct(productId);
      toast.success(`Produto excluído com sucesso!`);
      await getProducts();
    } catch (error) {
      toast.error("Erro ao excluir produto: " + error.message);
    }
  }, []);

  if (loading) return <ProductTableSkeleton />;
  if (error) {
    return (
      <div style={{ marginTop: "60px", padding: "20px" }}>
        <p className={styles.error}>{error}</p>
      </div>
    );
  }

  return (
    <div>
      <div style={{ marginTop: "60px", padding: "20px" }}>
        <div className={styles.container}>
          <div className={styles.header}>
            <h2>
              <span className={styles.itemCount}>
                {products.length} itens cadastrados
              </span>
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
                <ProductRow
                  key={product.id}
                  product={product}
                  editableStock={editableStock}
                  setEditableStock={setEditableStock}
                  onStockEdit={handleStockEdit}
                  onEdit={handleEdit}
                  onDelete={handleDeleteConfirm}
                />
              ))}
            </tbody>
          </table>
        </div>
      </div>
    </div>
  );
}
