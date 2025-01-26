import { useQuery } from "@tanstack/react-query"
import { getRecentPosts } from "../services/apiEndpoints"
import WaitForQuery from "../components/WaitForQuery"
import PostList from "../features/Posts/PostList"

const getCount = 10

export default function HomePage() {
    const query = useQuery({
        queryKey: ['recent-posts', getCount],
        queryFn: () => getRecentPosts(getCount)
    })

    return (
        <WaitForQuery query={query}>
            <div className="container-fluid">
                <h1 className="mb-2">Welcome!</h1>
                <PostList posts={query.data!}/>
            </div>
        </WaitForQuery>
    )
}