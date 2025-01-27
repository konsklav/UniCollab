import { ClientMessage } from "./Chat.types";

export default function ChatMessage({message}: {message: ClientMessage}) {
    return (
        <div className="chat-message">
            {message.username}: {message.content}
        </div>
    )
}