import { useState } from "react";
import Tabs from "../../components/ui/Tabs";
import { useFetchApi } from "../../hooks/useFetchApi";
import { useMutateApi } from "../../hooks/useMutateApi";
import styles from "../../styles/pages/criacao.module.css";


export default function CriarBrand() {
  const [nomeMarca, setNomeMarca] = useState("");

  const { data: marcas, loading, error } = useFetchApi("Brand.BrandsGet");
  const { mutate, loading: creating } = useMutateApi("Brand.BrandsCreate");

  const handleSubmit = async (e) => {
    e.preventDefault();

    await mutate(
      { name: nomeMarca },
      {
        onSuccess: () => {
          setNomeMarca("");
          window.location.reload();
        },
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
        currentValue="brand"
      />

      <div className={styles.container}>

        <div className={styles.criar}>
          <h2 className={styles.title}>Criar Marca</h2>

          <form onSubmit={handleSubmit} className={styles.form}>
            <label className={styles.label}>Nome da Marca</label>

            <input
              className={styles.input}
              value={nomeMarca}
              onChange={(e) => setNomeMarca(e.target.value)}
              required
            />

            <button className={styles.btn} disabled={creating}>
              {creating ? "Salvando..." : "Criar Marca"}
            </button>
          </form>

        </div>
        <div className={styles.cadastrado}>
          <h3 className={styles.subtitle}>Marcas cadastradas</h3>

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
                  {marcas?.map((marca) => (
                    <tr key={marca.id} className={styles.row}>
                      <td>{marca.name}</td>
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