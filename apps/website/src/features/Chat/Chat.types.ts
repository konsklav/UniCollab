import { UserInformation } from "../Users/Users.types"

export interface ChatRoomInformation {
    id: string,
    name: string,
    participantCount: number,
    lastMessageSent: MessageDto | undefined
}

export interface MessageDto {
    id: string,
    content: string,
    sentAt: string,
    sender: UserInformation
}

export type ChatClientCallbacks = {
    onMessageReceived?: (message: MessageDto) => void
    onInitialized?: () => void
    onJoinError?: () => void
}

export type ChatClientActions = {
    sendMessage: (message: string) => void
}

export type ServerMessage = {
    chatId: string
    userId: string
    content: string
}

export interface CreateChatRoomRequest {
    name: string
    initialParticipants: readonly string[]
}