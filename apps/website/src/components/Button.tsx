import { useState } from "react"
import { Color } from "./Components.types"
import { useForm } from "./Form/UniCollabForm"

type CommonButtonProps = {
    children: React.ReactNode
    loadingText?: string
    color?: Color

    className?: string | undefined
}
type ButtonState = 'loading' | 'idle'
type CoreButtonProps = CommonButtonProps & {
    onClick?: () => void
    type: 'submit' | 'button'
    state: ButtonState
}
type ButtonProps = CommonButtonProps & { onClick: () => Promise<void> }

export function Button(props: ButtonProps) {
    const [state, setState] = useState<ButtonState>('idle')

    const handleClick = () => {
        setState('loading')
        props.onClick()
            .finally(() => setState('idle'))
    }

    return <CoreButton {...props} onClick={handleClick} type={'button'} state={state}/>
}
export function SubmitButton(props: CommonButtonProps) {
    const formContext = useForm()

    const getState = (): ButtonState => formContext.state === 'submitting' ? 'loading' : 'idle'

    return <CoreButton {...props} state={getState()} type={'submit'} />
}

function CoreButton({
    onClick, 
    children, 
    type, 
    state, 
    className,
    loadingText = 'Loading...', 
    color = 'primary'}: CoreButtonProps) {
    return (
        <button type={type} className={`btn btn-${color} ${className}`} onClick={onClick} disabled={state === 'loading'}>
            {state === 'idle' && children}
            {state === 'loading' && <>
                <span className="spinner-border spinner-border-sm" aria-hidden="true"></span>
                <span role="status">{loadingText}</span>
            </>}
        </button>
    )
}
