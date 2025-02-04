import { useQuery } from "@tanstack/react-query"
import { useSession } from "../hooks/useSession"
import { getPostsOfUser } from "../endpoints/postEndpoints"
import WaitForQuery from "../components/WaitForQuery"
import PostList from "../features/Posts/PostList"
import NavigationButtons from "../components/NavigationButtons"
import { Link, useParams } from "react-router-dom"
import CreatePost from "../features/Posts/CreatePost"

export default function MyPostsPage() {
    const {state} = useParams()
    const {user} = useSession()
    const query = useQuery({
        queryKey: ['posts', user.id],
        queryFn: () => getPostsOfUser(user.id)
    })

    const getPage = () => {
        switch (state) {
            case 'create':
                return <CreatePost />
            default:
                return <PostList posts={query.data!}/>
        }
    }

    return (
        <WaitForQuery query={query}>
            <NavigationButtons>
                <Link to={'/user/posts'} className="btn btn-primary">Posts</Link>
                <Link to={'/user/posts/create'} className="btn btn-success">Create</Link>
            </NavigationButtons>
            <div className="p-3">
                {getPage()}
            </div>
        </WaitForQuery>
    )
}