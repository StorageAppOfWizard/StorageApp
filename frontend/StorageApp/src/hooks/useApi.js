// src/hooks/useApi.js
import { useState, useEffect } from "react";
import { getProducts } from "../services/productService";

export const useApi = (endpoint, limit = 15) => {
  const [data, setData] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const controller = new AbortController();
    let isMounted = true;

    const fetchData = async () => {
      try {
        if (endpoint === "products") {
          const response = await getProducts(limit, controller.signal);
          if (isMounted) {
            const mappedProducts = response.products.map((product) => ({
              ...product,
              brand: getRandomBrand(),
              stock: Math.floor(Math.random() * 500), 
            }));
            setData(mappedProducts);
            setLoading(false);
          }
        } else {
          const response = await getProducts(limit, controller.signal);
          if (isMounted) {
            setData(response);
            setLoading(false);
          }
        }
      } catch (error) {
        if (axios.isCancel(error)) {
          console.log(`Requisição ${endpoint} cancelada`);
        } else if (isMounted) {
          setError(`Erro ao carregar dados: ${error.message}`);
          setLoading(false);
        }
      }
    };

    fetchData();

    return () => {
      isMounted = false;
      controller.abort();
    };
  }, [endpoint, limit]);

  return { data, loading, error };
};

function getRandomBrand() {
  const brands = ["Royal Caribbean", "MSC Cruises", "Celebrity Cruises"];
  return brands[Math.floor(Math.random() * brands.length)];
}