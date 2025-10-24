// src/contexts/ToastContext.jsx
import { createContext, useContext, useState, useCallback } from 'react';
import { X, CheckCircle, AlertCircle, Info, AlertTriangle } from 'lucide-react';

const ToastContext = createContext();

export const useToast = () => {
    const context = useContext(ToastContext);
    if (!context) {
        throw new Error('useToast deve ser usado dentro de ToastProvider');
    }
    return context;
};

export const ToastProvider = ({ children }) => {
    const [toasts, setToasts] = useState([]);

    const removeToast = useCallback((id) => {
        setToasts(prev => prev.filter(toast => toast.id !== id));
    }, []);

    const addToast = useCallback((message, type = 'info', duration = 3000) => {
        const id = Date.now() + Math.random(); // ID único
        
        // Limita para no máximo 3 toasts
        setToasts(prev => {
            // Remove duplicatas da mesma mensagem
            const filtered = prev.filter(t => t.message !== message);
            
            // Mantém no máximo 3 toasts
            const limited = filtered.slice(-2); // Pega os últimos 2
            
            return [...limited, { id, message, type, duration }];
        });

        // Remove automaticamente após o duration
        if (duration > 0) {
            setTimeout(() => {
                setToasts(prev => prev.filter(t => t.id !== id));
            }, duration);
        }

        return id;
    }, []);

    // Atalhos para diferentes tipos
    const success = useCallback((message, duration = 3000) => addToast(message, 'success', duration), [addToast]);
    const error = useCallback((message, duration = 4000) => addToast(message, 'error', duration), [addToast]);
    const warning = useCallback((message, duration = 3000) => addToast(message, 'warning', duration), [addToast]);
    const info = useCallback((message, duration = 3000) => addToast(message, 'info', duration), [addToast]);

    return (
        <ToastContext.Provider value={{ addToast, removeToast, success, error, warning, info }}>
            {children}
            <ToastContainer toasts={toasts} onRemove={removeToast} />
        </ToastContext.Provider>
    );
};

// Componente que renderiza os toasts
const ToastContainer = ({ toasts, onRemove }) => {
    return (
        <div style={{
            position: 'fixed',
            top: '20px',
            right: '20px',
            zIndex: 9999,
            display: 'flex',
            flexDirection: 'column',
            gap: '10px',
            maxWidth: '400px',
            pointerEvents: 'none'
        }}>
            {toasts.map(toast => (
                <Toast key={toast.id} toast={toast} onRemove={onRemove} />
            ))}
        </div>
    );
};

// Componente individual de Toast
const Toast = ({ toast, onRemove }) => {
    const { id, message, type } = toast;

    const styles = {
        success: {
            backgroundColor: '#10b981',
            icon: <CheckCircle size={20} />
        },
        error: {
            backgroundColor: '#ef4444',
            icon: <AlertCircle size={20} />
        },
        warning: {
            backgroundColor: '#f59e0b',
            icon: <AlertTriangle size={20} />
        },
        info: {
            backgroundColor: '#3b82f6',
            icon: <Info size={20} />
        }
    };

    const typeStyle = styles[type] || styles.info;

    return (
        <>
            <style>{`
                @keyframes slideInRight {
                    from {
                        transform: translateX(100%);
                        opacity: 0;
                    }
                    to {
                        transform: translateX(0);
                        opacity: 1;
                    }
                }
            `}</style>
            
            <div
                style={{
                    backgroundColor: typeStyle.backgroundColor,
                    color: 'white',
                    padding: '16px',
                    borderRadius: '8px',
                    boxShadow: '0 10px 25px rgba(0, 0, 0, 0.2)',
                    display: 'flex',
                    alignItems: 'center',
                    gap: '12px',
                    minWidth: '300px',
                    maxWidth: '400px',
                    animation: 'slideInRight 0.3s ease-out',
                    pointerEvents: 'auto',
                    position: 'relative'
                }}
            >
                <div style={{ flexShrink: 0 }}>
                    {typeStyle.icon}
                </div>
                
                <div style={{ 
                    flex: 1, 
                    fontSize: '14px', 
                    lineHeight: '1.4',
                    wordBreak: 'break-word'
                }}>
                    {message}
                </div>
                
                <button
                    onClick={() => onRemove(id)}
                    style={{
                        background: 'none',
                        border: 'none',
                        color: 'white',
                        cursor: 'pointer',
                        padding: '4px',
                        display: 'flex',
                        alignItems: 'center',
                        opacity: 0.8,
                        transition: 'opacity 0.2s',
                        flexShrink: 0
                    }}
                    onMouseEnter={(e) => e.currentTarget.style.opacity = '1'}
                    onMouseLeave={(e) => e.currentTarget.style.opacity = '0.8'}
                    aria-label="Fechar notificação"
                >
                    <X size={18} />
                </button>
            </div>
        </>
    );
};