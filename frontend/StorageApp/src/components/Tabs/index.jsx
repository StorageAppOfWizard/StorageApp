import { useState } from "react";
import './Tabs.css'

export default function Tabs({ tabs, onChange, defaultValue }) {
  const [active, setActive] = useState(defaultValue || tabs[0].value);

  const handleClick = (value) => {
    setActive(value);
    onChange(value);
  };

  return (
    <div>
      <div className="main">
        {tabs.map((tab) => (
          <button
            key={tab.value}
            onClick={() => handleClick(tab.value)}
            className={`tab-button ${active === tab.value ? "active" : ""}`}
          >
            {tab.label}
          </button>
        ))}
      </div>
    </div>
  );
}
