import { createContext, useContext, useState } from "react";

interface AuthenticationProviderProps {
    children: React.ReactNode
}

interface AuthenticationContext {
    isAuthenticated: boolean
    login: () => void
    logout: () => void
}

const AuthContext = createContext<AuthenticationContext | undefined>(undefined);

export function AuthenticationProvider({children}: AuthenticationProviderProps) {
    const [isAuthenticated, setIsAuthenticated] = useState(false)

    const login = () => setIsAuthenticated(true)
    const logout = () => setIsAuthenticated(false)

    return (
        <AuthContext.Provider value={{isAuthenticated, login, logout}}>
            {children}
        </AuthContext.Provider>
    )
}

export const useAuth = () => {
    const context = useContext(AuthContext)
    if (!context) {
        throw new Error('useAuth did not find an AuthenticationProvider. Please ensure your component is wrapped in an AuthenticationProvider')
    }

    return context;
}