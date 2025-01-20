import axios from "axios"
import { User } from "../common/common.types"
import { commonUrls } from "../common/common.urls"

const api = axios.create({
    baseURL: commonUrls.api,
    headers: {
        'Content-Type': 'application/json'
    }
})

export const getAllUsers = async (): Promise<User[]> => {
    const response = await api.get('/users')
    return response.data
}