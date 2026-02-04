import { useState } from "react";
import Tabs from "../../components/Tabs";
import { useFetchApi } from "../../hooks/useFetchApi";
import { useMutateApi } from "../../hooks/useMutateApi";
import styles from "../../styles/pages/criacao.module.css";

export default function CriarCategory() {
  const [nomeCategoria, setnomeCategoria] = useState("");
  const [DescriptCategoria, setdescriptCategoria] = useState("");

  const { data: categorias, loading, error } = useFetchApi("Category.CategorysGet");
  const { mutate, loading: creating } = useMutateApi("Category.CategoryCreate")


  const handleSubmit = async (e) => {
    e.preventDefault();

    await mutate(
      {
        name: nomeCategoria,
        description: DescriptCategoria
      },
      {
        onSuccess: () => {
          setnomeCategoria("");
          setdescriptCategoria("");
          window.location.reload();
        }
      }
    );

  };

  return (
    <div style={{ marginTop: "60px", padding: "20px" }}>
      <Tabs
        tabs={[
          { value: "product", label: "Criar Produto", to: "/criar/produto" },
          { value: "brand", label: "Criar Marca", to: "/criar/marca" },
          { value: "category", label: "Criar Categoria", to: "/criar/categoria" },
        ]}
        currentValue="category"
      />

      <div className={styles.container}>
        <div className={styles.criar}>
          <h2 className={styles.title}>Criar Categoria</h2>

          <form onSubmit={handleSubmit} className={styles.form}>
            <label className={styles.label}> Nome da Categoria</label>
            <input
              type="text"
              className={styles.input}
              value={nomeCategoria}
              onChange={(e) => setnomeCategoria(e.target.value)}
              required
            />
            
            <label className={styles.label}>Descrição</label>
            <textarea
              type="text"
              className={styles.textarea}
              value={DescriptCategoria}
              onChange={(e) => setdescriptCategoria(e.target.value)}
            >
            </textarea>


            <button className={styles.btn} disabled={creating}>
              {creating ? "Salvando..." : "Criar Categoria"}
            </button>
          </form>

        </div>

        <div className={styles.cadastrado}>
          <h3 className={styles.subtitle}>Categorias cadastradas</h3>

          {loading && <p>Carregando...</p>}
          {error && <p style={{ color: "red" }}>{error}</p>}

          {!loading && (
            <div className={styles.tableContainer}>
              <table className={styles.table}>
                <thead>
                  <tr>
                    <th>Nome</th>
                  </tr>
                </thead>
                <tbody>
                  {categorias?.map((categorias) => (
                    <tr key={categorias.id}>
                      <td>{categorias.name}</td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>
          )}
        </div>
      </div>

    </div>
  );
}
