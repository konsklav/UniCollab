import { useEffect, useRef, useState } from "react"
import { useAuth } from "../state/authentication/authenticationStore"
import SignalRService from "../services/signalRService"
import { ChatClientCallbacks, ChatClientActions, ServerMessage, MessageDto } from "../features/Chat/Chat.types"

export const useChatClient = (callbacks: ChatClientCallbacks): ChatClientActions => {
    const {user, isAuthenticated} = useAuth()
    const signalRRef = useRef<SignalRService | undefined>(undefined)

    const [currentChatId, setCurrentChatId] = useState<string>()

    const switchChat = async (chatId: string) => {
        const signalR = signalRRef.current
        if (!signalR || !user) 
            return;

        if (currentChatId)
            await signalR.send('LeaveChat', currentChatId, user.id)

        await signalR.send('JoinChat', chatId, user.id)
        setCurrentChatId(chatId)
    }

    const handleReconnect = () => {
        console.log(`Reconnecting to chat '${currentChatId}'`)
        if (currentChatId) {
            switchChat(currentChatId)
        }
    }

    useEffect(() => {
        if (!user || !isAuthenticated()) 
            return;

        const signalR = new SignalRService('chat', handleReconnect)
        signalRRef.current = signalR

        signalR.startConnection()
        .then(() => callbacks.onInitialized?.())

        signalR.on('ReceiveMessage', (message: MessageDto) => {
            console.log(`Received message ${message}`)
            callbacks.onMessageReceived?.(message)
        })

        return () => signalR.stopConnection()
    }, [])

    return {
        switchChat,
        leaveChat: async () => {
            const signalR = signalRRef.current
            if (!signalR || !user) 
                return;

            if (currentChatId) {                
                await signalR.send('LeaveChat', currentChatId, user.id)
                setCurrentChatId(undefined)
            }
        },
        sendMessage: (message: string) => {
            const signalR = signalRRef.current
            if (!signalR || !user || !currentChatId)
                return;

            const cleanMessage = message.trim()
            if (cleanMessage === '')
                return;

            const serverMessage: ServerMessage = {
                chatId: currentChatId,
                userId: user.id,
                content: cleanMessage
            }

            signalR.send('SendMessage', serverMessage)
        }
    }
}