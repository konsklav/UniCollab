import { NavLink } from "react-router-dom";
import LoginForm from "../../features/Users/Login/LoginForm";

import { UserCredentials } from "../../features/Users/Users.types";
import { useState } from "react";
import { useLogin } from "../../hooks/useLogin";
import { Button } from "../../components/Button";

export default function LoginPage() {
    const [validationMessages, setValidationMessages] = useState<readonly string[]>([])
    const login = useLogin(() => {
        setValidationMessages(['Incorrect username or password.'])
    })

    const handleLogin = async (user: UserCredentials) => {
        let isValid = true
        setValidationMessages([])

        if (user.username.trim().length === 0) {
            setValidationMessages(msg => [...msg, 'Username cannot be empty.'])
            isValid = false
        }
        if (user.password.trim().length === 0) {
            setValidationMessages(msg => [...msg, 'Password cannot be empty.'])
            isValid = false
        }

        if (isValid) {
            await login(user)
        }
    }

    const handleGoogleLogin = async () => {
    }
    
    return <div>
        <LoginForm onLogin={handleLogin}/>
        {validationMessages.map(message => (
            <div key={message} className="text-danger">{message}</div>
        ))}
        <div className="mt-2">
            <Button color={'danger'} onClick={handleGoogleLogin}>Login with Google</Button>
        </div>
        <div>
            To create an account, <NavLink to='/register'>sign up</NavLink>.
        </div>
        
    </div>
}