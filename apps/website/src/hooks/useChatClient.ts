import { useEffect, useRef } from "react"
import { useAuth } from "../state/authentication/authenticationStore"
import SignalRService from "../services/signalRService"

type ChatClientCallbacks = {
    onMessageReceived: (message: ClientMessage) => void
}

type ChatClientActions = {
    sendMessage: (message: string) => void
}

type ServerMessage = {
    chatId: string
    userId: string
    content: string
}

export type ClientMessage = {
    username: string,
    content: string
}

export const useChatClient = (chatId: string, callbacks: ChatClientCallbacks): ChatClientActions => {
    const {credentials: user, isAuthenticated} = useAuth()
    const signalRRef = useRef<SignalRService | undefined>(undefined)

    useEffect(() => {
        if (!user || !isAuthenticated()) {
            return;
        }

        const signalR = new SignalRService('chat', user)
        signalRRef.current = signalR

        signalR.startConnection().then(() => {
            signalR.send('JoinChat', chatId)
        })

        signalR.on('ReceiveMessage', (message: ClientMessage) => {
            console.log(`Received message ${message}`)
            callbacks.onMessageReceived(message)
        })

        return () => signalR.stopConnection()
    }, [user])

    return {
        sendMessage: (message: string) => {
            const signalR = signalRRef.current
            if (!signalR || !user)
                return;

            const cleanMessage = message.trim()
            if (cleanMessage === '')
                return;

            const serverMessage: ServerMessage = {
                chatId: chatId,
                userId: user.username,
                content: cleanMessage
            }

            signalR.send('SendMessage', serverMessage)
        }
    }
}