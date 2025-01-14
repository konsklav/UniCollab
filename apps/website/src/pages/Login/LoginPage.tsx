import { useNavigate } from "react-router-dom";
import { LoginCredentials } from "../../features/Users/Users.types";
import LoginForm from "../../features/Users/Login/LoginForm";

import styles from './loginPage.module.css'
import Logo from "../../components/Text/Logo";
import { useAuth } from "../../components/Authentication/AuthenticationProvider";

export default function LoginPage() {
    const {login} = useAuth()
    const navigate = useNavigate()

    const handleLogin = (credentials: LoginCredentials) => {
        login()
        navigate('/')
    } 

    return (
        <div id={styles.login}>
            <div id={styles['login-logo']}>
                <Logo color="dark"/>
            </div>
            <LoginForm onLogin={handleLogin}/>
        </div>
    )
}