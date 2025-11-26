import { useState, useCallback } from 'react';
import { useAuthValidation } from './components/useAuthValidation.js';

export const useAuthForm = (mode = "register") => {
  const [userName, setuserName] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [passwordConfirmed, setpasswordConfirmed] = useState("");

  const {
    errors,
    touched,
    validateField,
    handleBlur,
    validateAll,
    resetValidation
  } = useAuthValidation (mode);

  const createChangeHandler = useCallback((fieldName, setter, extraValidation = null) => {
    return (e) => {
      const value = e.target.value;
      setter(value);

      if (touched[fieldName]) {
        validateField(fieldName, value, fieldName === 'passwordConfirmed' ? password : null);
      }

      if (extraValidation) extraValidation(value);
    };
  }, [touched, validateField, password]);

  const createBlurHandler = useCallback((fieldName, value, extraParam = null) => {
    return () => {
      handleBlur(fieldName);
      validateField(fieldName, value, extraParam);
    };
  }, [handleBlur, validateField]);

  const resetForm = useCallback(() => {
    setuserName("");
    setEmail("");
    setPassword("");
    setpasswordConfirmed("");
    resetValidation();
  }, [resetValidation]);


  const validate = useCallback(() => {
    if (mode === "login") {
      return validateAll(null, email, password, null);
    }
    return validateAll(userName, email, password, passwordConfirmed);
  }, [mode, validateAll, userName, email, password, passwordConfirmed]);

  return {
    values: {
      userName,
      email,
      password,
      passwordConfirmed
    },
    handlers: {
      userName: createChangeHandler('userName', setuserName),
      email: createChangeHandler('email', setEmail),
      password: createChangeHandler('password', setPassword, (value) => {
        if (touched.passwordConfirmed && passwordConfirmed) {
          validateField('passwordConfirmed', passwordConfirmed, value);
        }
      }),
      passwordConfirmed: createChangeHandler('passwordConfirmed', setpasswordConfirmed)
    },
    blurHandlers: {
      userName: createBlurHandler('userName', userName),
      email: createBlurHandler('email', email),
      password: createBlurHandler('password', password),
      passwordConfirmed: createBlurHandler('passwordConfirmed', passwordConfirmed, password)
    },
    errors,
    touched,
    validate,
    resetForm
  };
};
