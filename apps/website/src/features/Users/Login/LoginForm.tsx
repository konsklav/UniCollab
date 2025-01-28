import { useState } from "react";
import InputText from "../../../components/Form/InputText";
import { UserCredentials } from "../Users.types";

import '../userForms.css'
import InputPassword from "../../../components/Form/InputPassword";
import { UniCollabForm } from "../../../components/Form/UniCollabForm";
import { SubmitButton } from "../../../components/Button";

interface LoginFormProps {
    onLogin: (credentials: UserCredentials) => Promise<void>
}

export default function LoginForm({onLogin}: LoginFormProps) {
    const [credentials, setCredentials] = useState<UserCredentials>({
        username: '',
        password: ''
    });

    const setUsername = (username: string) => setCredentials({...credentials, username: username})
    const setPassword = (password: string) => setCredentials({...credentials, password: password})

    const handleSubmit = async () => await onLogin(credentials)

    return (
        <UniCollabForm className="user-form" name="login-form" onSubmit={handleSubmit}>
            <InputText value={credentials.username} onChange={setUsername} label="Username" />
            <InputPassword value={credentials.password} onChange={setPassword} label="Password" />
            
            <SubmitButton className="mt-4" loadingText="Signing In...">Sign In</SubmitButton>
        </UniCollabForm>
    )
}