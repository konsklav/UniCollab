import { NavLink } from "react-router-dom";
import RegisterForm from "../../features/Users/Register/RegisterForm";

export default function RegisterPage() {

    const handleRegister = () => {
        
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