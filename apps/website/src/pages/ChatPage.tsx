import { FormEvent, useState } from "react"
import InputText from "../components/Form/InputText";
import { ClientMessage, useChatClient } from "../hooks/useChatClient";
import ChatMessage from "../features/Chat/ChatMessage";


export default function ChatPage() {
    const [message, setMessage] = useState('')
    const [messages, setMessages] = useState<ClientMessage[]>([])
    const chat = useChatClient('abc123', {
        onMessageReceived: (message) => setMessages(current => [...current, message])
    })

    const handleMessageSubmit = (e: FormEvent<HTMLFormElement>): void => {
        e.preventDefault()
        chat.sendMessage(message)
    }

    return (
        <>
        <div className="container border p-3" style={{minHeight: '2rem'}}>
            {messages.map(msg => <ChatMessage message={msg}/>)}
        </div>

        <form onSubmit={handleMessageSubmit}>
            <InputText value={message} onChange={(newMsg) => setMessage(newMsg)}/>
            <button type="submit" className="btn btn-primary">Send</button>
        </form>
        </>
    )
}