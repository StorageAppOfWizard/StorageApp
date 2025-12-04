import { useState } from "react";
import Tabs from "../../components/Tabs";
import { useFetchApi } from "../../hooks/useFetchApi";
import { useMutateApi } from "../../hooks/useMutateApi";

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

      <h2>Criar Categoria</h2>

      <form onSubmit={handleSubmit} className="form">
        <label> Nome da Categoria: </label>
        <input
          type="text"
          value={nomeCategoria}
          onChange={(e) => setnomeCategoria(e.target.value)}
          required
        />
        <textarea
          type="text"
          value={DescriptCategoria}
          onChange={(e) => setdescriptCategoria(e.target.value)}
        >
        </textarea>


        <button className="btn" disabled={creating}>
          {creating ? "Salvando..." : "Criar Categoria"}
        </button>
      </form>


      <h3>Categorias cadastradas</h3>

      {loading && <p>Carregando...</p>}
      {error && <p style={{ color: "red" }}>{error}</p>}

      {!loading && (
        <table className="table-default">
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
      )}
    </div>
  );
}
