import { AuthenticatedUser } from "../common/common.types"
import { UserCredentials } from "../features/Users/Users.types"
import { api } from "../services/apiService"

export const basicLogin = async (user: UserCredentials): Promise<AuthenticatedUser> => {
    const response = await api.post('/auth/login/basic', user)
    return response.data
}