// Para operações de mutação (POST, PUT, DELETE)
import { useState } from "react";
import axios from "axios";
import { endpointMap } from "../endpoints";

const getEndpointConfig = (endpointPath) => {
  const [group, specificEndpoint] = endpointPath.split(".");
  return endpointMap[group]?.[specificEndpoint];
};

export const useMutateApi = (endpoint) => {
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);
  const [mutationResult, setMutationResult] = useState(null);

  const mutate = async (mutationData) => {
    const controller = new AbortController();
    setLoading(true);
    setError(null);
    setMutationResult(null);

    try {
      const config = getEndpointConfig(endpoint);
      if (!config || !config.isMutation) {
        throw new Error("Endpoint não suportado ou não é uma mutação");
      }

      const response = await config.fn(mutationData, controller.signal);
      setMutationResult(response);
      return response;
    } catch (error) {
      if (!axios.isCancel(error)) {
        setError(`Erro ao processar mutação: ${error.message}`);
      }
      throw error;
    } finally {
      setLoading(false);
    }
  };

  return { mutate, loading, error, mutationResult };
};
