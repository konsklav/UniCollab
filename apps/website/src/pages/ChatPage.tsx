import { FormEvent, useEffect, useRef, useState } from "react"
import { useAuth } from "../state/authentication/authenticationStore"
import SignalRService from "../services/signalRService";
import InputText from "../components/Form/InputText";

const tempId: string = 'abc123'

type ServerMessage = {
    chatId: string
    userId: string
    content: string
}

export default function ChatPage() {
    const {user, isAuthenticated} = useAuth()
    const [message, setMessage] = useState('')
    const [messages, setMessages] = useState<string[]>([])
    const signalRRef = useRef<SignalRService | undefined>(undefined)

    useEffect(() => {
        if (!user || !isAuthenticated()) {
            return;
        }

        const signalR = new SignalRService('chat', user)
        signalRRef.current = signalR

        signalR.startConnection().then(() => {
            signalR.send('JoinChat', tempId)
        })

        signalR.on('ReceiveMessage', (username, message) => {
            console.log(`Received message ${message}`)
            setMessages(msg => [...msg, `[${new Date().toLocaleTimeString()}] ${username}: ${message}`])
        })

        return () => signalR.stopConnection()
    }, [user])

    const handleMessageSubmit = (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault()

        console.log('submitting...')
        const signalR = signalRRef.current
        if (!signalR)
            return;

        const cleanMessage = message.trim()
        if (cleanMessage === '') 
            return;

        console.log('Sending Message')

        const msg: ServerMessage = {
            chatId: tempId,
            userId: tempId,
            content: cleanMessage
        }

        signalR.send('SendMessage', msg)
            .then(() => console.log('Message sent.'))
    }

    return (
        <>
        <div className="container border p-3" style={{minHeight: '2rem'}}>
            {messages.map(msg => (
                <div className="p-1">{msg}</div>
            ))}
        </div>

        <form onSubmit={handleMessageSubmit}>
            <InputText value={message} onChange={(newMsg) => setMessage(newMsg)}/>
            <button type="submit" className="btn btn-primary">Send</button>
        </form>
        </>
    )
}