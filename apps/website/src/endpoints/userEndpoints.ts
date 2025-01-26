import { User } from "../common/common.types"
import { api } from "../services/apiService"

export const getAllUsers = async (): Promise<readonly User[]> => {
    const response = await api.get('/users')
    return response.data
}