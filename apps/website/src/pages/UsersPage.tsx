import { useQuery } from "@tanstack/react-query";
import { getAllUsers } from "../utils/api";
import { User } from "../common/common.types";

export default function UsersPage() {
    const {data, error, isLoading} = useQuery<User[]>({
        queryKey: ['users'], 
        queryFn: getAllUsers})

    if (isLoading) return <>Loading...</>
    if (error instanceof Error) return <>Error: {error.message}</>

    return (
        <ul>
            {data?.map(user => <li key={user.id}>{user.username}</li>)}
        </ul>
    )
}