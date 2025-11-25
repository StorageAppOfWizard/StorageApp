import { BrowserRouter } from 'react-router-dom'
import RoutesApp from './routes/index'
import { AuthProvider } from './contexts/AuthContext'
import './index.css'

function App() {
  return (
    <BrowserRouter>
      <AuthProvider>
        <RoutesApp />
      </AuthProvider>
    </BrowserRouter>
  );
}

export default App;