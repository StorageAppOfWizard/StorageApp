import { Outlet } from "react-router-dom";
import Sidebar from "./Sidebar/index.jsx";
import Header from "./Header/index.jsx";

export default function Layout() {
  return (
    <div style={{ display: "flex", minHeight: "100vh" }}>
      <Sidebar />
      <div style={{ flex: 1, marginLeft: "165px" }}>
        <Header profileName="Luiz"/>
        <main style={{ padding: "60px 20px 20px", minHeight: "calc(100vh - 60px)" }}>
          <Outlet />
        </main>
      </div>
    </div>
  );
}