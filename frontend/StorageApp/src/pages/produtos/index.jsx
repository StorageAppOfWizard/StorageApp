import React, { useState, useCallback, useEffect } from "react";
import { useNavigate, Link } from "react-router-dom";
import { useFetchApi } from "../../hooks/useFetchApi";
import { useMutateApi } from "../../hooks/useMutateApi";

import EditProductModal from "../../components/EditProductModal";
import ProductTableSkeleton from "../../components/ProductTableSkeleton";
import ProductRow from "../../components/ProductRow";
import styles from "../../styles/pages/produtos.module.css";

import { Plus } from "lucide-react";
import { toast } from "react-toastify";

export default function Produtos() {
  const navigate = useNavigate();

  const [editableStock, setEditableStock] = useState(null);
  const [inputSearch, setInputSearch] = useState("");
  const [localProducts, setLocalProducts] = useState([]);
  const [isEditModalOpen, setIsEditModalOpen] = useState(false);
  const [productToEdit, setProductToEdit] = useState(null);


  const {
    data: products,
    loading,
    error,
  } = useFetchApi("Product.ProductsGet");
  const { data: categorias } = useFetchApi("Category.CategorysGet");
  const { data: marcas } = useFetchApi("Brand.BrandsGet");


  const { mutate: mutateStock } = useMutateApi("Product.ProductUpdateStock");
  const { mutate: mutateDelete } = useMutateApi("Product.ProductDelete");
  const { mutate: mutateUpdate } = useMutateApi("Product.ProductUpdate");



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

  const handleStockEdit = async (productId, newStock) => {
    const stockNum = Number(newStock);

    if (isNaN(stockNum) || stockNum < 0) {
      toast.error("Estoque inválido! Use um número positivo.");
      setEditableStock(false);
      return;
    }

    try {
      await mutateStock({
        id: productId,
        newStock: stockNum,
      });

      setLocalProducts((prev) =>
        prev.map((p) =>
          p.id === productId
            ? { ...p, quantity: stockNum, status: getStatusFromStock(stockNum) }
            : p
        )
      );

      setEditableStock(false);
      toast.success("Estoque atualizado com sucesso!");
    } catch (error) {
      toast.error("Erro ao atualizar estoque: " + (error?.message ?? error));
    }
  };

  const handleEdit = useCallback((productId) => {
    const prod = localProducts.find((p) => p.id === productId);
    setProductToEdit(prod);
    setIsEditModalOpen(true);
  }, [localProducts]);

  const handleSubmitEdit = async (formData, id) => {
    try {
      await mutateUpdate({
        id,
        ...formData,
      });

      toast.success("Produto atualizado!");

      setLocalProducts((prev) =>
        prev.map((p) => (p.id === id ? { ...p, ...formData } : p))
      );

      setIsEditModalOpen(false);
      window.location.reload();
    } catch (error) {
      toast.error("Erro ao atualizar: " + (error?.message ?? error));
    }
  };


  const handleDeleteConfirm = async (productId) => {
    try {
      await mutateDelete({ id: productId });

      setLocalProducts((prev) => prev.filter((p) => p.id !== productId));

      toast.success(`Produto excluído com sucesso!`);
    } catch (error) {
      toast.error("Erro ao excluir produto: " + (error?.message ?? error));
    }
  };

  const filteredProducts = localProducts.filter((p) => {
    const s = inputSearch.trim().toLowerCase();
    if (!s) return true;

    return [p.name, p.categoryName, p.brand]
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

            <Link to="/criar/produto">
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
            {filteredProducts.length === 0 ? (
              <tr>
                <td colSpan="6">Nenhum produto cadastrado</td>
              </tr>
            ) : (
              filteredProducts.map((product) => (
                <ProductRow
                  key={product.id}
                  product={product}
                  editableStock={editableStock}
                  setEditableStock={setEditableStock}
                  onStockEdit={handleStockEdit}
                  onEdit={handleEdit}
                  onDelete={handleDeleteConfirm}
                />
              ))
            )}
          </tbody>
        </table>
      </div>

      <EditProductModal
        isOpen={isEditModalOpen}
        onClose={() => setIsEditModalOpen(false)}
        product={productToEdit}
        marcas={marcas}
        categorias={categorias}
        onSubmitEdit={handleSubmitEdit}
      />

    </div>
  );
}
