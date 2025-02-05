import { RichUserInformation, UserInformation } from "../features/Users/Users.types"
import { api } from "../services/apiService"

export const getAllUsers = async (userId: string): Promise<readonly RichUserInformation[]> => {
    const response = await api.get(`/users?metadata=friend&target=${userId}`)
    return response.data
}

export const getFriends = async (userId: string): Promise<readonly UserInformation[]> => {
    const response = await api.get(`/users/${userId}/friends`)
    return response.data
}

export const addFriend = async (userId: string, friendId: string) => 
    await api.post(`/users/${userId}/friends/${friendId}`)

export const removeFriend = async (userId: string, friendId: string) =>
    await api.delete(`/users/${userId}/friends/${friendId}`)