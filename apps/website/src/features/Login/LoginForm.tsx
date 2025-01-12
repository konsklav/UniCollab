import { FormEvent, useState } from "react";
import InputText from "../../components/Form/InputText";
import { LoginCredentials } from "./Login.types";

export default function LoginForm() {
    const [credentials, setCredentials] = useState<LoginCredentials>({
        username: '',
        password: ''
    });

    const setUsername = (username: string) => setCredentials({...credentials, username: username})
    const setPassword = (password: string) => setCredentials({...credentials, password: password})

    function handleSubmit(e: FormEvent<HTMLFormElement>): void {
        e.preventDefault()

        console.log(credentials)
    }

    return (
        <form onSubmit={handleSubmit}>
            <InputText value={credentials.username} onChange={setUsername} label="Username" />
            <InputText value={credentials.password} onChange={setPassword} label="Password" />
            <button className="btn btn-primary" type="submit">Sign In</button>
        </form>
    )
}