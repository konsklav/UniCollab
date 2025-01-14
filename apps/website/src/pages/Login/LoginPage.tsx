import { redirect } from "react-router-dom";
import { LoginCredentials } from "../../features/Login/Login.types";
import LoginForm from "../../features/Login/LoginForm";

import styles from './loginPage.module.css'

export default function LoginPage() {

    const handleLogin = (credentials: LoginCredentials) => {
        redirect('/')
    } 

    return (
        <div id={styles.login}>
            <LoginForm onLogin={handleLogin}/>
        </div>
    )
}