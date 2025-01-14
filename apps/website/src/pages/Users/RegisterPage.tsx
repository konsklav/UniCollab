import RegisterForm from "../../features/Users/Register/RegisterForm";
import { RegisterProperties } from "../../features/Users/Users.types";

export default function RegisterPage() {

    const handleRegister = (registerData: RegisterProperties) => {
        
    }

    return <RegisterForm onRegister={handleRegister}/>
}