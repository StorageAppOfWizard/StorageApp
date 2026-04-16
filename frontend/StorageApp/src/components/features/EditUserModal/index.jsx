import { useState, useEffect } from "react";
import { X, User, Mail, Shield, ToggleLeft, ToggleRight, Lock } from "lucide-react";
import styles from "./EditUserModal.module.css";

const ROLES = [
  { value: "Admin", label: "Admin", color: "#f97316" },
  { value: "Membro", label: "Membro", color: "#3b82f6" },
  { value: "DEV", label: "DEV", color: "#a855f7" },
];

const STATUS = [
  { value: "active", label: "Ativo" },
  { value: "inactive", label: "Desativado" },
];

const PAGES = [
  { key: "criar_produto", label: "Criar Produto", path: "/criar/produto" },
  { key: "criar_marca", label: "Criar Marca", path: "/criar/marca" },
  { key: "criar_categoria", label: "Criar Categoria", path: "/criar/categoria" },
  { key: "criar_pedido", label: "Criar Pedido", path: "/criar/pedido" },
  { key: "ultimos_pedidos", label: "Últimos Pedidos", path: "/criar/ultimos-pedidos" },
  { key: "usuarios", label: "Usuários", path: "/usuarios" },
  { key: "pedidos", label: "Pedidos", path: "/pedidos" },
  { key: "configuracoes", label: "Configurações", path: "/configuracoes" },
];

export default function EditUserModal({ user, onClose, onSave }) {
  const [form, setForm] = useState({
    name: "",
    email: "",
    role: "Membro",
    status: "active",
    permissions: {},
  });

  useEffect(() => {
    if (user) {
      setForm({
        name: user.nome || "",
        email: user.email || "",
        role: user.role || "Membro",
        status: user.status || "active",
        permissions: user.permissions || {},
      });
    }
  }, [user]);

  const handleChange = (field, value) => {
    setForm((prev) => ({ ...prev, [field]: value }));
  };

  const togglePermission = (key) => {
    setForm((prev) => ({
      ...prev,
      permissions: {
        ...prev.permissions,
        [key]: !prev.permissions[key],
      },
    }));
  };

  const handleAllPermissions = (val) => {
    const all = {};
    PAGES.forEach((p) => (all[p.key] = val));
    setForm((prev) => ({ ...prev, permissions: all }));
  };

  const handleSubmit = () => {
    onSave?.({ ...user, ...form });
    onClose?.();
  };

  const allEnabled = PAGES.every((p) => form.permissions[p.key]);
  const noneEnabled = PAGES.every((p) => !form.permissions[p.key]);

  if (!user) return null;

  return (
    <div className={styles.overlay} onClick={(e) => e.target === e.currentTarget && onClose?.()}>
      <div className={styles.modal}>

        {/* Header */}
        <div className={styles.header}>
          <div className={styles.headerLeft}>
            <div className={styles.avatar}>
              {form.name?.[0]?.toUpperCase() || "?"}
            </div>
            <div>
              <h2 className={styles.title}>Editar Usuário</h2>
              <p className={styles.subtitle}>{user.email}</p>
            </div>
          </div>
          <button className={styles.closeBtn} onClick={onClose}>
            <X size={18} />
          </button>
        </div>

        <div className={styles.body}>

          {/* Nome + Email */}
          <div className={styles.section}>
            <h3 className={styles.sectionTitle}>
              <User size={14} /> Informações Básicas
            </h3>
            <div className={styles.row}>
              <div className={styles.field}>
                <label>Nome</label>
                <input
                  value={form.nome}
                  onChange={(e) => handleChange("name", e.target.value)}
                  placeholder="Nome completo"
                />
              </div>
              <div className={styles.field}>
                <label>Email</label>
                <input
                  value={form.email}
                  onChange={(e) => handleChange("email", e.target.value)}
                  placeholder="email@exemplo.com"
                />
              </div>
            </div>
          </div>

          {/* Função + Status */}
          <div className={styles.section}>
            <h3 className={styles.sectionTitle}>
              <Shield size={14} /> Função & Status
            </h3>
            <div className={styles.row}>
              <div className={styles.field}>
                <label>Função</label>
                <div className={styles.roleGroup}>
                  {ROLES.map((r) => (
                    <button
                      key={r.value}
                      className={`${styles.roleBtn} ${form.role === r.value ? styles.roleBtnActive : ""}`}
                      style={form.role === r.value ? { "--role-color": r.color } : {}}
                      onClick={() => handleChange("role", r.value)}
                    >
                      {r.label}
                    </button>
                  ))}
                </div>
              </div>
              <div className={styles.field}>
                <label>Status</label>
                <div className={styles.statusToggle} onClick={() => handleChange("status", form.status === "active" ? "inactive" : "active")}>
                  {form.status === "active" ? (
                    <><ToggleRight size={28} className={styles.toggleOn} /> <span className={styles.statusLabelOn}>Ativo</span></>
                  ) : (
                    <><ToggleLeft size={28} className={styles.toggleOff} /> <span className={styles.statusLabelOff}>Desativado</span></>
                  )}
                </div>
              </div>
            </div>
          </div>

          {/* Permissões */}
          <div className={styles.section}>
            <div className={styles.permHeader}>
              <h3 className={styles.sectionTitle}>
                <Lock size={14} /> Permissões de Acesso
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

        {/* Footer */}
        <div className={styles.footer}>
          <button className={styles.cancelBtn} onClick={onClose}>Cancelar</button>
          <button className={styles.saveBtn} onClick={handleSubmit}>Salvar alterações</button>
        </div>
      </div>
    </div>
  );
}