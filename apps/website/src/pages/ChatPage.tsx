import { Link, useNavigate, useParams } from "react-router-dom"
import Chat from "../features/Chat/ChatRoom"
import BrowseAllChats from "../features/Chat/BrowseAllChats"
import CreateChatForm from "../features/Chat/CreateChatForm"

export type ChatPageParams = '' | 'browse' | 'create'

export default function ChatPage() {
    const { state } = useParams()
    const navigate = useNavigate()

    const showChildComponent = () => {
        if (!state || state === '') {
            return <Chat/>
        }
        if (state === 'browse') {
            return <BrowseAllChats/>
        }
        if (state === 'create') {
            return <CreateChatForm/>
        }
    }

    return (
        <div className="container-fluid">
            
            <div className="btn-group">
                {state && (
                    <Link className="btn" to={'/chat'}>
                        <i className="bi bi-arrow-left"></i>
                    </Link>)}
                <Link className="btn btn-success" to={'/chat/browse'}>Create New</Link>
                <Link className="btn btn-primary" to={'/chat/create'}>Browse Chats</Link>
            </div>
            <div className="container-fluid">
                {showChildComponent()}
            </div>
        </div>
    )
}