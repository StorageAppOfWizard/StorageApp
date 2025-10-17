import { useState, useContext } from 'react'
import { Link } from 'react-router-dom'
import styles from "../../styles/Singn.module.css";


export default function SingnIn() {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');

    

    async function handleSignIn(e) {
        e.preventDefault();


        if (email !== '' && password !== '') {
            await singIn(email, password, setEmail, setPassword);
        }
    }

    return (
            <div className={styles.container}>
                <div className={styles.login}>

                    <form onSubmit={handleSignIn}>
                        <h1>Login</h1>
                        <input
                            type="text"
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

                        <button type='submit' value='Entrar'>
                            Entrar
                        </button>

                        <Link to="/cadastrar">Criar Conta</Link>
                    </form>

                    

                </div>
            </div>
    )
}