import { useEffect, useRef } from "react"
import { useAuth } from "../state/authentication/authenticationStore"
import SignalRService from "../services/signalRService"
import { ChatClientCallbacks, ChatClientActions, ClientMessage, ServerMessage } from "../features/Chat/Chat.types"

export const useChatClient = (chatId: string, callbacks: ChatClientCallbacks): ChatClientActions => {
    const {user, isAuthenticated} = useAuth()
    const signalRRef = useRef<SignalRService | undefined>(undefined)

    useEffect(() => {
        if (!user || !isAuthenticated()) 
            return;

        const signalR = new SignalRService('chat')
        signalRRef.current = signalR

        signalR.startConnection()
        .then(() => {
            signalR.send('JoinChat', chatId)
            .then(() => callbacks.onInitialized?.())
            .catch(() => callbacks.onJoinError?.())
        })

        signalR.on('ReceiveMessage', (message: ClientMessage) => {
            console.log(`Received message ${message}`)
            callbacks.onMessageReceived?.(message)
        })

        return () => signalR.stopConnection()
    }, [])

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