import { useState } from "react";
import Tabs from "../../components/ui/Tabs";
import { useMutateApi } from "../../hooks/useMutateApi";
import { useFetchApi } from "../../hooks/useFetchApi";
import styles from "../../styles/pages/criacao.module.css";
import { useToast } from "../../hooks/useToast";

export default function CriarProduto() {
  const { data: marcas } = useFetchApi("Brand.BrandsGet");
  const { data: categorias } = useFetchApi("Category.CategorysGet");

  const toast = useToast();
  const { mutate, loading: creating } = useMutateApi("Product.ProductCreate");

  const [form, setForm] = useState({
    name: "",
    quantity: 0,
    description: "",
    brandId: "",
    categoryId: "",
  });

  function handleChange(e) {
    setForm({ ...form, [e.target.name]: e.target.value });
  }

  async function handleSubmit(e) {
    e.preventDefault();

    await mutate(
      {
        name: form.name,
        description: form.description,
        quantity: Number(form.quantity),
        brandId: form.brandId,
        categoryId: form.categoryId,
      },
      {
        onSuccess: () => {
          setForm({
            name: "",
            quantity: 0,
            description: "",
            brandId: "",
            categoryId: "",
          });
          toast.success("Produto criado com sucesso!");
        },
        onError: (err) => {
          toast.error(`Erro ao criar produto: ${err.response.data.errors}`);
        }
      }
    );
  }

  return (
    <div style={{ marginTop: "60px", padding: "20px" }}>
      <Tabs
        tabs={[
          { value: "product", label: "Criar Produto", to: "/criar/produto" },
          { value: "brand", label: "Criar Marca", to: "/criar/marca" },
          { value: "category", label: "Criar Categoria", to: "/criar/categoria" },
        ]}
        currentValue="product"
      />

      <div className={styles.container}>
        <div className={styles.criar}>
          <h2 className={styles.title}>Criar Produto</h2>


          <form onSubmit={handleSubmit} className={styles.formGrid}>

            <div className={styles.left}>
              <label className={styles.label}>Nome do Produto</label>
              <input
                className={styles.input}
                type="text"
                name="name"
                value={form.name}
                onChange={handleChange}
                required
              />

              <label className={styles.label}>Quantidade</label>
              <input
                className={styles.input}
                type="number"
                name="quantity"
                value={form.quantity}
                onChange={handleChange}
                required
              />

              <label className={styles.label}>Marca</label>
              <select
                className={styles.input}
                name="brandId"
                value={form.brandId}
                onChange={handleChange}
                required
              >
                <option value="">Selecione uma marca</option>
                {marcas?.map((m) => (
                  <option key={m.id} value={m.id}>{m.name}</option>
                ))}
              </select>
            </div>

            <div className={styles.right}>
              <label className={styles.label}>Categoria</label>
              <select
                className={styles.input}
                name="categoryId"
                value={form.categoryId}
                onChange={handleChange}
                required
              >
                <option value="">Selecione uma categoria</option>
                {categorias?.map((c) => (
                  <option key={c.id} value={c.id}>{c.name}</option>
                ))}
              </select>

              <label className={styles.label}>Descrição</label>
              <textarea
                className={styles.textarea}
                name="description"
                value={form.description}
                onChange={handleChange}
              />

              <button className={styles.btn} type="submit" disabled={creating}>
                {creating ? "Criando..." : "Criar Produto"}
              </button>
            </div>

          </form>

        </div>
      </div>
    </div>
  );
}
