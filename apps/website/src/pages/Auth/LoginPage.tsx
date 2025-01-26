import { NavLink, useNavigate } from "react-router-dom";
import LoginForm from "../../features/Users/Login/LoginForm";

import { useAuth } from "../../state/authentication/authenticationStore";
import { UserCredentials } from "../../features/Users/Users.types";
import { useState } from "react";

export default function LoginPage() {
    const [validationMessages, setValidationMessages] = useState<readonly string[]>([])
    const {login} = useAuth()
    const navigate = useNavigate()

    const handleLogin = (user: UserCredentials) => {
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
            login(user, 'Basic')
            navigate('/')
        }
    } 

    return <div>
        <LoginForm onLogin={handleLogin}/>
        {validationMessages.map(message => (
            <div className="text-danger">{message}</div>
        ))}
        <div>
            To create an account, <NavLink to='/register'>sign up</NavLink>.
        </div>
    </div>
}