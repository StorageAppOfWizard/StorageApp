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

  const { enabled = true, ...restQueryParams } = queryParams;

  const fetchData = useCallback(async (signal) => {
    if (!enabled) {
      setLoading(false);
      return;
    }

    try {
      setLoading(true);
      const config = getEndpointConfig(endpoint);
      if (!config) {
        throw new Error("Endpoint não suportado");
      }

      const response = await config.fn({
        ...options,
        queryParams: restQueryParams,
        signal,
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
  }, [endpoint, JSON.stringify(options), JSON.stringify(restQueryParams)]);

  useEffect(() => {
    const controller = new AbortController();
    fetchData(controller.signal);
    return () => controller.abort();
  }, [fetchData]);

  return { data, loading, error, refetch: fetchData };
};
