interface ValidationErrorProps {
    message: string
}

export default function ValidationError({message}: ValidationErrorProps) {
    return <div className="text-danger">{message}</div>
}