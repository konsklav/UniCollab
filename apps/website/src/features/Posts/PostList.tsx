import PostPreview from "./PostPreview";
import { PostInformation } from "./Posts.types";

interface PostListProps {
    posts: readonly PostInformation[]
}

export default function PostList({posts}: PostListProps) {
    return ( 
    <div className="container">
        <div className="row g-2">
            {posts.map(post => (
                <div className="col-auto" key={post.id}>
                    <PostPreview key={post.slug} {...post}/>
                </div>
            ))}
        </div>
    </div>
    )
}