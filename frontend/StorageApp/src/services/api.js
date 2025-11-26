import axios from "axios";


export const apiMain = axios.create({
  baseURL: "http://4.229.146.92/",
  timeout: 10000,
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

// apiAuth.interceptors.request.use((config) => {
//   const token = localStorage.getItem("token");

//   if (token) {
//     config.headers.common['Authorization'] = `Bearer ${token}`;
//   }

//   return config;
// })