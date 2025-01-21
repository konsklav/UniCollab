import { NavLink, useNavigate } from "react-router-dom";
import LoginForm from "../../features/Users/Login/LoginForm";

import { useAuth } from "../../state/authentication/authenticationStore";
import { UserCredentials } from "../../features/Users/Users.types";

export default function LoginPage() {
    const {login} = useAuth()
    const navigate = useNavigate()

    const handleLogin = (user: UserCredentials) => {
        login(user)
        navigate('/')
    } 

    return <div>
        <LoginForm onLogin={handleLogin}/>
        <div>
            To create an account, <NavLink to='/register'>sign up</NavLink>.
        </div>
    </div>
}