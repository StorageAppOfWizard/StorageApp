import { useState } from "react";
import Tabs from "../../components/Tabs";
import { useFetchApi } from "../../hooks/useFetchApi";
import { useMutateApi } from "../../hooks/useMutateApi";


export default function CriarBrand() {
   const [nomeMarca, setNomeMarca] = useState("");

  // GET: lista todas as marcas
  const { data: marcas, loading, error } = useFetchApi("Brand.GetAll");

  // POST: cria nova marca
  const { mutate, loading: creating } = useMutateApi("Brand.Create");

  const handleSubmit = async (e) => {
    e.preventDefault();

    await mutate(
      { nome: nomeMarca },
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

      <h2>Criar Marca</h2>

      <form onSubmit={handleSubmit} className="form">
        <label>Nome da Marca</label>
        <input
          value={nomeMarca}
          onChange={(e) => setNomeMarca(e.target.value)}
          required
        />

        <button className="btn" disabled={creating}>
          {creating ? "Salvando..." : "Criar Marca"}
        </button>
      </form>

      <hr style={{ margin: "25px 0" }} />

      <h3>Marcas cadastradas</h3>

      {loading && <p>Carregando...</p>}
      {error && <p style={{ color: "red" }}>{error}</p>}

      {!loading && (
        <table className="table-default">
          <thead>
            <tr>
              <th>ID</th>
              <th>Nome</th>
            </tr>
          </thead>
          <tbody>
            {marcas?.map((marca) => (
              <tr key={marca.id}>
                <td>{marca.id}</td>
                <td>{marca.nome}</td>
              </tr>
            ))}
          </tbody>
        </table>
      )}
    </div>
  );
}