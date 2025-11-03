import { useState } from 'react';
import { useLocation, useNavigate } from 'react-router-dom';
import styles from '../../styles/header.module.css';
import { ChevronDown } from 'lucide-react';
import { headerData } from '../../data/menuItems';
import { useMutateApi } from '../../hooks/useMutateApi';
import { authEndpoint } from '../../endpoints/auth';

export default function Header({ profileName = "", overrideTitle, overrideIcon }) {

  const { mutate } = useMutateApi(authEndpoint.UserLogout)

  const location = useLocation();
  const navigate = useNavigate();
  const [isDropdownOpen, setIsDropdownOpen] = useState(false);
  const { title, icon: Icon } = overrideTitle || overrideIcon
    ? { title: overrideTitle, icon: overrideIcon }
    : (headerData[location.pathname] || { title: "Página Não Encontrada", icon: null });

  const handleDropdownToggle = () => {
    setIsDropdownOpen((prev) => !prev);
  };

  const handleOptionClick = (path) => {
    navigate(path);
    setIsDropdownOpen(false);
  };

  const handleLogout = () => {
    mutate(null, {
      onSuccess: () => navigate("/"),
    });
  };

  return (
    <header className={styles.header}>
      <div className={styles.titleContainer}>
        {Icon && <Icon />}
        <span className={styles.title}>{title}</span>
      </div>
      <div className={styles.profile} onClick={handleDropdownToggle}>
        <span className={styles.profileName}>{profileName}</span>
        <ChevronDown className={styles.profileIcon} />
        {isDropdownOpen && (
          <div className={styles.dropdown}>
            <div onClick={() => handleOptionClick('/configuracoes')} className={styles.dropdownItem}>
              Configurações
            </div>
            <div onClick={() => handleOptionClick('/historicos')} className={styles.dropdownItem}>
              Histórico
            </div>
            <div onClick={handleLogout} className={styles.dropdownItem}>
              Sair
            </div>
          </div>
        )}
      </div>
    </header>
  );
}