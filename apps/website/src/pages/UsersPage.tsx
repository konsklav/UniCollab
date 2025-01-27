import { useQuery } from "@tanstack/react-query";
import WaitForQuery from "../components/WaitForQuery";
import { getAllUsers } from "../endpoints/userEndpoints";
import { UserInformation } from "../features/Users/Users.types";

export default function UsersPage() {
    const query = useQuery<readonly UserInformation[]>({
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