import React from 'react';
import { Trash2, Edit, CheckCircle, XCircle, Snowflake } from 'lucide-react';
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

const getUserStatus = (user) => {
  if (user.isFrozen || user.frozen) return 'frozen';
  if (user.isActive === false || user.ativo === false) return 'inactive';
  return 'active';
};


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

      <td>
        {(() => {
          const status = getUserStatus(user);

          if (status === 'active') {
            return (
              <span className={styles.statusActive}>
                <CheckCircle size={16} />
              </span>
            );
          }

          if (status === 'inactive') {
            return (
              <span className={styles.statusInactive}>
                <XCircle size={16} />
              </span>
            );
          }

          if (status === 'frozen') {
            return (
              <span className={styles.statusFrozen}>
                <Snowflake size={16} />
              </span>
            );
          }
        })()}
      </td>

      <td>

        <span className={styles.actionIcon} onClick={() => onEdit(user.id)}>
          <Edit size={16} />
        </span>
        <span className={styles.actionIcon} onClick={() => onDelete(user.id)}>
          <Trash2 size={16} />
        </span>
      </td>
    </tr>
  );
}