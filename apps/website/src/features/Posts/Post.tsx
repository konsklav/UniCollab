import { PostInformation } from "./Posts.types";

export default function Post({content}: PostInformation) {
    return <>{content}</>
}