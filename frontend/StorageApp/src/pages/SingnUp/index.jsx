// src/pages/singnUp/index.jsx
import { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import styles from "../../styles/Singn.module.css";
import { useMutateApi } from "../../hooks/useMutateApi";

export default function SingnUp() {
    const [userName, setuserName] = useState("");
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [passwordConfirmed, setpasswordConfirmed] = useState("");
    const [formError, setFormError] = useState(null);

    const { mutate, loading, error, mutationResult } = useMutateApi("Auth.UserCreate");

    async function handleSignUp(e) {
        e.preventDefault();

        console.log("Tentando criar conta com:", { userName, email, password, passwordConfirmed });

        // Validação básica
        if (!userName || !email || !password || !passwordConfirmed) {
            setFormError("Todos os campos são obrigatórios");
            return;
        }
        if (password !== passwordConfirmed) {
            setFormError("As senhas não coincidem");
            return;
        }
        if (password.length < 6) {
            setFormError("A senha deve ter pelo menos 6 caracteres");
            return;
        }
        if (!/\S+@\S+\.\S+/.test(email)) {
            setFormError("Email inválido");
            return;
        }

        setFormError(null);
        try {
            await mutate({ userName, email, password, passwordConfirmed });
        } catch (err) {
            console.error("Erro na mutação:", err);
        }
    }

    useEffect(() => {
        if (mutationResult) {
            alert("Usuário criado com sucesso!");
            setuserName("");
            setEmail("");
            setPassword("");
            setpasswordConfirmed("");
            setFormError(null);
        }
    }, [mutationResult]);

    useEffect(() => {
        if (error) {
            alert("Erro: " + error);
        }
    }, [error]);

    return (
        <div className={styles.container}>
            <div className={styles.login}>
                <form onSubmit={handleSignUp}>
                    <h1>Criar Conta</h1>
                    {formError && <p style={{ color: "red" }}>{formError}</p>}
                    <input
                        type="text"
                        placeholder="Nome do Usuário"
                        value={userName}
                        onChange={(e) => setuserName(e.target.value)}
                        disabled={loading}
                    />
                    <input
                        type="email"
                        placeholder="email@email.com"
                        value={email}
                        onChange={(e) => setEmail(e.target.value)}
                        disabled={loading}
                    />
                    <input
                        type="password"
                        placeholder="Senha"
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                        disabled={loading}
                    />
                    <input
                        type="password"
                        placeholder="Confirmar Senha"
                        value={passwordConfirmed}
                        onChange={(e) => setpasswordConfirmed(e.target.value)}
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