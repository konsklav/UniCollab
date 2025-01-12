export interface InputProps<T> {
    value: T
    onChange: (value: T) => void
    placeholder?: string
    label?: string    
    id?: string
}