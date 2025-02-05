import { UUIDTypes } from "uuid"
import { ChatRoomInformation } from "../Chat/Chat.types"

export interface UserCredentials {
    username: string
    password: string
}

export interface UserInformation {
    id: string
    username: string
}

export interface RichUserInformation {
    user: UserInformation
    isFriend: boolean
    mutualFriends: readonly UserInformation[]
    mutualChats: readonly ChatRoomInformation[]
    totalPostsUploaded: number
    postsPerSubject: readonly SubjectCount[]
}

type SubjectCount = {name: string, count: number}

export interface User {
    id: UUIDTypes
    username: string
    friends: UserInformation[]
    postSlugs: string[]
}