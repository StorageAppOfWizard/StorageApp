import { useState, useEffect } from "react";
import { getProducts } from "../services/productService";
import { getBrands } from "../services/brandService";
import { getCategories } from "../services/categoryService";

import axios from "axios";

export const useApi = (endpoint, limit = 15) => {
  const [data, setData] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const controller = new AbortController();
    let isMounted = true;

    const fetchData = async () => {
      try {
        setLoading(true);
        let response;
        if (endpoint == "Product") { 
          response = await getProducts(controller.signal);
          if (isMounted && response) {
            const mappedProducts = response.map((product) => ({
              ...product,
              brand: product.brandName,
              stock: product.quantity,
            }));
            
            setData(mappedProducts);
          }
          console.log("Response dos produtos:", response);
          
        } else if (endpoint === "Brand") {
          response = await getBrands(controller.signal);
          if (isMounted) setData(response);
        } else if (endpoint === "Category") {
          response = await getCategories(controller.signal);
          if (isMounted) setData(response);
        } else {
          throw new Error("Endpoint não suportado");
        }
      } catch (error) {
        if (!axios.isCancel(error) && isMounted) {
          setError(`Erro ao carregar dados: ${error.message}`);
        }
      } finally {
        if (isMounted) setLoading(false);
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