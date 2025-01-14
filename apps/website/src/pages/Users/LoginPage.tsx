import { useNavigate } from "react-router-dom";
import { LoginCredentials } from "../../features/Users/Users.types";
import LoginForm from "../../features/Users/Login/LoginForm";

import './auth.css'
import { useAuth } from "../../components/Authentication/AuthenticationProvider";

export default function LoginPage() {
    const {login} = useAuth()
    const navigate = useNavigate()

    const handleLogin = (credentials: LoginCredentials) => {
        login()
        navigate('/')
    } 

    return <LoginForm onLogin={handleLogin}/>
}