import { useState, useCallback } from 'react';

export const useRegisterValidation = () => {
  const [errors, setErrors] = useState({});
  const [touched, setTouched] = useState({});

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
  }, []);


  const validatePasswordConfirmed = useCallback((value, password) => {
    if (!value || value.length === 0) {
      return "O campo Confirmar Senha é obrigatório";
    }
    if (value !== password) {
      return "As senhas não coincidem";
    }
    return null;
  }, []);


  const validateField = useCallback((fieldName, value, password = null) => {
    let error = null;

    switch (fieldName) {
      case 'userName':
        error = validateUserName(value);
        break;
      case 'email':
        error = validateEmail(value);
        break;
      case 'password':
        error = validatePassword(value);
        break;
      case 'passwordConfirmed':
        error = validatePasswordConfirmed(value, password);
        break;
      default:
        break;
    }

    setErrors(prev => ({
      ...prev,
      [fieldName]: error
    }));

    return error;
  }, [validateUserName, validateEmail, validatePassword, validatePasswordConfirmed]);


  const handleBlur = useCallback((fieldName) => {
    setTouched(prev => ({
      ...prev,
      [fieldName]: true
    }));
  }, []);


  
  const validateAll = useCallback((userName, email, password, passwordConfirmed) => {
    const newErrors = {
      userName: validateUserName(userName),
      email: validateEmail(email),
      password: validatePassword(password),
      passwordConfirmed: validatePasswordConfirmed(passwordConfirmed, password)
    };

    setErrors(newErrors);


    setTouched({
      userName: true,
      email: true,
      password: true,
      passwordConfirmed: true
    });


    return !Object.values(newErrors).some(error => error !== null);
  }, [validateUserName, validateEmail, validatePassword, validatePasswordConfirmed]);


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
    resetValidation
  };
};