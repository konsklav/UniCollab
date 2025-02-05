import { NavLink } from "react-router-dom";
import LoginForm from "../../features/Users/Login/LoginForm";

import { UserCredentials } from "../../features/Users/Users.types";
import { useState } from "react";
import { useLogin } from "../../hooks/useLogin";
import { CredentialResponse, GoogleLogin, GoogleOAuthProvider } from "@react-oauth/google";

export default function LoginPage() {
    const [validationMessages, setValidationMessages] = useState<readonly string[]>([])
    const {basicLogin, googleLogin} = useLogin(() => {
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
            await basicLogin(user)
        }
    }

    const handleGoogleSuccess = async (response: CredentialResponse) => {
        await googleLogin(response)
    }

    const handleGoogleFailure = async () => {
        console.log('ERROR OH SHIT!')
    }
    
    return <div>
        <LoginForm onLogin={handleLogin}/>
        {validationMessages.map(message => (
            <div key={message} className="text-danger">{message}</div>
        ))}
        <div className="mt-2">
            <GoogleOAuthProvider clientId={'993988601504-kva11gp8kea0t764esg6ga76pgqstap7.apps.googleusercontent.com'}>
                <GoogleLogin 
                    locale=""
                    text={'signin_with'}
                    onSuccess={handleGoogleSuccess}
                    onError={handleGoogleFailure}/>
            </GoogleOAuthProvider>
        </div>
        <div>
            To create an account, <NavLink to='/register'>sign up</NavLink>.
        </div>
        
    </div>
}