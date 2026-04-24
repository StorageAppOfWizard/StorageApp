import { Shield, Users, Crown, User } from "lucide-react";
import styles from"../../../styles/features/UserStatsCards.module.css";

const CARDS_CONFIG = [
  {
    key: "total",
    label: "Total",
    icon: Users,
    colorClass: styles.cardTotal,
  },
  {
    key: "admin",
    label: "Admins",
    icon: Crown,
    colorClass: styles.cardAdmin,
  },
  {
    key: "manager",
    label: "Managers",
    icon: Shield,
    colorClass: styles.cardManager,
  },
  {
    key: "member",
    label: "Membros",
    icon: User,
    colorClass: styles.cardMember,
  },
];

export default function UserStatsCards({ stats }) {
  return (
    <div className={styles.grid}>
      {CARDS_CONFIG.map(({ key, label, icon: Icon, colorClass }) => (
        <div key={key} className={`${styles.card} ${colorClass}`}>
          <div className={styles.cardHeader}>
            <Icon size={20} className={styles.icon} />
            <span className={styles.label}>{label}</span>
          </div>
          <span className={styles.count}>{stats[key] ?? 0}</span>
        </div>
      ))}
    </div>
  );
}
