import { useState } from "react";
import { Eye, EyeOff } from "lucide-react";

export const ValidatedInput = ({ 
    type = "text",
    placeholder,
    value,
    onChange,
    onBlur,
    error,
    touched,
    disabled = false,
    style = {}
}) => {
    const [showPassword, setShowPassword] = useState(false);
    const isPasswordField = type === "password";

    const inputType = isPasswordField && showPassword ? "text" : type;

    return (
        <div style={{ marginBottom: "15px" }}>
            <div style={{ position: "relative" }}>
                <input
                    type={inputType}
                    placeholder={placeholder}
                    value={value}
                    onChange={onChange}
                    onBlur={onBlur}
                    disabled={disabled}
                    style={{
                        borderColor: touched && error ? '#ef4444' : undefined,
                        paddingRight: isPasswordField ? '40px' : undefined,
                        ...style
                    }}
                />
                
                {isPasswordField && (
                    <button
                        type="button"
                        onClick={() => setShowPassword(!showPassword)}
                        disabled={disabled}
                        style={{
                            position: "absolute",
                            right: "10px",
                            top: "50%",
                            transform: "translateY(-50%)",
                            background: "none",
                            border: "none",
                            cursor: disabled ? "not-allowed" : "pointer",
                            padding: "4px",
                            display: "flex",
                            alignItems: "center",
                            justifyContent: "center",
                            color: "#6b7280",
                            maxWidth: "max-content",
                            opacity: disabled ? 0.5 : 1
                        }}
                        aria-label={showPassword ? "Ocultar senha" : "Mostrar senha"}
                    >
                        {showPassword ? (
                            <EyeOff size={20} />
                        ) : (
                            <Eye size={20} />
                        )}
                    </button>
                )}
            </div>
            
            {touched && error && (
                <p style={{ 
                    color: "#ef4444", 
                    fontSize: "0.875rem", 
                    marginTop: "4px",
                    marginBottom: 0
                }}>
                    {error}
                </p>
            )}
        </div>
    );
};