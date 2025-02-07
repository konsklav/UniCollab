import { UserInformation } from "../Users/Users.types"

type GroupCreator = UserInformation | undefined

export interface GroupDto {
    id: string
    name: string
    members: readonly UserInformation[]
    creator: GroupCreator
}

export interface GroupInformationDto {
    id: string
    name: string
    memberCount: number
    creator: GroupCreator
}

export interface DetailedGroupInformationDto {
    info: GroupInformationDto
    friendsInGroup: readonly UserInformation[]
}

export interface CreateGroupRequest {
    name: string,
    initialMembers: readonly string[]
}