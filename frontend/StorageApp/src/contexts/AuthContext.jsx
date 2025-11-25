import { createContext, useContext, useState, useEffect } from 'react';
import { jwtDecode } from 'jwt-decode';
import { endpointMap } from '../endpoints';

const AuthContext = createContext({});

export function AuthProvider({ children }) {
  const [user, setUser] = useState(null);
  const [token, setToken] = useState(null);
  const [loading, setLoading] = useState(true);

  const { Auth } = endpointMap;

  useEffect(() => {
    const loadUser = async () => {
      try {
        const storedToken = localStorage.getItem("token");

        if (!storedToken) {
          setLoading(false);
          return;
        }

        const decoded = jwtDecode(storedToken);
        const isExpired = decoded.exp * 1000 < Date.now();

        if (isExpired) {
          await Auth.UserLogout.fn();
          localStorage.removeItem("token");
          setLoading(false);
          return;
        }

        const baseUser = {
          id: decoded.nameid,
          name: decoded.unique_name,
          role: decoded.role,
        };

        setUser(baseUser);
        setToken(storedToken);

        try {
          const fullUser = await Auth.UserDataFromToken.fn(storedToken);

          if (fullUser) {
            setUser(prev => ({
              ...prev,
              ...fullUser
            }));
          }
        } catch (err) {
          console.warn("Não foi possível buscar dados completos do usuário.");
        }

      } catch (error) {
        console.error("Erro ao carregar usuário:", error);
        await Auth.UserLogout.fn();
      } finally {
        setLoading(false);
      }
    };

    loadUser();
  }, []);

  const login = async (tokenString) => {
    try {
      localStorage.setItem("token", tokenString);
      setToken(tokenString);

      const decoded = jwtDecode(tokenString);

      const baseUser = {
        id: decoded.nameid,
        name: decoded.unique_name,
        role: decoded.role,
      };

      setUser(baseUser);

      try {
        const fullUser = await Auth.UserDataFromToken.fn(tokenString);
        if (fullUser) {
          setUser(prev => ({ ...prev, ...fullUser }));
        }
      } catch (err) {
        console.warn("Falha ao buscar dados completos no login.");
      }

    } catch (error) {
      console.error("Erro no login:", error);
      throw error;
    }
  };


  const logout = async () => {
    try {
      await Auth.UserLogout.fn();
    } catch (err) {
      console.warn("Erro ao deslogar do backend.");
    }

    localStorage.removeItem("token");
    setUser(null);
    setToken(null);
  };


  const refreshUser = async () => {
    if (!token) return;

    try {
      const fullUser = await Auth.UserDataFromToken.fn(token);

      setUser(prev => ({
        ...prev,
        ...fullUser
      }));

    } catch (error) {
      console.error("Erro ao atualizar usuário:", error);
    }
  };

  return (
    <AuthContext.Provider
      value={{
        user,
        token,
        loading,
        login,
        logout,
        refreshUser,
        isAuthenticated: !!user && !!token,
      }}
    >
      {children}
    </AuthContext.Provider>
  );
}

export function useAuth() {
  const context = useContext(AuthContext);
  if (!context) throw new Error("useAuth deve ser usado dentro de AuthProvider");
  return context;
}
