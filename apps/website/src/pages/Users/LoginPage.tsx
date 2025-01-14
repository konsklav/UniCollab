import { NavLink, useNavigate } from "react-router-dom";
import { UserCredentials } from "../../features/Users/Users.types";
import LoginForm from "../../features/Users/Login/LoginForm";

import { useAuth } from "../../components/Authentication/AuthenticationProvider";

export default function LoginPage() {
    const {login} = useAuth()
    const navigate = useNavigate()

    const handleLogin = (credentials: UserCredentials) => {
        login()
        navigate('/')
    } 

    return <div>
        <LoginForm onLogin={handleLogin}/>
        <div>
            To create an account, <NavLink to='/register'>sign up</NavLink>.
        </div>
    </div>
}