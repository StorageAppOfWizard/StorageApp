import { useState, useEffect } from "react";
import { X, User, Mail, Shield, Lock, ToggleLeft, ToggleRight } from "lucide-react";
import styles from "./EditUserModal.module.css";

const ROLES = [
  { value: "Admin",   label: "Admin",   accent: "#e05252" },
  { value: "Manager", label: "Manager", accent: "#e09532" },
  { value: "Member",  label: "Membro",  accent: "#2eb57a" },
];

// Cor de fundo do header por role — conecta com UserStatsCards
const ROLE_HEADER_GRADIENT = {
  Admin:   "linear-gradient(120deg, #e05252 0%, #e87272 100%)",
  Manager: "linear-gradient(120deg, #e09532 0%, #e8b652 100%)",
  Member:  "linear-gradient(120deg, #2eb57a 0%, #3ecf8e 100%)",
};

const PAGES = [
  { key: "criar_produto",   label: "Criar Produto",   path: "/criar/produto" },
  { key: "criar_marca",     label: "Criar Marca",     path: "/criar/marca" },
  { key: "criar_categoria", label: "Criar Categoria", path: "/criar/categoria" },
  { key: "criar_pedido",    label: "Criar Pedido",    path: "/criar/pedido" },
  { key: "ultimos_pedidos", label: "Últimos Pedidos", path: "/criar/ultimos-pedidos" },
  { key: "usuarios",        label: "Usuários",        path: "/usuarios" },
  { key: "pedidos",         label: "Pedidos",         path: "/pedidos" },
  { key: "configuracoes",   label: "Configurações",   path: "/configuracoes" },
];


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
    .join("") || "?";
}

/** Normaliza role: ["Admin"] ou "Admin" → "Admin" */
function getRoleValue(role) {
  return Array.isArray(role) ? role[0] : role;
}

export default function EditUserModal({ user, onClose, onSave }) {
  const [form, setForm] = useState({
    userName: "",
    email: "",
    role: "Member",
    isActive: undefined,   // undefined = API ainda não manda este campo
    permissions: {},
  });

  useEffect(() => {
    if (!user) return;
    setForm({
      userName:    user.userName || "",
      email:       user.email    || "",
      role:        getRoleValue(user.role) || "Member",
      // isActive só entra no form se a API mandar explicitamente
      isActive:    user.isActive !== undefined ? user.isActive : undefined,
      permissions: user.permissions || {},
    });
  }, [user]);

  if (!user) return null;


  const initials       = getInitials(form.userName);
  const avatarBg       = getAvatarColor(form.userName);
  const headerGradient = ROLE_HEADER_GRADIENT[form.role] || ROLE_HEADER_GRADIENT.Member;
  const hasStatus      = form.isActive !== undefined;

  const allEnabled  = PAGES.every((p) => form.permissions[p.key]);
  const noneEnabled = PAGES.every((p) => !form.permissions[p.key]);


  const handleChange = (field, value) =>
    setForm((prev) => ({ ...prev, [field]: value }));

  const togglePermission = (key) =>
    setForm((prev) => ({
      ...prev,
      permissions: { ...prev.permissions, [key]: !prev.permissions[key] },
    }));

  const handleAllPermissions = (val) => {
    const all = Object.fromEntries(PAGES.map((p) => [p.key, val]));
    setForm((prev) => ({ ...prev, permissions: all }));
  };

  const handleSubmit = () => {
    onSave?.({ ...user, ...form });
    onClose?.();
  };

  const handleOverlayClick = (e) => {
    if (e.target === e.currentTarget) onClose?.();
  };

  return (
    <div className={styles.overlay} onClick={handleOverlayClick}>
      <div className={styles.modal}>

        {/* ── Header com faixa colorida por role ── */}
        <div className={styles.header} style={{ "--header-gradient": headerGradient }}>
          <div className={styles.headerBand} />

          <div className={styles.headerContent}>
            <div className={styles.headerLeft}>
              {/* Avatar: mesma lógica do UserCard */}
              <div
                className={styles.avatar}
                style={{ backgroundColor: avatarBg }}
                aria-label={`Avatar de ${form.userName}`}
              >
                {initials}
              </div>
              <div>
                <h2 className={styles.title}>Editar Usuário</h2>
                <p className={styles.subtitle}>{user.email}</p>
              </div>
            </div>

            <button className={styles.closeBtn} onClick={onClose} aria-label="Fechar modal">
              <X size={16} />
            </button>
          </div>
        </div>

        {/* ── Body ── */}
        <div className={styles.body}>

          {/* Seção: Informações Básicas */}
          <div className={styles.section}>
            <h3 className={styles.sectionTitle}>
              <User size={13} /> Informações Básicas
            </h3>
            <div className={styles.row}>
              <div className={styles.field}>
                <label htmlFor="modal-username">Nome</label>
                <input
                  id="modal-username"
                  value={form.userName}
                  onChange={(e) => handleChange("userName", e.target.value)}
                  placeholder="Nome do usuário"
                />
              </div>
              <div className={styles.field}>
                <label htmlFor="modal-email">Email</label>
                <input
                  id="modal-email"
                  value={form.email}
                  onChange={(e) => handleChange("email", e.target.value)}
                  placeholder="email@exemplo.com"
                />
              </div>
            </div>
          </div>

          {/* Seção: Função & Status */}
          <div className={styles.section}>
            <h3 className={styles.sectionTitle}>
              <Shield size={13} /> Função & Status
            </h3>
            <div className={styles.row}>

              {/* Role — botões pill, cor muda junto com o header */}
              <div className={styles.field}>
                <label>Função</label>
                <div className={styles.roleGroup}>
                  {ROLES.map((r) => {
                    const isActive = form.role === r.value;
                    return (
                      <button
                        key={r.value}
                        className={`${styles.roleBtn} ${isActive ? styles.roleBtnActive : ""}`}
                        style={isActive ? { "--role-accent": r.accent } : {}}
                        onClick={() => handleChange("role", r.value)}
                      >
                        {r.label}
                      </button>
                    );
                  })}
                </div>
              </div>

              {/* Status — só aparece quando a API mandar isActive */}
              {hasStatus && (
                <div className={styles.field}>
                  <label>Status</label>
                  <div
                    className={styles.statusToggle}
                    onClick={() => handleChange("isActive", !form.isActive)}
                    role="switch"
                    aria-checked={form.isActive}
                    tabIndex={0}
                    onKeyDown={(e) => e.key === "Enter" && handleChange("isActive", !form.isActive)}
                  >
                    {form.isActive ? (
                      <>
                        <ToggleRight size={28} className={styles.toggleOn} />
                        <span className={styles.statusLabelOn}>Ativo</span>
                      </>
                    ) : (
                      <>
                        <ToggleLeft size={28} className={styles.toggleOff} />
                        <span className={styles.statusLabelOff}>Inativo</span>
                      </>
                    )}
                  </div>
                </div>
              )}
            </div>
          </div>

          {/* Seção: Permissões de Acesso */}
          <div className={styles.section}>
            <div className={styles.permHeader}>
              <h3 className={styles.sectionTitle}>
                <Lock size={13} /> Permissões de Acesso
              </h3>
              <div className={styles.permBulkBtns}>
                <button
                  className={`${styles.bulkBtn} ${allEnabled ? styles.bulkBtnDisabled : ""}`}
                  onClick={() => handleAllPermissions(true)}
                  disabled={allEnabled}
                >
                  Todas
                </button>
                <button
                  className={`${styles.bulkBtn} ${noneEnabled ? styles.bulkBtnDisabled : ""}`}
                  onClick={() => handleAllPermissions(false)}
                  disabled={noneEnabled}
                >
                  Nenhuma
                </button>
              </div>
            </div>

            <div className={styles.permGrid}>
              {PAGES.map((page) => {
                const enabled = !!form.permissions[page.key];
                return (
                  <div
                    key={page.key}
                    className={`${styles.permCard} ${enabled ? styles.permCardOn : ""}`}
                    onClick={() => togglePermission(page.key)}
                    role="checkbox"
                    aria-checked={enabled}
                    tabIndex={0}
                    onKeyDown={(e) => e.key === "Enter" && togglePermission(page.key)}
                  >
                    <div className={styles.permCardDot} />
                    <span className={styles.permCardLabel}>{page.label}</span>
                    <span className={styles.permCardPath}>{page.path}</span>
                  </div>
                );
              })}
            </div>
          </div>
        </div>

        {/* ── Footer ── */}
        <div className={styles.footer}>
          <button className={styles.cancelBtn} onClick={onClose}>
            Cancelar
          </button>
          <button className={styles.saveBtn} onClick={handleSubmit}>
            Salvar alterações
          </button>
        </div>
      </div>
    </div>
  );
}