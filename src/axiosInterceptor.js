import axios from "axios";
import { getUserBilgileri } from "./Services/authService";

axios.interceptors.request.use(
  (config) => {
    const token = getUserBilgileri();
    if (token) {
      config.headers.Authorization = `Bearer ${token.token}`;
    }
    return config;
  },
  (error) => Promise.reject(error)
);
