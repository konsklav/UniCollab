import { useQuery } from "@tanstack/react-query"
import { useSession } from "../hooks/useSession"
import { getPostsOfUser } from "../endpoints/postEndpoints"
import WaitForQuery from "../components/WaitForQuery"
import PostList from "../features/Posts/PostList"

export default function MyPostsPage() {
    const {user} = useSession()
    const query = useQuery({
        queryKey: ['posts', user.id],
        queryFn: () => getPostsOfUser(user.id)
    })

    return (
        <WaitForQuery query={query}>
            <PostList posts={query.data!}/>
        </WaitForQuery>
    )
}