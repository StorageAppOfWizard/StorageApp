import { useRef, useEffect } from "react";
import { Link, useNavigate } from "react-router-dom";
import styles from "../../styles/Singn.module.css";
import { useMutateApi } from "../../hooks/useMutateApi";
import { useAuthForm } from "../../hooks/useAuthForm";
import { ValidatedInput } from "../../components/ValidateInput";
import { useToast } from "../../contexts/ToastContext";

export default function Login() {
    const { mutate, loading, error, mutationResult } = useMutateApi("Auth.UserLogin");
    const { values, handlers, blurHandlers, errors, touched, validate } = useAuthForm("login");
    const navigate = useNavigate();
    const toast = useToast();

    const successShown = useRef(false);
    const errorShown = useRef(false);

    async function handleLogin(e) { 
        e.preventDefault();
        errorShown.current = false;
        successShown.current = false;

        if (!validate()) {
            toast.warning("Por favor, preencha todos os campos corretamente");
            return;
        }

        await mutate(values);
    }

    useEffect(() => {
        if (mutationResult && !successShown.current) {
            successShown.current = true;

            toast.success("Login efetuado com sucesso!");
            setTimeout(() => {
                navigate("/produtos", { replace: true });
            }, 1000);
        }
    }, [mutationResult, navigate, toast]);

    useEffect(() => {
        if (error && !errorShown.current) {
            errorShown.current = true;
            toast.error("Erro ao fazer login: " + error);
        }
    }, [error, toast]);

    return (
        <div className={styles.container}>
            <div className={styles.login}>
                <form onSubmit={handleLogin}>
                    <h1>Entrar</h1>

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

                    <button type="submit" disabled={loading}>
                        {loading ? "Entrando..." : "Entrar"}
                    </button>

                    <Link to="/cadastrar">Não tenho conta</Link>
                </form>
            </div>
        </div>
    );
}
