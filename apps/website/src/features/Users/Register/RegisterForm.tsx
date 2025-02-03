import { useState } from 'react'
import { UserCredentials } from '../Users.types'
import InputText from '../../../components/Form/InputText'
import InputPassword from '../../../components/Form/InputPassword'

import '../userForms.css'
import ValidationError from '../../../components/Form/ValidationError'
import { SubmitButton } from '../../../components/Button'
import { UniCollabForm } from '../../../components/Form/UniCollabForm'

interface RegisterFormProps {
    onValidRegister: (registerData: UserCredentials) => Promise<void>
    error?: string | undefined
}

interface RegisterData extends UserCredentials {
    verifyPassword: string
}

export default function RegisterForm({onValidRegister, error}: RegisterFormProps) {
    const [registerData, setRegisterData] = useState<RegisterData>({
        username: '',
        password: '',
        verifyPassword: ''
    })
    const [validationError, setValidationError] = useState<string | undefined>(error)

    const setUsername = (username: string) => setRegisterData({...registerData, username: username})
    const setPassword = (password: string) => setRegisterData({...registerData, password: password})
    const setVerifyPassword = (password: string) => setRegisterData({...registerData, verifyPassword: password})

    const handleRegister = async () => {
        if (registerData.password !== registerData.verifyPassword)
        {
            setValidationError('Passwords do not match.')
            return;
        }

        onValidRegister(registerData)
    }

    return (
        <div>
            <UniCollabForm className='user-form' onSubmit={handleRegister}>
                <InputText value={registerData.username} onChange={setUsername} label='Username'/>
                <InputPassword value={registerData.password} onChange={setPassword} label='Password'/>
                <InputPassword value={registerData.verifyPassword} onChange={setVerifyPassword} label='Verify Password'/>
                <SubmitButton color={'primary'}>Sign Up</SubmitButton>
            </UniCollabForm>
            {validationError && <ValidationError message={validationError}/>}
        </div>
    )
}