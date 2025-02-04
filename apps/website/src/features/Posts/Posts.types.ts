import { UserInformation } from "../Users/Users.types"

export interface PostInformation {
    id: string,
    title: string,
    content: string
    author: UserInformation,
    subjects: readonly string[]
    uploadDate: Date
    slug: string
}

export interface CreatePostRequest {
    title: string
    content: string
    subjects: readonly string[]
}