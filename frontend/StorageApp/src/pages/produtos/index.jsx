import React, { useState, useCallback, useEffect } from "react";
import { Link, Navigate, useNavigate } from "react-router-dom";
import { useFetchApi } from "../../hooks/useFetchApi";
import { useMutateApi } from "../../hooks/useMutateApi";

import EditProductModal from "../../components/EditProductModal";
import ProductTableSkeleton from "../../components/ProductTableSkeleton";
import ProductRow from "../../components/ProductRow";
import styles from "../../styles/pages/produtos.module.css";

import { Plus } from "lucide-react";
import { useToast } from "../../hooks/useToast";

export default function Produtos() {

  const [editableStock, setEditableStock] = useState(null);
  const [inputSearch, setInputSearch] = useState("");
  const [localProducts, setLocalProducts] = useState([]);
  const [isEditModalOpen, setIsEditModalOpen] = useState(false);
  const [productToEdit, setProductToEdit] = useState(null);


  const { data: products, loading, error, refetch: refetchProducts } = useFetchApi("Product.ProductsGet");
  const { data: categorias } = useFetchApi("Category.CategorysGet");
  const { data: marcas } = useFetchApi("Brand.BrandsGet");

  const { mutate: mutateStock } = useMutateApi("Product.ProductUpdateStock");
  const { mutate: mutateDelete } = useMutateApi("Product.ProductDelete");
  const { mutate: mutateUpdate } = useMutateApi("Product.ProductUpdate");

  const toast = useToast();
  const navigate =useNavigate();

  useEffect(() => {
    if (Array.isArray(products.items)) {
      setLocalProducts(products.items);
      
    }
  }, [products]);


  const handleStockEdit = async (productId, newStock) => {
    const stockNum = Number(newStock);

    if (stockNum < 0) {
      toast.error("Estoque inválido! Use um número positivo.");
      setEditableStock(false);
      return;
    }
    await mutateStock({
      id: productId,
      newStock: stockNum,
    }, {
      onSuccess: () => {
        setEditableStock(false);
        toast.success("Estoque atualizado com sucesso!");
        refetchProducts();
      },
      onError: (err) => {
        toast.error(`Erro ao atualizar estoque: ${err.response.data.errors}`);
      }
    });
  };

  const handleEdit = useCallback((productId) => {
    const prod = localProducts.find((p) => p.id === productId);
    setProductToEdit(prod);
    setIsEditModalOpen(true);
  }, [localProducts]);

  const handleSubmitEdit = async (formData, id) => {
    await mutateUpdate({
      id,
      ...formData,
    },
      {
        onSuccess: () => {
          toast.success("Produto atualizado com sucesso!");
          setIsEditModalOpen(false);
          refetchProducts();
        },
        onError: (err) => {
          toast.error(`Erro ao atualizar produto: ${err?.response.data.errors ?? err.response.data.errors}`);
        }
      });
  };


  const handleDeleteConfirm = async (productId) => {
    await mutateDelete({ id: productId }, {
      onSuccess: () => {
        toast.success(`Produto excluído com sucesso!`);
        refetchProducts();
      },
      onError: (err) => { toast.error(`Erro ao excluir produto: ${err?.response.data.errors ?? err.response.data.errors}`); }
    });
  };

  const handleOrder = (productId) => {
    navigate('/criar/pedido', {
      state:{
        productId: productId
      }
    });
  }

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
                  onOrder={handleOrder}
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
