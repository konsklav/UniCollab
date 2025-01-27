export interface ChatRoomInformation {
    id: string,
    name: string,
    participantCount: number
}

export type ChatClientCallbacks = {
    onMessageReceived: (message: ClientMessage) => void
}

export type ChatClientActions = {
    sendMessage: (message: string) => void
}

export type ServerMessage = {
    chatId: string
    userId: string
    content: string
}

export type ClientMessage = {
    username: string,
    content: string
}

export interface CreateChatRoomRequest {
    name: string
    initialParticipants: readonly string[]
}