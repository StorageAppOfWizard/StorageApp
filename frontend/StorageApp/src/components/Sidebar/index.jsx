import { useState } from "react";
import { NavLink } from "react-router-dom";
import { ShoppingCart, Package, Users, DollarSign, Menu, Settings } from 'lucide-react'
import "./sidebar.css"

export default function Sidebar() {
    const [isOpen, setIsOpen] = useState(false);

    const menuItems = [
        { name: "Pedidos", icon: ShoppingCart, path: "" },
        { name: "Produtos", icon: Package, path: "" },
        { name: "Clientes", icon: Users, path: "" },
        { name: "Históricos", icon: DollarSign, path: "" },
        { name: "Usuários", icon: Users, path: "" },
        { name: "Configurações", icon: Settings, path: "" }
    ];

    return (
        <>
            {/* Botão do menu no topo (mobile) */}
            <button className="menu-toggle" onClick={() => setIsOpen(!isOpen)}>
                <Menu size={24} />
            </button>

             {/* Sidebar */}
            <nav className={`sidebar ${isOpen ? "open" : ""}`}>
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
                                onClick={() => setIsOpen(false)} // Fecha o mobile
                            >
                                <Icon className="icon" />
                                <span>{item.name}</span>
                            </NavLink>
                        )
                    })

                    }
                </div>
            </nav>

             {/* Fundo escuro no mobile*/}  
            {isOpen && <div className="overlay" onClick={() => setIsOpen(false)}></div>}
        </>
    )
}