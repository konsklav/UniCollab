import { useQuery } from "@tanstack/react-query"
import { UserInformation } from "../../features/Users/Users.types"
import { getAllUsers } from "../../endpoints/userEndpoints"

export const useGetAllUsers = () => {
    return useQuery<readonly UserInformation[]>({
        queryKey: ['users'], 
        queryFn: getAllUsers})
}