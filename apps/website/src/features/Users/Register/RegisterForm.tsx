import { useState } from 'react'
import '../userForms.css'
import { RegisterProperties } from '../Users.types'

interface RegisterFormProps {
    onRegister: (registerData: RegisterProperties) => void
}

export default function RegisterForm({onRegister}: RegisterFormProps) {
    const [registerData, setRegisterData] = useState<RegisterProperties>({
        username: '',
        password: '',
        verifyPassword: ''
    })
}