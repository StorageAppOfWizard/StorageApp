import { useEffect } from "react";
import { Link, useNavigate } from "react-router-dom";
import styles from "../../styles/pages/Singn.module.css";
import { useMutateApi } from "../../hooks/useMutateApi";
import { useAuthForm } from "../../hooks/useAuthForm";
import { ValidatedInput } from "../../components/ValidateInput";
import { useToast } from "../../hooks/useToast";
import { useAuth } from "../../contexts/AuthContext";
import { useToastOnMutation } from "../../hooks/useToastOnMutation";

export default function Login() {
    const { mutate, loading, error, mutationResult } = useMutateApi("Auth.UserLogin");
    const { values, handlers, blurHandlers, errors, touched } = useAuthForm("login");
    const navigate = useNavigate();
    const toast = useToast();
    const { login } = useAuth();

    // Hook de toast que retorna função de reset
    const resetLoginToast = useToastOnMutation(mutationResult, error, {
        success: "Login efetuado com sucesso!",
        error: "Erro ao fazer login"
    });

    async function handleLogin(e) {
        e.preventDefault();
        
        if (errors.password != null || errors.email != null) {
            console.log(errors);
            toast.warning("Por favor, preencha todos os campos corretamente");
            return;
        }

        resetLoginToast(); // Reseta antes de executar
        await mutate(values);
    }

    // Navegação após login bem-sucedido
    useEffect(() => {
        if (mutationResult) {
            setTimeout(async () => {
                await login(mutationResult.token);
                navigate("/produtos", { replace: true });
            }, 1000);
        }
    }, [mutationResult, navigate, login]);

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