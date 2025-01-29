import { ChatRoomInformation, CreateChatRoomRequest } from "../features/Chat/Chat.types";
import { UserInformation } from "../features/Users/Users.types";
import { api } from "../services/apiService";

export const getJoinableChats = async (user: UserInformation): Promise<readonly ChatRoomInformation[]> => {
    const response = await api.get(`/chat/join/${user.id}`)
    return response.data
}

export const joinChatRoom = async (chatId: string, user: UserInformation): Promise<void> => {
    const response = await api.put(`/chat/${chatId}`, user)
    return response.data
}

export const createChatRoom = async (request: CreateChatRoomRequest) => {
    const response = await api.post('/chat', request)
    return response.data
}