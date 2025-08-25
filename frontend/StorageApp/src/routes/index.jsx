import { Routes, Route } from "react-router-dom";
import Produtos from "../pages/produtos";
import Layout from "../components/Layout";

export default function RoutesApp() {
  return (
    <Routes>
      <Route element={<Layout />}>
        <Route path="/" element={<Produtos />} />
      </Route>
    </Routes>
  );
}
