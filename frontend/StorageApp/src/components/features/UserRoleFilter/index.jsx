import styles from "../../../styles/features/UserRoleFilter.module.css";

const FILTERS = [
  { label: "Todos", value: "all" },
  { label: "Admin", value: "Admin" },
  { label: "Manager", value: "Manager" },
  { label: "Member", value: "Member" },
];
 
export default function UserRoleFilter({ activeRole, onChange }) {
  return (
    <div className={styles.filterGroup}>
      {FILTERS.map(({ label, value }) => (
        <button
          key={value}
          className={`${styles.pill} ${activeRole === value ? styles.active : ""}`}
          onClick={() => onChange(value)}
        >
          {label}
        </button>
      ))}
    </div>
  );
}
