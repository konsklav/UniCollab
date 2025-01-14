import { NavLink } from "react-router-dom";
import RegisterForm from "../../features/Users/Register/RegisterForm";
import { UserCredentials } from "../../features/Users/Users.types";

export default function RegisterPage() {

    const handleRegister = (registerData: UserCredentials) => {
        
    }

    return (
        <div>
            <RegisterForm onValidRegister={handleRegister}/>
            <div>
                Already have an account? <NavLink to='/login'>Login</NavLink>
            </div>
        </div>
    )
}