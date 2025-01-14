export interface InputProps<T> {
    value: T
    onChange: (value: T) => void
    placeholder?: string
    label?: string    
    id?: string
}

export interface PasswordInputProps extends InputProps<string> {
    isVisible?: boolean 
}