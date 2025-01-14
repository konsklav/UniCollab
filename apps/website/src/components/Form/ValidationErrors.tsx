import ValidationError from "./ValidationError"

interface ValidationErrorsProps {
    messages: readonly string[]
}

export default function ValidationErrors({messages}: ValidationErrorsProps) {
    return <>
        {messages.length > 0 && 
        <ul>
            {messages.map(message =>(
                <li>
                    <ValidationError message={message}/>
                </li>
            ))}
        </ul>}
    </>
}