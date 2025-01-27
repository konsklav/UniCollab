import { UserInformation } from "../features/Users/Users.types"

export interface ChildrenProps {
    children: React.ReactNode
}

// API Contracts
export interface AuthenticatedUser {
    user: UserInformation
    token: string
}