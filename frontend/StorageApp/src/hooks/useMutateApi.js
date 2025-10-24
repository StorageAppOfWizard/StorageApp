// Para operações de mutação (POST, PUT, DELETE)
import { useState } from "react";
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
      console.log(response)
      return response;

    } catch (error) {
      console.log(error)
      setError(`${error.response.data.errors}`);

    } finally {
      setLoading(false);
    }
  };

  return { mutate, loading, error, mutationResult };
};
