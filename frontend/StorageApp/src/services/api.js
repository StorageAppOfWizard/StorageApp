import axios from "axios";


export const apiMain = axios.create({
  baseURL: "https://localhost:7216/",
  timeout: 10000,
  headers: {
    "Content-Type": "application/json",
    "Authorization": `Bearer ${localStorage.getItem("token") || ""}`,
  },
});


export const apiAuth = axios.create({
  baseURL: "https://localhost:7249/",
  timeout: 5000,
  headers: {
    "Content-Type": "application/json",
  },
});