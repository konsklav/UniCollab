import { AuthenticatedUser } from "../common/common.types"
import { RegisterRequest } from "../features/Users/Register/Register.types"
import { UserCredentials } from "../features/Users/Users.types"
import { api } from "../services/apiService"

export const basicLogin = async (user: UserCredentials): Promise<AuthenticatedUser> => {
    const response = await api.post('/auth/login/basic', user)
    return response.data
}

export const basicRegister = async (request: RegisterRequest): Promise<void> => {
    await api.post('/auth/register', request)
}