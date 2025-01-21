import { ClientMessage } from "../../hooks/useChatClient";

export default function ChatMessage({message}: {message: ClientMessage}) {
    return (
        <div className="chat-message">
            {message.username}: {message.content}
        </div>
    )
}