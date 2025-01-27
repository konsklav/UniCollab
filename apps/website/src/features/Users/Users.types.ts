import { UUIDTypes } from "uuid"

export interface UserCredentials {
    username: string
    password: string
}

export interface UserInformation {
    id: string
    username: string
}

export interface User {
    id: UUIDTypes
    username: string
    friends: UserInformation[]
    postSlugs: string[]
}