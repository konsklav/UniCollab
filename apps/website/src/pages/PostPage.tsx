import { useParams } from "react-router-dom";
import { getPostBySlug } from "../services/apiService";
import { useQuery } from "@tanstack/react-query";
import WaitForQuery from "../components/WaitForQuery";

export default function PostPage() {
    const { slug } = useParams()

    if (!slug) {
        throw new Error('The PostPage requires a URL parameter by the name "slug"')
    }

    const query = useQuery({
        queryKey: ['post-slug', slug],
        queryFn: () => getPostBySlug(slug)})

    return (
        <WaitForQuery query={query}>
            <article id="user-post">
                <h1>{query.data?.title}</h1>
                <div>Uploaded at {query.data?.uploadDate.toDateString()} by <strong>{query.data?.author}</strong></div>
                <p>
                    {query.data?.content}
                </p>
            </article>
        </WaitForQuery>
    )
}