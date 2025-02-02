import { ChatRoomInformation, CreateChatRoomRequest } from "../features/Chat/Chat.types";
import { UserInformation } from "../features/Users/Users.types";
import { api } from "../services/apiService";

export const userChats = (userId: string) => `/users/${userId}/chats`

export const getJoinableChats = async (user: UserInformation): Promise<readonly ChatRoomInformation[]> => {
    const response = await api.get(`${userChats(user.id)}?type=joinable`)
    return response.data
}

export const getParticipatingChats = async (user: UserInformation): Promise<readonly ChatRoomInformation[]> => {
    const response = await api.get(`${userChats(user.id)}?type=participating`)
    return response.data
}

export const joinChatRoom = async (chatId: string, user: UserInformation): Promise<void> => {
    const response = await api.post(`${userChats(user.id)}/${chatId}`)
    return response.data
}

export const createChatRoom = async (request: CreateChatRoomRequest) => {
    const response = await api.post('/chat', request)
    return response.data
}