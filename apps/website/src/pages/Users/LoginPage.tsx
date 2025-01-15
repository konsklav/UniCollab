import { NavLink, useNavigate } from "react-router-dom";
import LoginForm from "../../features/Users/Login/LoginForm";

import { useAuth } from "../../components/Authentication/AuthenticationProvider";

export default function LoginPage() {
    const {login} = useAuth()
    const navigate = useNavigate()

    const handleLogin = () => {
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