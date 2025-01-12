export interface InputProps<T> {
    onChange: (value: T) => void
    placeholder?: string
    label?: string    
    id?: string
}