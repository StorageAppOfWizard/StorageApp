//Ajustar as entradas e saidas
import { ToastProvider } from "../contexts/ToastContext";
import { Routes, Route } from "react-router-dom";
import Produtos from "../pages/produtos/index";
import CreateProduto from "../pages/criar";
import Layout from "../components/Layout";
import SingnIn from "../pages/singnIn";
import SingnUp from "../pages/singnUp";

export default function RoutesApp() {
  return (
    <ToastProvider>
      <Routes>
        <Route path="/" element={<SingnIn />} />
        <Route path="/cadastrar" element={<SingnUp />} />
        <Route element={<Layout />}>


          <Route path="/produtos" element={<Produtos />} />
          <Route path="/criar" element={<CreateProduto />} />
        </Route>
      </Routes>
    </ToastProvider>
  );
}