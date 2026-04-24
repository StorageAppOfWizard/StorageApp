import { Pencil, Trash2 } from "lucide-react";
import styles from "../../../styles/features/UserCard.module.css";

const AVATAR_COLORS = [
  "#4f6bed", "#e05252", "#e09532", "#2eb57a",
  "#9b59b6", "#1abc9c", "#e67e22", "#2980b9",
];
 
function getAvatarColor(name = "") {
  const code = name.charCodeAt(0) || 0;
  return AVATAR_COLORS[code % AVATAR_COLORS.length];
}
 
function getInitials(name = "") {
  return name
    .split(" ")
    .filter(Boolean)
    .slice(0, 2)
    .map((n) => n[0].toUpperCase())
    .join("");
}
 
const ROLE_CLASSES = {
  Admin: styles.roleAdmin,
  Manager: styles.roleManager,
  Member: styles.roleMember,
};
 
export default function UserCard({ user, onEdit, onDelete }) {
  // Normaliza role: API manda ["Admin"] mas preparado para string também
  const role = Array.isArray(user.role) ? user.role[0] : user.role;
 
  const initials = getInitials(user.userName);
  const avatarBg = getAvatarColor(user.userName);
  const roleClass = ROLE_CLASSES[role] || styles.roleMember;
 
  // isActive: undefined enquanto a API não mandar.
  // hasStatus controla se a seção aparece ou não — zero risco de quebrar.
  const hasStatus = user.isActive !== undefined;
 
  return (
    <div className={styles.card}>
 
      {/* ── Avatar + nome + email ── */}
      <div className={styles.userInfo}>
        <div
          className={styles.avatar}
          style={{ backgroundColor: avatarBg }}
          aria-label={`Avatar de ${user.userName}`}
        >
          {initials}
        </div>
 
        <div className={styles.nameBlock}>
          <span className={styles.name}>{user.userName}</span>
          <span className={styles.email} title={user.email}>
            {user.email}
          </span>
          {role && (
            <span className={`${styles.roleTag} ${roleClass}`}>{role}</span>
          )}
        </div>
      </div>
 
      <hr className={styles.divider} />
 
      {/* ── Status (oculto até a API mandar isActive) ── */}
      {hasStatus && (
        <div className={styles.meta}>
          <div className={styles.metaRow}>
            <span className={styles.metaLabel}>Status</span>
            <span
              className={`${styles.statusBadge} ${
                user.isActive ? styles.statusActive : styles.statusInactive
              }`}
            >
              {user.isActive ? "Ativo" : "Inativo"}
            </span>
          </div>
        </div>
      )}
 
      {/* ── Ações ── */}
      <div className={styles.actions}>
        <button
          className={styles.editBtn}
          onClick={() => onEdit(user.id)}
          aria-label={`Editar ${user.userName}`}
        >
          <Pencil size={14} />
          Editar
        </button>
        <button
          className={styles.deleteBtn}
          onClick={() => onDelete(user.id)}
          aria-label={`Deletar ${user.userName}`}
        >
          <Trash2 size={15} />
        </button>
      </div>
    </div>
  );
}
