import PostList from "../features/Posts/PostList";

export default function PostsPage() {
    return <PostList posts={[
        {title: 'Epic Post', content: 'Hello!', author: 'konsklav', slug: 'hello', uploadDate: new Date()},
        {title: 'Kinda Epic Post', content: 'Hello 2!', author: 'nove', slug: 'hello-2', uploadDate: new Date()},
    ]}/>
}