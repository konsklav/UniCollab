import { RichUserInformation, UserInformation } from "../features/Users/Users.types"
import { api } from "../services/apiService"

export const getAllUsers = async (): Promise<readonly UserInformation[]> => {
    const response = await api.get('/users')
    return response.data
}

export const getAllUsersRich = async (userId: string): Promise<readonly RichUserInformation[]> => {
    const response = await api.get(`/users?info=detail&target=${userId}`)
    return response.data
}

export const getFriends = async (userId: string): Promise<readonly UserInformation[]> => {
    const response = await api.get(`/users/${userId}/friends`)
    return response.data
}

export const addFriend = async (userId: string, friendId: string): Promise<void> => 
    (await api.post(`/users/${userId}/friends/${friendId}`)).data

export const removeFriend = async (userId: string, friendId: string): Promise<void> =>
    (await api.delete(`/users/${userId}/friends/${friendId}`)).data