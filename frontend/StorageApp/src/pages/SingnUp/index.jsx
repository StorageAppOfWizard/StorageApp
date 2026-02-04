import { useState, useEffect, useRef } from "react";
import { Link, useNavigate } from "react-router-dom";
import styles from "../../styles/pages/Singn.module.css";
import { useMutateApi } from "../../hooks/useMutateApi";
import { useAuthForm } from "../../hooks/useAuthForm";
import { ValidatedInput } from "../../components/ValidateInput";
import { useToast } from "../../hooks/useToast";

export default function SingnUp() {
    const [formError, setFormError] = useState(null);
    const { mutate, loading, error, mutationResult } = useMutateApi("Auth.UserCreate");
    const navigate = useNavigate();
    const toast = useToast();

    const successShown = useRef(false);
    const errorShown = useRef(false);

    const {
        values,
        handlers,
        blurHandlers,
        errors,
        touched,
        resetForm
    } = useAuthForm("register");

async function handleSignUp(e) {
    e.preventDefault();
    setFormError(null);
    errorShown.current = false;

    if (errors.password != null || errors.email != null) {
            console.log(errors);
            toast.warning("Por favor, preencha todos os campos corretamente");
            return;
        }

    successShown.current = false;
    await mutate(values);
}

useEffect(() => {
    if (mutationResult && !successShown.current) {
        successShown.current = true;

        toast.success("Cadastro criado com sucesso!");
        resetForm();
        setFormError(null);

        setTimeout(() => {
            navigate("/", { replace: true });
        }, 1500);
    }
}, [mutationResult, navigate, toast, resetForm]);

    useEffect(() => {
        if (error && !errorShown.current) {
            errorShown.current = true;
            
            toast.error(error)
        }
    }, [error]);

    return (
        <div className={styles.container}>
            <div className={styles.login}>
                <form onSubmit={handleSignUp}>
                    <h1>Criar Conta</h1>

                    <ValidatedInput
                        type="text"
                        placeholder="Nome do Usuário"
                        value={values.userName}
                        onChange={handlers.userName}
                        onBlur={blurHandlers.userName}
                        error={errors.userName}
                        touched={touched.userName}
                        disabled={loading}
                    />

                    <ValidatedInput
                        type="email"
                        placeholder="email@email.com"
                        value={values.email}
                        onChange={handlers.email}
                        onBlur={blurHandlers.email}
                        error={errors.email}
                        touched={touched.email}
                        disabled={loading}
                    />

                    <ValidatedInput
                        type="password"
                        placeholder="Senha"
                        value={values.password}
                        onChange={handlers.password}
                        onBlur={blurHandlers.password}
                        error={errors.password}
                        touched={touched.password}
                        disabled={loading}
                    />

                    <ValidatedInput
                        type="password"
                        placeholder="Confirmar Senha"
                        value={values.passwordConfirmed}
                        onChange={handlers.passwordConfirmed}
                        onBlur={blurHandlers.passwordConfirmed}
                        error={errors.passwordConfirmed}
                        touched={touched.passwordConfirmed}
                        disabled={loading}
                    />

                    <button type="submit" disabled={loading}>
                        {loading ? "Criando..." : "Criar Conta"}
                    </button>

                    <Link to="/">Já tenho conta</Link>
                </form>
            </div>
        </div>
    );
}