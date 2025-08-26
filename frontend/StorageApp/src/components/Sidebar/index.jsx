import { useState } from "react";
import { NavLink } from "react-router-dom";
import { Menu } from 'lucide-react';
import styles from '../../styles/sidebar.module.css';
import logo from '../../assets/Logo-teste.png';
import { menuItems } from '../../data/menuItems';

export default function Sidebar() {
    const [isOpen, setIsOpen] = useState(false);

    return (
        <>
            <button className={styles.sidebarToggle} onClick={() => setIsOpen(!isOpen)} aria-label="Abrir/Fechar menu lateral">
                <Menu size={24} />
            </button>

            <nav className={`${styles.sidebar} ${isOpen ? styles.open : ''}`}>
                <div className={styles.logo}>
                    <img src={logo} alt="Logo da Marca" />
                </div>

                <div className={styles.menu}>
                    {menuItems.map((item) => {
                        const Icon = item.icon;
                        return (
                            <NavLink
                                key={item.name}
                                to={item.path}
                                className={({ isActive }) =>
                                    `${styles.menuItem} ${isActive ? styles.active : ''}`
                                }
                                onClick={() => setIsOpen(false)}
                            >
                                <Icon className={styles.icon} />
                                <span>{item.name}</span>
                            </NavLink>


                        );
                    })}
                </div>
            </nav>

            {isOpen && <div className={styles.overlay} onClick={() => setIsOpen(false)}></div>}
        </>
    );
}