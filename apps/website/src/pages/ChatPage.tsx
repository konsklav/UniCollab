import { Link, useParams } from "react-router-dom"
import Chat from "../features/Chat/ChatRoom"
import BrowseAllChats from "../features/Chat/BrowseAllChats"
import CreateChatForm from "../features/Chat/CreateChatForm"

export type ChatPageParams = '' | 'browse' | 'create'

export default function ChatPage() {
    const { state } = useParams()

    const showChildComponent = () => {
        if (!state || state === '') {
            return <Chat/>
        }
        if (state === 'browse') {
            return <BrowseAllChats />
        }
        if (state === 'create') {
            return (
                <div className="w-100 d-flex align-items-center justify-content-center">
                    <CreateChatForm/>
                </div>
            )
        }
    }

    return (
        <div className="container-fluid">
            
            <div className="btn-group">
                <Link className="btn btn-primary" to={'/chat'}>Chat</Link>
                <Link className="btn btn-success" to={'/chat/create'}>Create</Link>
                <Link className="btn btn-secondary" to={'/chat/browse'}>Browse</Link>
            </div>
            <div className="container-fluid p-3">
                {showChildComponent()}
            </div>
        </div>
    )
}