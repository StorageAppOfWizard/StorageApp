import { ToastProvider } from "../contexts/ToastContext";
import { Routes, Route } from "react-router-dom";

import PrivateRoute from "./private";

import Produtos from "../pages/produtos/index";
import CreateProduto from "../pages/criar";
import Layout from "../components/Layout";
import SingnIn from "../pages/singnIn";
import SingnUp from "../pages/singnUp";
import Orders from "../pages/orders"; 

export default function RoutesApp() {



  return (
    <ToastProvider>
      <Routes>
        {/* Rotas públicas */}
        <Route path="/" element={<SingnIn />} />
        <Route path="/cadastrar" element={<SingnUp />} />

        {/* Rotas privadas */}
        <Route element={<PrivateRoute />}>
          <Route element={<Layout />}>
            <Route path="/produtos" element={<Produtos />} />
            <Route path="/criar" element={<CreateProduto />} />
            <Route path="/pedidos" element={<Orders />} />
          </Route>
        </Route>
      </Routes>
    </ToastProvider>
  );
}