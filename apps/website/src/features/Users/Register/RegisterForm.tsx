import { FormEvent, useState } from 'react'
import { UserCredentials } from '../Users.types'
import InputText from '../../../components/Form/InputText'
import InputPassword from '../../../components/Form/InputPassword'

import '../userForms.css'
import ValidationError from '../../../components/Form/ValidationError'

interface RegisterFormProps {
    onRegister: (registerData: UserCredentials) => void
}

interface RegisterData extends UserCredentials {
    verifyPassword: string
}

export default function RegisterForm({onRegister}: RegisterFormProps) {
    const [registerData, setRegisterData] = useState<RegisterData>({
        username: '',
        password: '',
        verifyPassword: ''
    })
    const [validationError, setValidationError] = useState<string | undefined>(undefined)

    const setUsername = (username: string) => setRegisterData({...registerData, username: username})
    const setPassword = (password: string) => setRegisterData({...registerData, password: password})
    const setVerifyPassword = (password: string) => setRegisterData({...registerData, verifyPassword: password})

    const handleRegister = (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault()

        if (registerData.password !== registerData.verifyPassword)
        {
            setValidationError('Passwords do not match.')
            return;
        }

        onRegister(registerData)
    }

    return (
        <div>
            <form className='user-form' onSubmit={handleRegister}>
                <InputText value={registerData.username} onChange={setUsername} label='Username'/>
                <InputPassword value={registerData.password} onChange={setPassword} label='Password'/>
                <InputPassword value={registerData.verifyPassword} onChange={setVerifyPassword} label='Verify Password'/>
                <button type='submit' className='btn btn-primary'>Sign Up</button>
            </form>
            {validationError && <ValidationError message={validationError}/>}
        </div>
    )
}