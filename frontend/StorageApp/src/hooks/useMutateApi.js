import { useState } from "react";
import { endpointMap } from "../endpoints";

const getEndpointConfig = (endpointPath) => {
  if (typeof endpointPath === "string") {
    const [group, specificEndpoint] = endpointPath.split(".");
    return endpointMap[group]?.[specificEndpoint];
  }

  if (typeof endpointPath === "object" && endpointPath.fn) {
    return endpointPath;
  }

  return null;
};

export const useMutateApi = (endpoint) => {
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);
  const [mutationResult, setMutationResult] = useState(null);

  const mutate = async (mutationData, options = {}) => {
    const controller = new AbortController();
    setLoading(true);
    setError(null);
    setMutationResult(null);
    console.log("Iniciando mutação para endpoint:", endpoint, "com dados:", mutationData);

    try {
      const config = getEndpointConfig(endpoint);
      if (!config || !config.isMutation) {
        throw new Error("Endpoint inválido ou não é uma mutação");
      }

      const response = await config.fn(mutationData, controller.signal);
      setMutationResult(response);
      console.log("o que está chegando no Mutation: ",mutationData);
      

      if (options.onSuccess) options.onSuccess(response);
      return response;

    } catch (err) {
      console.error("❌ Erro na mutação:", err);

      let message = "Erro desconhecido.";

      const dataErrors = await err.response?.data?.errors;

      if (dataErrors) {
        if (typeof dataErrors === "string") {
          message = dataErrors;
        } else if (Array.isArray(dataErrors)) {
          message = dataErrors.join(", ");
        } else if (typeof dataErrors === "object") {
          message = Object.values(dataErrors).join(", ");
        }
      } else if (err.message) {
        message = err.message;
      }

      setError(message);

      if (options.onError) options.onError(err);
    } finally {
      setLoading(false);
    }
  };

  return { mutate, loading, error, mutationResult };
};