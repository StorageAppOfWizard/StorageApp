// Para operações de leitura (GET)
import { useState, useEffect } from "react";
import axios from "axios";
import { endpointMap } from "../endpoints";

const getEndpointConfig = (endpointPath) => {
  const [group, specificEndpoint] = endpointPath.split(".");
  return endpointMap[group]?.[specificEndpoint];
};

export const useFetchApi = (endpoint, options = {}) => {
  const [data, setData] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const controller = new AbortController();
    let isMounted = true;

    const fetchData = async () => {
      try {
        setLoading(true);
        const config = getEndpointConfig(endpoint);
        if (!config) {
          throw new Error("Endpoint não suportado");
        }

        const response = await config.fn({
          ...options,
          signal: controller.signal,
        });
        if (isMounted && response) {
          const transformedData = config.transform
            ? config.transform(response)
            : response;
          setData(transformedData);
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
  }, [endpoint, JSON.stringify(options)]);

  return { data, loading, error };
};
