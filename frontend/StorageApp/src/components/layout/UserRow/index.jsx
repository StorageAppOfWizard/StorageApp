import React from 'react';
import { Trash2, Edit2, Check } from 'lucide-react';
import styles from '../../../styles/pages/usuarios.module.css';

export default function UserRow({ user, onEdit, onDelete }) {
  if (!user) return null;

  const getRoleColor = (role) => {
    const r = typeof role === 'string' ? role.toLowerCase() : '';
    switch (r) {
      case 'admin':
        return styles.roleAdmin;
      case 'manager':
        return styles.roleManager;
      case 'member':
        return styles.roleMember;
      default:
        return styles.roleMember;
    }
  };

  const permissions = {
    atendimento: user.atendimento !== false,
    callcenter: user.callcenter !== false,
    tecnico: user.tecnico !== false,
    vendedor: user.vendedor !== false,
    twoFa: user.twoFactorEnabled === true || user.twoFa === true,
    ativo: user.isActive !== false && user.ativo !== false,
  };

  const PermissionIcon = ({ enabled }) => (
    <div className={styles.permissionWrapper}>
      {enabled ? (
        <Check size={16} className={styles.permissionEnabled} />
      ) : (
        <span className={styles.permissionDisabled}>—</span>
      )}
    </div>
  );

  return (
    <tr className={styles.tableRow}>
      <td>
        <input type="checkbox" className={styles.checkbox} />
      </td>

      <td className={styles.nameCell}>
        <div className={styles.userName}>{user.userName || 'N/A'}</div>
      </td>

      <td className={styles.emailCell}>
        {user.email || 'N/A'}
      </td>

      <td>
        <span className={`${styles.roleTag} ${getRoleColor(user.role)}`}>
          {String(user.role || 'Member')}
        </span>
      </td>

      <td><PermissionIcon enabled={permissions.atendimento} /></td>
      <td><PermissionIcon enabled={permissions.callcenter} /></td>
      <td><PermissionIcon enabled={permissions.tecnico} /></td>
      <td><PermissionIcon enabled={permissions.vendedor} /></td>
      <td><PermissionIcon enabled={permissions.twoFa} /></td>

      <td>
        <div
          className={
            permissions.ativo
              ? styles.statusActive
              : styles.statusInactive
          }
        />
      </td>

      <td>
        <div className={styles.actions}>
          <button
            onClick={() => onEdit(user.id)}
            className={styles.iconButton}
            title="Editar"
          >
            <Edit2 size={18} />
          </button>

          <button
            onClick={() => onDelete(user.id)}
            className={`${styles.iconButton} ${styles.delete}`}
            title="Excluir"
          >
            <Trash2 size={18} />
          </button>
        </div>
      </td>
    </tr>
  );
}