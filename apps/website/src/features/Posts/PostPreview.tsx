import { NavLink } from "react-router-dom";
import { PostInformation } from "./Posts.types";

import './posts.css'

export default function PostPreview({title, author, uploadDate, slug}: PostInformation) {
    return (
        <NavLink to={`/posts/${slug}`}>
            <div className="post-preview">
                <div className="post-preview-content">{title}</div>
                <div className="post-preview-author">
                    {author.username}
                    <br/>
                    {uploadDate.toLocaleDateString()}
                </div>
            </div>
        </NavLink>
    )
}