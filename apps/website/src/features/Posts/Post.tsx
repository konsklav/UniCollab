import { PostInformation } from "./Posts.types";

export default function Post({post}: {post: PostInformation}) {
    return (
        <article className="container-fluid">
            <h1>{post.title}</h1>
            <div className="text-black-50 mb-2">
                Uploaded at <strong>{post.uploadDate.toLocaleString()}</strong> by <strong>{post.author.username}</strong></div>
            <p>{post.content}</p>
        </article>
    )
}