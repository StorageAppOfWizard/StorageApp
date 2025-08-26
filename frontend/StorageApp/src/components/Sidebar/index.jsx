import { useState } from "react";
import { NavLink } from "react-router-dom";
import { ShoppingCart, Package, Users, DollarSign, Menu, Settings } from 'lucide-react';
import "./sidebar.css";
import logo from '../../assets/Logo-teste.png';

export default function Sidebar() {
    const [isOpen, setIsOpen] = useState(false);

    const menuItems = [
        { name: "Pedidos", icon: ShoppingCart, path: "/pedidos" },
        { name: "Produtos", icon: Package, path: "/" },
        { name: "Clientes", icon: Users, path: "/clientes" },
        { name: "Históricos", icon: DollarSign, path: "/historicos" },
        { name: "Usuários", icon: Users, path: "/usuarios" },
        { name: "Configurações", icon: Settings, path: "/configuracoes" }
    ];

    return (
        <>
            <button className="menu-toggle" onClick={() => setIsOpen(!isOpen)}>
                <Menu size={24} />
            </button>

            <nav className={`sidebar ${isOpen ? "open" : ""}`}>
                <div className="logo">
                    <img src={logo} alt="Logo da Marca" />
                </div>

                <div className="menu">
                    {menuItems.map((item) => {
                        const Icon = item.icon;
                        return (
                            <NavLink
                                key={item.name}
                                to={item.path}
                                className={({ isActive }) =>
                                    `menu-item ${isActive ? "active" : ""}`
                                }
                                onClick={() => setIsOpen(false)}
                            >
                                <Icon className="icon" />
                                <span>{item.name}</span>
                            </NavLink>
                        );
                    })}
                </div>
            </nav>

            {isOpen && <div className="overlay" onClick={() => setIsOpen(false)}></div>}
        </>
    );
}
