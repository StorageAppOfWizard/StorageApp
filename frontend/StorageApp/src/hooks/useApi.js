import { useState, useEffect } from "react";
import { getProducts, getBrands, getCategories } from "../services/productService";
import axios from "axios";

export const useApi = (endpoint, limit = 15) => {
  const [data, setData] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const controller = new AbortController();
    let isMounted = true;

    const fetchData = async () => {
      try {
        setLoading(true);
        let response;
        //Mudar a regra de negócio para verificar o endpoint e chamar o serviço correto porque vai ter muitos endpoints e vai ficar inviável fazer um hook para cada um
        // Verificar se é possível passar o serviço como parâmetro e verificar se existe ou não, se não existir lançar um erro, se existir chamar o serviço
        if (endpoint == "Product") { 
          response = await getProducts(20,controller.signal);
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

function getRandomBrand() {
  const brands = ["Royal Caribbean", "MSC Cruises", "Celebrity Cruises"];
  return brands[Math.floor(Math.random() * brands.length)];
}