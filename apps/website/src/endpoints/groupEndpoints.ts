import { CreateGroupRequest, GroupDto, GroupInformationDto } from "../features/Groups/Group.types"
import { api } from "../services/apiService"

export const getGroupById = async (groupId: string): Promise<GroupDto> => {
    const response = await api.get(`/groups/${groupId}`)
    return response.data
} 

export const getUserGroups = async (userId: string): Promise<readonly GroupInformationDto[]> => {
    const response = await api.get(`/users/${userId}/groups?type=participating`)
    return response.data
}

export const getJoinableGroups = async (userId: string): Promise<readonly GroupInformationDto[]> => {
    const response = await api.get(`/users/${userId}/groups?type=joinable`)
    return response.data
}

export const createGroup = async (request: CreateGroupRequest) => 
    await api.post(`/groups`, request)

export const joinGroup = async (userId: string, groupId: string) => 
    await api.post(`/users/${userId}/groups/${groupId}`)

export const leaveGroup = async (userId: string, groupId: string) => 
    await api.delete(`/users/${userId}/groups/${groupId}`)