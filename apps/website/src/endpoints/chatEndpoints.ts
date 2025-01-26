import { ChatRoomInformation } from "../features/Chat/Chat.types";
import { api } from "../services/apiService";
import { useAuth } from "../state/authentication/authenticationStore";

export const getJoinableChats = async (): Promise<readonly ChatRoomInformation[]> => {
    const {user} = useAuth.getState()

    const response = await api.get(`/chat/joinable/${user?.id}`)
    return response.data
}

export const joinChatRoom = async (chatId: string): Promise<void> => {
    const {user} = useAuth.getState()

    const response = await api.post(`/chat/${chatId}`, user)
    return response.data
}