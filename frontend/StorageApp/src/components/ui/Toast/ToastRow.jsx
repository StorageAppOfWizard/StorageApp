import { X, CheckCircle, AlertCircle, Info, AlertTriangle } from 'lucide-react';

export const Toast = ({ id, message, type }) => {
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
                
                @keyframes slideOutRight {
                    from {
                        transform: translateX(0);
                        opacity: 1;
                    }
                    to {
                        transform: translateX(100%);
                        opacity: 0;
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
                    pointerEvents: 'auto'
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
            </div>
        </>
    );
};