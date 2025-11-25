import { useLocation, Link, useNavigate } from "react-router-dom";
import styles from "../../styles/header.module.css";
import { Settings } from "lucide-react";
import { headerData } from "../../data/menuItems";
import { useAuth } from "../../contexts/AuthContext";

export default function Header({ overrideTitle, overrideIcon }) {
  const { user, loading: userLoading } = useAuth();
  const location = useLocation();

  const { title, icon: Icon } =
    overrideTitle || overrideIcon
      ? { title: overrideTitle, icon: overrideIcon }
      : headerData[location.pathname] || {
        title: "Página Não Encontrada",
        icon: null,
      };

  const profileName = user?.unique_name || user?.name || "Carregando...";

  return (
    <header className={styles.header}>
      <div className={styles.titleContainer}>
        {Icon && <Icon className={styles.icon} />}
        <span className={styles.title}>{title}</span>
      </div>

      <div className={styles.actions}>
        <div className={styles.actionProfile}>
          <div className={styles.profile}>
            {userLoading ? (
              <span className={styles.profileName}>Carregando...</span>
            ) : (
              <span className={styles.profileName}>{profileName}</span>
            )}
          </div>

          <Link to="/configuracoes" className={styles.actionIcon}>
            <Settings />
          </Link>
        </div>
      </div>
    </header>
  );
}
