import { useParams } from "react-router-dom";

export default function PostPage() {
    const { slug } = useParams()

    if (!slug) {
        throw new Error('The PostPage requires a URL parameter by the name "slug"')
    }

    return <>{slug}</>
}