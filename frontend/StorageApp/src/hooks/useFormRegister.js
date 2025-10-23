import { useState, useCallback } from 'react';
import { useRegisterValidation } from './useFormValidation.js';

export const useRegisterForm = () => {
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
    } = useRegisterValidation();

    
    const createChangeHandler = useCallback((fieldName, setter, extraValidation = null) => {
        return (e) => {
            const value = e.target.value;
            setter(value);

            
            if (touched[fieldName]) {
                validateField(fieldName, value, fieldName === 'passwordConfirmed' ? password : null);
            }

            
            if (extraValidation) {
                extraValidation(value);
            }
        };
    }, [touched, validateField, password]);

    
    const handleUserNameChange = createChangeHandler('userName', setuserName);
    
    const handleEmailChange = createChangeHandler('email', setEmail);
    
    const handlePasswordChange = createChangeHandler('password', setPassword, (value) => {
        
        if (touched.passwordConfirmed && passwordConfirmed) {
            validateField('passwordConfirmed', passwordConfirmed, value);
        }
    });
    
    const handlePasswordConfirmedChange = createChangeHandler(
        'passwordConfirmed', 
        setpasswordConfirmed
    );


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
        return validateAll(userName, email, password, passwordConfirmed);
    }, [validateAll, userName, email, password, passwordConfirmed]);


    return {

        values: {
            userName,
            email,
            password,
            passwordConfirmed
        },

        handlers: {
            userName: handleUserNameChange,
            email: handleEmailChange,
            password: handlePasswordChange,
            passwordConfirmed: handlePasswordConfirmedChange
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