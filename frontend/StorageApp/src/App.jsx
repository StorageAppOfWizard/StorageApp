import { BrowserRouter } from 'react-router-dom'
import RoutesApp from './routes/index'
import './index.css'

function App() {
  return (
    <BrowserRouter>
      <RoutesApp />
    </BrowserRouter>
  );
}

export default App;