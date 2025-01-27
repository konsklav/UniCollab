import { UserInformation } from "../features/Users/Users.types"
import { api } from "../services/apiService"

export const getAllUsers = async (): Promise<readonly UserInformation[]> => {
    const response = await api.get('/users')
    return response.data
}