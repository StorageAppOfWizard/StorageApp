import axios from "axios";


export const apiMain = axios.create({
  baseURL: import.meta.env.VITE_API_MAIN_URL || "https://localhost:8000",
  timeout: 10000,
  headers: {
    "Content-Type": "application/json",
  },
});


export const apiAuth = axios.create({
  baseURL: import.meta.env.VITE_API_AUTH_URL || "https://localhost:5000",
  timeout: 10000,
  headers: {
    "Content-Type": "application/json",
  },
});