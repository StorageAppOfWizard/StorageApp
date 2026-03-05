import React, { useState, useEffect } from "react";
import styles from "../../../styles/pages/criacao.module.css";
import { useToast } from "../../../hooks/useToast";

export default function OrderForm({
  todosProdutos,
  produtoSelecionadoNav,
  profileName,
  createOrder,
  creating,
}) {
  const toast = useToast();

  const initialForm = {
    productId: "",
    quantity: 1,
    requester: profileName || "",
    orderDate: new Date().toISOString().split("T")[0],
    category: "",
    brand: "",
    productName: "",
    stock: 0,
  };

  const [selectedProductId, setSelectedProductId] = useState("");
  const [form, setForm] = useState(initialForm);

  useEffect(() => {
    const produto = produtoSelecionadoNav?.items?.[0];
    if (!produto) return;

    setSelectedProductId(produto.id);
    setForm((prev) => ({
      ...prev,
      productId: produto.id,
      productName: produto.name,
      category: produto.categoryName,
      brand: produto.brandName,
      stock: produto.quantity,
      requester: profileName,
      orderDate: new Date().toISOString().split("T")[0],
    }));
  }, [produtoSelecionadoNav, profileName]);

  useEffect(() => {
    if (!selectedProductId || !todosProdutos?.items) return;

    const produtoSelecionado = todosProdutos.items.find(
      (p) => p.id === selectedProductId,
    );
    if (!produtoSelecionado) return;

    setForm((prev) => ({
      ...prev,
      productId: produtoSelecionado.id,
      productName: produtoSelecionado.name,
      category: produtoSelecionado.categoryName,
      brand: produtoSelecionado.brandName,
      stock: produtoSelecionado.quantity,
      requester: profileName,
      orderDate: new Date().toISOString().split("T")[0],
    }));
  }, [selectedProductId, todosProdutos, profileName]);

  function handleProductSelect(e) {
    setSelectedProductId(e.target.value);
  }

  function handleChange(e) {
    const { name, value } = e.target;
    setForm((prev) => ({ ...prev, [name]: value }));
  }

  async function handleSubmit(e) {
    e.preventDefault();
    try {
      await createOrder({
        productId: form.productId,
        quantity: Number(form.quantity),
      });

      toast.success("Pedido criado com sucesso!");

      setSelectedProductId("");
      setForm({
        ...initialForm,
        requester: profileName,
        orderDate: new Date().toISOString().split("T")[0],
      });
    } catch (err) {
      toast.error("Erro ao criar pedido.");
    }
  }

  return (
    <form onSubmit={handleSubmit} className={styles.formGrid}>
      <div className={styles.left}>
        <label className={styles.label}>Produto</label>
        <select
          className={styles.input}
          value={selectedProductId}
          onChange={handleProductSelect}
          required
        >
          <option value="">Selecione um produto</option>
          {todosProdutos?.items?.map((produto) => (
            <option key={produto.id} value={produto.id}>
              {produto.name}
            </option>
          ))}
        </select>

        <label className={styles.label}>Solicitante</label>
        <input
          className={`${styles.input} ${styles.readOnly}`}
          type="text"
          value={form.requester}
          readOnly
          disabled
        />

        <label className={styles.label}>Quantidade</label>
        <input
          className={styles.input}
          type="number"
          name="quantity"
          value={form.quantity}
          min={1}
          max={form.stock || 999}
          onChange={handleChange}
          required
        />

        <label className={styles.label}>Data do Pedido</label>
        <input
          className={`${styles.input} ${styles.readOnly}`}
          type="date"
          value={form.orderDate}
          readOnly
          disabled
        />
      </div>

      <div className={styles.right}>

        <label className={styles.label}>Categoria</label>
        <input
          className={`${styles.input} ${styles.readOnly}`}
          value={form.category}
          readOnly
          disabled
        />

        <label className={styles.label}>Marca</label>
        <input className={`${styles.input} ${styles.readOnly}`} value={form.brand} readOnly disabled />

        <label className={styles.label}>Estoque</label>
        <input className={`${styles.input} ${styles.readOnly}`} value={form.stock} readOnly disabled />

        <button
          className={styles.btn}
          disabled={creating || !selectedProductId}
        >
          {creating ? "Salvando..." : "Criar Pedido"}
        </button>
      </div>
    </form>
  );
}
