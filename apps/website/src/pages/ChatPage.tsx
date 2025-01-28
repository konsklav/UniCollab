import { Link, useParams } from "react-router-dom"
import Chat from "../features/Chat/ChatRoom"
import BrowseAllChats from "../features/Chat/BrowseAllChats"
import CreateChatForm from "../features/Chat/CreateChatForm"

export type ChatPageParams = '' | 'browse' | 'create'

export default function ChatPage() {
    const { state } = useParams()
    const page = getPageFromRoute(state)

    return (
        <div className="container-fluid">
            
            <div className="btn-group">
                <Link className="btn btn-primary" to={'/chat'}>Chat</Link>
                <Link className="btn btn-success" to={'/chat/create'}>Create</Link>
                <Link className="btn btn-secondary" to={'/chat/browse'}>Browse</Link>
            </div>
            <div className="container-fluid p-3">
                {page}
            </div>
        </div>
    )
}

const getPageFromRoute = (route: string | undefined) => {
    switch (route) {
        case 'browse':
            return <BrowseAllChats/>
        case 'create':
            return <CreateChatForm/>
        case undefined:
        case '':
        default:
            return <Chat/>
    }
}