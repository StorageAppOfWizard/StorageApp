import { useState, useCallback, useMemo } from 'react';
import * as yup from 'yup';

export const useAuthValidation = (mode = "register") => {
  const [errors, setErrors] = useState({});
  const [touched, setTouched] = useState({});

  // --- SCHEMAS DE VALIDAÇÃO ---
  const registerSchema = useMemo(() => yup.object().shape({
    userName: yup
      .string()
      .required("O campo Nome é obrigatório")
      .min(3, "O campo deve conter entre 3 e 20 caracteres")
      .max(20, "O campo deve conter entre 3 e 20 caracteres")
      .trim(),
    email: yup
      .string()
      .required("O campo E-mail é obrigatório")
      .email("O campo deve conter um e-mail válido")
      .min(3, "O campo deve conter entre 3 e 100 caracteres")
      .max(100, "O campo deve conter entre 3 e 100 caracteres")
      .trim(),
    password: yup
      .string()
      .required("O campo Senha é obrigatório")
      .min(8, "A senha deve ter pelo menos 8 caracteres.")
      .max(150, "A senha não deve exceder 150 caracteres.")
      .matches(/[A-Z]/, "A senha deve conter pelo menos uma letra maiúscula.")
      .matches(/[a-z]/, "A senha deve conter pelo menos uma letra minúscula.")
      .matches(/[0-9]/, "A senha deve conter pelo menos um número.")
      .matches(/[^a-zA-Z0-9]/, "A senha deve conter pelo menos um caractere especial."),
    passwordConfirmed: yup
      .string()
      .required("O campo Confirmar Senha é obrigatório")
      .oneOf([yup.ref('password')], "As senhas não coincidem")
  }), []);

  const loginSchema = useMemo(() => yup.object().shape({
    email: yup
      .string()
      .required("O campo E-mail é obrigatório")
      .email("O campo deve conter um e-mail válido")
      .min(3, "O campo deve conter entre 3 e 100 caracteres")
      .max(100, "O campo deve conter entre 3 e 100 caracteres")
      .trim(),
    password: yup
      .string()
      .required("O campo Senha é obrigatório")
  }), []);

  const schema = mode === "register" ? registerSchema : loginSchema;

  // --- VALIDAÇÃO INDIVIDUAL ---
  const validateField = useCallback(
    async (fieldName, value, allValues = {}) => {
      try {
        // Valida apenas o campo específico
        await yup.reach(schema, fieldName).validate(value);
        
        // Se for passwordConfirmed, precisa validar com a senha também
        if (fieldName === 'passwordConfirmed' && mode === 'register') {
          await schema.validateAt(fieldName, { ...allValues, [fieldName]: value });
        }

        setErrors((prev) => ({
          ...prev,
          [fieldName]: null,
        }));

        return null;
      } catch (error) {
        const errorMessage = error.message;
        
        setErrors((prev) => ({
          ...prev,
          [fieldName]: errorMessage,
        }));

        return errorMessage;
      }
    },
    [schema, mode]
  );

  const handleBlur = useCallback((fieldName) => {
    setTouched((prev) => ({
      ...prev,
      [fieldName]: true,
    }));
  }, []);

  // --- VALIDAÇÃO GLOBAL ---
  const validateAll = useCallback(
    async (formData) => {
      try {
        await schema.validate(formData, { abortEarly: false });
        
        setErrors({});
        
        const touchedFields = Object.keys(formData).reduce((acc, key) => {
          acc[key] = true;
          return acc;
        }, {});
        
        setTouched(touchedFields);

        return true;
      } catch (error) {
        const newErrors = {};
        
        if (error.inner) {
          error.inner.forEach((err) => {
            if (err.path && !newErrors[err.path]) {
              newErrors[err.path] = err.message;
            }
          });
        }

        setErrors(newErrors);

        const touchedFields = Object.keys(formData).reduce((acc, key) => {
          acc[key] = true;
          return acc;
        }, {});

        setTouched(touchedFields);

        return false;
      }
    },
    [schema]
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