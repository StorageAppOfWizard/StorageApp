import axios from "axios";


export const apiMain = axios.create({
  baseURL: "http://localhost:5185/",
  timeout: 10000,
  headers: {
    "Content-Type": "application/json",
    "Authorization": `Bearer ${localStorage.getItem("token") || ""}`,
  },
});


export const apiAuth = axios.create({
  baseURL: "http://localhost:5111/",
  timeout: 5000,
  headers: {
    "Content-Type": "application/json",
  },
});