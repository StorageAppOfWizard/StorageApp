import { ToastProvider } from "../providers/ToastProvider.jsx";
import { Routes, Route } from "react-router-dom";

import PrivateRoute from "./private";

import Produtos from "../pages/produtos/index";
import Layout from "../components/Layout";
import SingnIn from "../pages/singnIn";
import SingnUp from "../pages/singnUp";
import Orders from "../pages/orders";
import Configuracoes from "../pages/configuracoes";

import Create from "../pages/criar";
import CriarProduto from "../pages/criar/criarProduto";
import CriarBrand from "../pages/criar/criarBrand"
import CriarCategory from "../pages/criar/criarCategory"
import CriarOrder from "../pages/criar/criarOrder";
import UltimosPedidos from "../pages/criar/ultimosPedidos.jsx";
import Users from "../pages/users";

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

            {/* Rotas de criação */}
            <Route path="/criar" element={<Create />} /> 
            <Route path="/criar/produto" element={<CriarProduto />} />
            <Route path="/criar/marca" element={<CriarBrand />} />
            <Route path="/criar/categoria" element={<CriarCategory />} />
            <Route path="/criar/pedido" element={<CriarOrder />} />
            <Route path="/criar/ultimos-pedidos" element={<UltimosPedidos />} />
            <Route path="/usuarios" element={<Users />} />



            <Route path="/pedidos" element={<Orders />} />
            <Route path="/configuracoes" element={<Configuracoes />} />
          </Route>
        </Route>
      </Routes>
    </ToastProvider>
  );
}