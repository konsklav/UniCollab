import { useState } from "react"
import { UniCollabForm } from "../../../components/Form/UniCollabForm"
import InputText from "../../../components/Form/InputText"
import { SubmitButton } from "../../../components/Button"

interface ChatInputProps {
    onSend: (content: string) => void
}

export default function ChatInput({onSend}: ChatInputProps) {
    const [message, setMessage] = useState('')

    const handleSend = (): Promise<void> => {
        return new Promise((resolve) => resolve(onSend(message)))
    }
    
    return (
        <UniCollabForm onSubmit={handleSend} name="message">
            <div className="d-flex gap-2">
                <InputText 
                    value={message}
                    onChange={msg => setMessage(msg)}
                    placeholder="Type your message..."/>
                <SubmitButton color={'secondary'} loadingText="...">Send</SubmitButton>
            </div>
        </UniCollabForm>
    )
}