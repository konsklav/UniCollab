import { useQuery, useQueryClient } from "@tanstack/react-query";
import WaitForQuery from "../components/WaitForQuery";
import { addFriend, getAllUsersRich, removeFriend } from "../endpoints/userEndpoints";
import { useSession } from "../hooks/useSession";
import UserCard from "../features/Friends/UserCard";

const queryKey = ['users', 'meta-friends']

export default function UsersPage() {
    const {user} = useSession()
    const queryClient = useQueryClient();
    const query = useQuery({
        queryKey: queryKey,
        queryFn: () => getAllUsersRich(user.id)
    })

    const handleAddFriend = async (friendId: string) => {
        await addFriend(user.id, friendId);
        queryClient.invalidateQueries({queryKey: queryKey})
    }
    const handleRemoveFriend = async (unfriendId: string) => {
        await removeFriend(user.id, unfriendId);
        queryClient.invalidateQueries({queryKey: queryKey})
    }
    
    return (
        <div className="container">
            <div className="row">
                <WaitForQuery query={query}>
                    {query.data?.map(info => (
                        <div key={info.user.id} className="col-auto">
                            <UserCard 
                                info={info}
                                onAddFriend={handleAddFriend} 
                                onRemoveFriend={handleRemoveFriend}/>
                        </div>
                    ))}
                </WaitForQuery>
            </div>
        </div>
    )
}