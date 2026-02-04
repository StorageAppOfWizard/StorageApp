import { useState, useEffect, useCallback } from "react";
import axios from "axios";
import { endpointMap } from "../endpoints";

const getEndpointConfig = (endpointPath) => {
  const [group, specificEndpoint] = endpointPath.split(".");
  return endpointMap[group]?.[specificEndpoint];
};

export const useFetchApi = (endpoint, options = {}, queryParams={}) => {
  const [data, setData] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  const fetchData = useCallback(async () => {
    const controller = new AbortController();

    try {
      setLoading(true);
      const config = getEndpointConfig(endpoint);
      if (!config) {
        throw new Error("Endpoint não suportado");
      }

      const response = await config.fn({
        ...options,
        queryParams,
        signal: controller.signal,
      });

      const transformedData = config.transform
        ? config.transform(response)
        : response;

      setData(transformedData);
    } catch (error) {
      if (!axios.isCancel(error)) {
        setError(`Erro ao carregar dados: ${error.message}`);
      }
    } finally {
      setLoading(false);
    }

    return () => controller.abort();
  }, [endpoint, JSON.stringify(options)]);

  useEffect(() => {
    fetchData();
  }, [fetchData]);

  return { data, loading, error, refetch: fetchData };
};
