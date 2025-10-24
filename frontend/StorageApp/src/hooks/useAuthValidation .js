import { useState, useCallback } from 'react';

export const useAuthValidation = (mode = "register") => {
  const [errors, setErrors] = useState({});
  const [touched, setTouched] = useState({});

  // --- VALIDAÇÕES BÁSICAS ---
  const validateUserName = useCallback((value) => {
    if (!value || !value.trim()) {
      return "O campo Nome é obrigatório";
    }
    if (value.length < 3 || value.length > 20) {
      return "O campo deve conter entre 3 e 20 caracteres";
    }
    return null;
  }, []);

  const validateEmail = useCallback((value) => {
    if (!value || !value.trim()) {
      return "O campo E-mail é obrigatório";
    }
    if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(value)) {
      return "O campo deve conter um e-mail válido";
    }
    if (value.length < 3 || value.length > 100) {
      return "O campo deve conter entre 3 e 100 caracteres";
    }
    return null;
  }, []);

  const validatePassword = useCallback((value) => {
    if (!value || value.length === 0) {
      return "O campo Senha é obrigatório";
    }

    // No login, pode pular as regras mais fortes
    if (mode === "login") return null;

    if (value.length < 8) {
      return "A senha deve ter pelo menos 8 caracteres.";
    }
    if (value.length > 150) {
      return "A senha não deve exceder 150 caracteres.";
    }
    if (!/[A-Z]/.test(value)) {
      return "A senha deve conter pelo menos uma letra maiúscula.";
    }
    if (!/[a-z]/.test(value)) {
      return "A senha deve conter pelo menos uma letra minúscula.";
    }
    if (!/[0-9]/.test(value)) {
      return "A senha deve conter pelo menos um número.";
    }
    if (!/[^a-zA-Z0-9]/.test(value)) {
      return "A senha deve conter pelo menos um caractere especial.";
    }
    return null;
  }, [mode]);

  const validatePasswordConfirmed = useCallback((value, password) => {
    if (!value || value.length === 0) {
      return "O campo Confirmar Senha é obrigatório";
    }
    if (value !== password) {
      return "As senhas não coincidem";
    }
    return null;
  }, []);

  // --- VALIDAÇÃO INDIVIDUAL ---
  const validateField = useCallback(
    (fieldName, value, password = null) => {
      let error = null;

      switch (fieldName) {
        case "userName":
          if (mode === "register") error = validateUserName(value);
          break;
        case "email":
          error = validateEmail(value);
          break;
        case "password":
          error = validatePassword(value);
          break;
        case "passwordConfirmed":
          if (mode === "register") error = validatePasswordConfirmed(value, password);
          break;
        default:
          break;
      }

      setErrors((prev) => ({
        ...prev,
        [fieldName]: error,
      }));

      return error;
    },
    [validateUserName, validateEmail, validatePassword, validatePasswordConfirmed, mode]
  );

  const handleBlur = useCallback((fieldName) => {
    setTouched((prev) => ({
      ...prev,
      [fieldName]: true,
    }));
  }, []);

  // --- VALIDAÇÃO GLOBAL ---
  const validateAll = useCallback(
    (userName, email, password, passwordConfirmed) => {
      let newErrors = {};

      if (mode === "register") {
        newErrors = {
          userName: validateUserName(userName),
          email: validateEmail(email),
          password: validatePassword(password),
          passwordConfirmed: validatePasswordConfirmed(passwordConfirmed, password),
        };
      } else {
        newErrors = {
          email: validateEmail(email),
          password: validatePassword(password),
        };
      }

      setErrors(newErrors);

      const touchedFields =
        mode === "register"
          ? {
              userName: true,
              email: true,
              password: true,
              passwordConfirmed: true,
            }
          : {
              email: true,
              password: true,
            };

      setTouched(touchedFields);

      return !Object.values(newErrors).some((error) => error !== null);
    },
    [mode, validateUserName, validateEmail, validatePassword, validatePasswordConfirmed]
  );

  const resetValidation = useCallback(() => {
    setErrors({});
    setTouched({});
  }, []);

  return {
    errors,
    touched,
    validateField,
    handleBlur,
    validateAll,
    resetValidation,
  };
};
