import { PostInformation } from "../features/Posts/Posts.types"
import { UserInformation } from "../features/Users/Users.types"
import { api } from "../services/apiService"

class PostInformationCore implements PostInformation {
    id: string
    title: string
    content: string
    author: UserInformation
    uploadDate: Date
    slug: string
    subjects: readonly string[]

    constructor(id: string, title: string, content: string, author: UserInformation, uploadDate: string | number | Date, slug: string, subjects: readonly string[]) {
        this.id = id
        this.title = title
        this.content = content
        this.author = author
        this.uploadDate = uploadDate instanceof Date 
            ? uploadDate
            : typeof uploadDate === 'string'
                ? new Date(uploadDate)
                : new Date()
        this.slug = slug
        this.subjects = subjects
    }
}

const getPostInformation = (data: PostInformation) => {
    return new PostInformationCore(data.id, data.title, data.content, data.author, data.uploadDate, data.slug, data.subjects)
}

export const getPostBySlug = async (slug: string): Promise<PostInformation> => {
    const response = await api.get(`/posts/${slug}`)
    return getPostInformation(response.data)
}

export const getRecentPosts = async (count: number): Promise<readonly PostInformation[]> => {
    if (count <= 0) {
        throw new Error('Invalid count parameter.')
    }

    const response = await api.get(`/posts/recent/${count}`)
    return response.data.map(getPostInformation)
}