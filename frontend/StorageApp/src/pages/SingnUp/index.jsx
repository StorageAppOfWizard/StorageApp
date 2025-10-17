import { useState, useContext } from "react";
import { Link } from "react-router-dom";
import styles from "../../styles/Singn.module.css";

export default function SingnUp() {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [name, setName] = useState('');

    

    async function handleSubmit(e) {
        e.preventDefault();

        if(name !== "" && email !== "" && password !== ""){
            await SingnUp(email, password, name)
        }
    }

    return (
        <div className={styles.container}>
            <div className={styles.login}>

                <form onSubmit={handleSubmit}>
                    <h1>Cadastrar</h1>

                    <input
                        type="text"
                        placeholder="Nome do Usuário"
                        value={name}
                        onChange={(e) => setName(e.target.value)}
                    />

                    <input
                        type="email"
                        placeholder="email@email.com"
                        value={email}
                        onChange={(e) => setEmail(e.target.value)}
                    />

                    <input
                        type="password"
                        placeholder="********"
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                    />

                    <button type="submit" value='Entrar'>
                        Criar Conta
                    </button>

                    <Link to="/">Logar</Link>
                </form>
            </div>
        </div>
    )
}