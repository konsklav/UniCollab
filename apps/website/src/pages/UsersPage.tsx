import { useQuery } from "@tanstack/react-query";
import { User } from "../common/common.types";
import WaitForQuery from "../components/WaitForQuery";
import { getAllUsers } from "../endpoints/userEndpoints";

export default function UsersPage() {
    const query = useQuery<readonly User[]>({
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