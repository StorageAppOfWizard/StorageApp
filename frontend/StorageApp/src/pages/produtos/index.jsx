import React, { useState, useCallback, useEffect } from "react";
import { useNavigate, Link } from "react-router-dom";
import { useApi } from "../../hooks/useApi";
import ProductTableSkeleton from "../../components/ProductTableSkeleton";
import ProductRow from "../../components/ProductRow";
import styles from "../../styles/produtos.module.css";
import { Plus } from "lucide-react";
import { toast } from "react-toastify";
import { deleteProduct, updateProductStock } from "../../services/productService";

export default function Produtos() {
  const navigate = useNavigate();
  const [editableStock, setEditableStock] = useState(null);
  const [inputSearch, setInputSearch] = useState("");
  const [localProducts, setLocalProducts] = useState([]);

  const { data: categories } = useApi("Category");
  const { data: products, loading, error } = useApi("Product");

  useEffect(() => {
    if (Array.isArray(products)) {
      setLocalProducts(products);
    }
  }, [products]);

  const getStatusFromStock = (stock) => {
    if (stock === 0) return "OutOfStock";
    if (stock < 5) return "LowStock";
    return "Available";
  };

// const handleStockEdit = useCallback(
//   async (productId, newStock) => {
//     const stockNum = Number(newStock);
//     if (isNaN(stockNum) || stockNum < 0) {
//       toast.error("Estoque inválido! Use um número positivo.");
//       setEditableStock(false);
//       return;
//     }

//     try {
//       await updateProductStock(productId, stockNum);
//       setLocalProducts((prev) =>
//         prev.map((p) =>
//           p.id === productId
//             ? { ...p, stock: stockNum, status: getStatusFromStock(stockNum) } 
//             : p
//         )
//       );
//       setEditableStock(false);
//       toast.success("Estoque atualizado com sucesso!");
//     } catch (error) {
//       toast.error("Erro ao atualizar estoque: " + (error?.message ?? error));
//     }
//   },
//   []
// );

  const handleEdit = useCallback(
    (productId) => {
      navigate(`/produtos/edit/${productId}`);
    },
    [navigate]
  );

  const handleDeleteConfirm = useCallback(async (productId) => {
    try {
      await deleteProduct(productId);
      setLocalProducts((prev) => prev.filter((p) => p.id !== productId));
      toast.success(`Produto excluído com sucesso!`);
    } catch (error) {
      toast.error("Erro ao excluir produto: " + (error?.message ?? error));
    }
  }, []);

  const filteredProducts = localProducts.filter((p) => {
    const s = inputSearch.trim().toLowerCase();
    if (!s) return true;

    return [
      p.name,
      p.categoryName,
      p.brand,
    ]
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
    <div>
      <div style={{ marginTop: "60px", padding: "20px" }}>
        <div className={styles.container}>
          <div className={styles.header}>
            <h2>
              <span className={styles.itemCount}>
                {filteredProducts.length} itens cadastrados
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
              {filteredProducts.map((product) => (
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
