import React, { useState, useEffect } from "react";
import styles from "../../styles/components/EditPopup.module.css";
import { Copy } from "lucide-react";

export default function EditProductModal({
    isOpen,
    onClose,
    product,
    marcas,
    categorias,
    onSubmitEdit,
}) {
    if (!isOpen || !product) return null;

    const [form, setForm] = useState({
        name: "",
        quantity: "",
        brandId: "",
        categoryId: "",
        description: "",
    });

    useEffect(() => {
        if (product) {
            setForm({
                name: product.name || "",
                quantity: product.quantity || "",
                brandId: product.brandId || "",
                categoryId: product.categoryId || "",
                description: product.description || "",
            });
        }
    }, [product]);

    const handleChange = (e) => {
        const { name, value } = e.target;
        setForm((prev) => ({ ...prev, [name]: value }));
    };

    const handleSubmit = () => {
        if (!onSubmitEdit) return;
        onSubmitEdit(form, product.id);
    };

    return (
        <div className={styles.modalOverlay}>
            <div className={styles.modalContent}>

                <h3>Editar Produto</h3>
                <span className={styles.idWrapper}>
                    ID: {product.id}
                    <Copy
                        size={12}
                        className={styles.copyIcon}
                        onClick={() => navigator.clipboard.writeText(product.id)}
                    />
                </span>

                <div className={styles.modalBody}>

                    <label className={styles.modalLabel}>Nome do Produto</label>
                    <input
                        type="text"
                        className={styles.modalInput}
                        name="name"
                        value={form.name}
                        onChange={handleChange}
                        required
                    />

                    <label className={styles.modalLabel}>Quantidade</label>
                    <input
                        type="number"
                        className={styles.modalInput}
                        name="quantity"
                        value={form.quantity}
                        onChange={handleChange}
                        required
                    />

                    <label className={styles.modalLabel}>Marca</label>
                    <select
                        className={styles.modalInput}
                        name="brandId"
                        value={form.brandId}
                        onChange={handleChange}
                        required
                    >
                        <option value="">Selecione uma marca</option>
                        {marcas?.map((m) => (
                            <option key={m.id} value={m.id}>
                                {m.name}
                            </option>
                        ))}
                    </select>

                    <label className={styles.modalLabel}>Categoria</label>
                    <select
                        className={styles.modalInput}
                        name="categoryId"
                        value={form.categoryId}
                        onChange={handleChange}
                        required
                    >
                        <option value="">Selecione uma categoria</option>
                        {categorias?.map((c) => (
                            <option key={c.id} value={c.id}>
                                {c.name}
                            </option>
                        ))}
                    </select>

                    <label className={styles.modalLabel}>Descrição do Produto</label>
                    <textarea
                        className={styles.modalTextarea}
                        name="description"
                        value={form.description}
                        onChange={handleChange}
                    />
                </div>

                <div className={styles.modalFooter}>
                    <button className={styles.btnEdit} onClick={handleSubmit}>
                        Salvar Alterações
                    </button>

                    <button className={styles.btnClose} onClick={onClose}>
                        Fechar
                    </button>
                </div>
            </div>
        </div>
    );
}
