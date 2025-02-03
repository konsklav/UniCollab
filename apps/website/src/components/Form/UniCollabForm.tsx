import React, { createContext, FormEvent, useContext, useState } from "react"

type FormProps = {
    children: React.ReactNode
    className?: string | undefined,
    name?: string | undefined
    onSubmit: () => Promise<void>
}

type FormState = 'submitting' | 'idle'

type FormContextType = {
    state: FormState
}

const FormContext = createContext<FormContextType | undefined>(undefined)

export const useForm = () => {
    const context = useContext(FormContext)
    if (!context)
        throw new Error('useForm() needs to be used inside a component that is wrapped in a <Form> component.')

    return context
}

export function UniCollabForm({children, className, name, onSubmit}: FormProps) {
    const [state, setState] = useState<FormState>('idle')

    const handleSubmit = (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault()
        
        setState('submitting')
        onSubmit()
            .finally(() => setState('idle'))
    }

    return (
        <FormContext.Provider value={{state}}>
            <form className={className} name={name} onSubmit={handleSubmit}>
                {children}
            </form>
        </FormContext.Provider>
    )
}

