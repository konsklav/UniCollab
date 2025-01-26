import { NavLink } from "react-router-dom";
import { PostInformation } from "./Posts.types";

import './posts.css'

export default function PostPreview({title, author, uploadDate, slug, subjects}: PostInformation) {
    return (
        <NavLink to={`/posts/${slug}`}>
            <div className="post-preview">
                <div className="post-preview-title mb-3">{title}</div>
                <div className="post-preview-details">
                    {author.username}
                    <br/>
                    {uploadDate.toLocaleString()}
                    <br/>
                    <strong>Subjects:</strong> {subjects.join(', ')}
                </div>
            </div>
        </NavLink>
    )
}