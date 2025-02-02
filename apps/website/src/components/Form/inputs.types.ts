export interface InputProps<T> {
    value: T
    onChange: (value: T) => void
    placeholder?: string
    label?: string    
    id?: string
    className?: string
}

export interface PasswordInputProps extends InputProps<string> {
    isVisible?: boolean 
}