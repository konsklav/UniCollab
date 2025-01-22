import { UserInformation } from "../Users/Users.types"

export interface PostInformation {
    title: string,
    content: string
    author: UserInformation,
    uploadDate: Date
    slug: string
}