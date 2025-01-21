import { useQuery } from "@tanstack/react-query";
import { getAllUsers } from "../services/apiEndpoints";
import { User } from "../common/common.types";
import WaitForQuery from "../components/WaitForQuery";

export default function UsersPage() {
    const query = useQuery<User[]>({
        queryKey: ['users'], 
        queryFn: getAllUsers})

    return (
        <WaitForQuery query={query}>
            <ul>
                {query.data?.map(user => <li key={user.id}>{user.username}</li>)}
            </ul>
        </WaitForQuery>
    )
}