import { useQuery } from "@tanstack/react-query"
import WaitForQuery from "../components/WaitForQuery"
import PostList from "../features/Posts/PostList"
import { getRecentPosts } from "../endpoints/postEndpoints"
import { useSession } from "../hooks/useSession"

const getCount = 12

export default function HomePage() {
    const {user} = useSession()
    const query = useQuery({
        queryKey: ['recent-posts', getCount],
        queryFn: () => getRecentPosts(getCount)
    })

    return (
        <WaitForQuery query={query}>
            <div className="container-fluid">
                <h1 className="mb-2">Welcome, {user.username}!</h1>
                <PostList posts={query.data!}/>
            </div>
        </WaitForQuery>
    )
}