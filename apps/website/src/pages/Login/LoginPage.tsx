import { redirect } from "react-router-dom";
import { LoginCredentials } from "../../features/Login/Login.types";
import LoginForm from "../../features/Login/LoginForm";

export default function LoginPage() {

    const handleLogin = (credentials: LoginCredentials) => {
        redirect('/')
    } 

    return (
        <div id="login-container">
            <LoginForm onLogin={handleLogin}/>
        </div>
    )
}