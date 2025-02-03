import { NavLink, useNavigate } from "react-router-dom";
import RegisterForm from "../../features/Users/Register/RegisterForm";
import { useMutation } from "@tanstack/react-query";
import { RegisterRequest } from "../../features/Users/Register/Register.types";
import { basicRegister } from "../../endpoints/authEndpoints";
import { useState } from "react";

export default function RegisterPage() {
    const [error, setError] = useState<string | undefined>()
    const mutation = useMutation({
        mutationFn: (request: RegisterRequest) => basicRegister(request) 
    })

    const navigate = useNavigate()

    const handleRegister = async (request: RegisterRequest) => {
        await mutation.mutateAsync(request, {
            onSuccess: () => navigate('/login'),
            onError: (err, _v, _c) => setError(err.message)
        })
    }

    return (
        <div>
            <RegisterForm onValidRegister={handleRegister} error={error}/>
            <div>
                Already have an account? <NavLink to='/login'>Login</NavLink>
            </div>
        </div>
    )
}