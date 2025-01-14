import { FormEvent, useState } from "react";
import InputText from "../../components/Form/InputText";
import { LoginCredentials } from "./Login.types";

import styles from './LoginForm.module.css'

interface LoginFormProps {
    onLogin: (credentials: LoginCredentials) => void
}

export default function LoginForm({onLogin}: LoginFormProps) {
    const [credentials, setCredentials] = useState<LoginCredentials>({
        username: '',
        password: ''
    });

    const setUsername = (username: string) => setCredentials({...credentials, username: username})
    const setPassword = (password: string) => setCredentials({...credentials, password: password})

    function handleSubmit(e: FormEvent<HTMLFormElement>): void {
        e.preventDefault()
        onLogin(credentials)
    }

    return (
        <form id={styles['login-form']} name="login-form" onSubmit={handleSubmit} aria-label="login-form">
            <InputText value={credentials.username} onChange={setUsername} label="Username" />
            <InputText value={credentials.password} onChange={setPassword} label="Password" />
            <button className="btn btn-primary mt-4" type="submit">Sign In</button>
        </form>
    )
}