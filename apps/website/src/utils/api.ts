import axios from "axios";
import { commonUrls } from "../common/common.urls";
import { User } from "../common/common.types";

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