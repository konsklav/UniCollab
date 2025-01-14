import { PostInformation } from "./Posts.types";

export default function Post({content, author}: PostInformation) {
    return <>{content}</>
}