import { useState, useContext } from 'react'
import { Link } from 'react-router-dom'


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
            <div className='container-center'>
                <div className='login'>

                    <form onSubmit={handleSignIn}>
                        <h1>Entrar</h1>
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
                    </form>

                    <Link to="/register">Criar Conta</Link>

                </div>
            </div>
    )
}