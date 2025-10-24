import { ShoppingCart, Package, Users, DollarSign, Settings, PackagePlus } from 'lucide-react';

export const menuItems = [
    { name: "Pedidos", icon: ShoppingCart, path: "/pedidos" },
    { name: "Produtos", icon: Package, path: "/produtos" },
    { name: "Clientes", icon: Users, path: "/clientes" },
    { name: "Históricos", icon: DollarSign, path: "/historicos" },
    { name: "Usuários", icon: Users, path: "/usuarios" },
    { name: "Configurações", icon: Settings, path: "/configuracoes" }
];

export const headerData = {
    "/produtos": { title: "Produtos", icon: Package },
    "/pedidos": { title: "Pedidos", icon: ShoppingCart },
    "/clientes": { title: "Clientes", icon: Users  },
    "/historicos": { title: "Históricos", icon: DollarSign },
    "/usuarios": { title: "Usuários", icon: Users },
    "/configuracoes": { title: "Configurações", icon: Settings },
    "/criar": { title: "Novos Produtos", icon: PackagePlus },
};