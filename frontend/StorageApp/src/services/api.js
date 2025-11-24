import axios from "axios";


export const apiMain = axios.create({
<<<<<<< HEAD
  baseURL: "https://localhost:7216",
  timeout: 10000,
=======
  baseURL: "http://localhost:5185",
  timeout: 5000,
>>>>>>> 0e119f3b3d86b5994b1f7606753560cf34c49b34
  headers: {
    "Content-Type": "application/json",
    "Authorization": `Bearer ${localStorage.getItem("token") || ""}`,
  },
});


export const apiAuth = axios.create({
  baseURL: "http://localhost:5000/",
  timeout: 5000,
  headers: {
    "Content-Type": "application/json",
  },
});

apiAuth.interceptors.request.use((config) => {
  const token = localStorage.getItem("token");

  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }

  return config;
})