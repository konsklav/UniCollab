import { User } from "../common/common.types"
import { PostInformation } from "../features/Posts/Posts.types"
import { AxiosWrapper } from "./axiosWrapper"

const api = new AxiosWrapper()

export const getAllUsers = async (): Promise<User[]> => {
    const response = await api.get('/users')
    return response.data
}

export const getPostBySlug = async (slug: string): Promise<PostInformation> => {
    const response = await api.get(`/posts/${slug}`)
    return response.data
}

export const getRecentPosts = async (count: number): Promise<readonly PostInformation[]> => {
    if (count <= 0) {
        throw new Error('Invalid count parameter.')
    }

    const response = await api.get(`/posts/recent/${count}`)
    return response.data
}