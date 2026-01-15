import { useEffect, useRef } from 'react';
import { useToast } from '../hooks/useToast';


export const useToastOnMutation = (mutationResult, error, messages) => {
  const toast = useToast();
  
  const successShown = useRef(false);
  const errorShown = useRef(false);

  // Effect para sucesso
  useEffect(() => {
    if (mutationResult && !successShown.current) {
      successShown.current = true;
      toast.success(messages.success);
    }
  }, [mutationResult, toast, messages.success]);

  // Effect para erro
  useEffect(() => {
    if (error && !errorShown.current) {
      errorShown.current = true;
      toast.error(`${messages.error}: ${error}`);
    }
  }, [error, toast, messages.error]);

  // Retorna função para resetar os flags
  const reset = () => {
    successShown.current = false;
    errorShown.current = false;
  };

  return reset;
};