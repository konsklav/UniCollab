import WaitForQuery from "../components/WaitForQuery";
import { useGetAllUsers } from "../hooks/queries/useGetAllUsers";

export default function UsersPage() {
    const query = useGetAllUsers()

    return (
        <WaitForQuery query={query}>
            <ul>
                {query.data?.map(user => <li key={user.id}>{user.username}</li>)}
            </ul>
        </WaitForQuery>
    )
}