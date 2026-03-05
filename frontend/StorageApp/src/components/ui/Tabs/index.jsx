import { Link } from "react-router-dom";
import "./tabs.css";

export default function Tabs({ tabs, currentValue }) {
  return (
    <div className="tabs-container">
      {tabs.map((tab) => (
        <Link
          key={tab.value}
          to={tab.to}
          className={`tab-item ${currentValue === tab.value ? "active" : ""}`}
        >
          {tab.label}
        </Link>
      ))}
    </div>
  );
}
