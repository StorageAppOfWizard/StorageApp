//Ajustar as entradas e saidas

import { Routes, Route } from "react-router-dom";
import Produtos from "../pages/produtos/index";
import CreateProduto from "../pages/criar";
import Layout from "../components/Layout";

export default function RoutesApp() {
  return (
    <Routes>
      <Route element={<Layout />}>
        <Route path="/" element={<Produtos />} />
        <Route path="/criar" element={<CreateProduto />} />
      </Route>
    </Routes>
  );
}