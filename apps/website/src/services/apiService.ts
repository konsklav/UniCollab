import axios from "axios"
import { User } from "../common/common.types"
import { commonUrls } from "../common/common.urls"
import { PostInformation } from "../features/Posts/Posts.types"

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

export const getPostBySlug = async (slug: string): Promise<PostInformation> => {
    const response = await api.get(`/posts/${slug}`)
    return response.data
}

