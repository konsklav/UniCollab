import { FormEvent, useState } from "react";
import InputText from "../../../components/Form/InputText";
import { UserCredentials } from "../Users.types";

import '../userForms.css'
import InputPassword from "../../../components/Form/InputPassword";

interface LoginFormProps {
    onLogin: (credentials: UserCredentials) => void
}

export default function LoginForm({onLogin}: LoginFormProps) {
    const [credentials, setCredentials] = useState<UserCredentials>({
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
        <form className="user-form" name="login-form" onSubmit={handleSubmit} aria-label="login-form">
            <InputText value={credentials.username} onChange={setUsername} label="Username" />
            <InputPassword value={credentials.password} onChange={setPassword} label="Password" />
            <button className="btn btn-primary mt-4" type="submit">Sign In</button>
        </form>
    )
}