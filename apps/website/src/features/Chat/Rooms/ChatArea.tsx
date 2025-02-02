import { ChatRoomInformation, MessageDto } from "../Chat.types"
import MessageBoard from "../Messages/MessageBoard"
import ChatInput from "./ChatInput"

interface ChatAreaProps {
    selectedChat: ChatRoomInformation,
    messages: readonly MessageDto[],
    onSend: (message: string) => void
}

export default function ChatArea({selectedChat, messages, onSend}: ChatAreaProps) {
    return (
        <div className="p-2 d-flex flex-column">
            <h2 className="fw-bold align-self-center">
                {selectedChat.name}
            </h2>

            <div className="flex-grow-1">
                <MessageBoard messages={messages}/>
            </div>
            <div className="p-2 w-100">
                <ChatInput onSend={onSend}/>
            </div>
        </div>
    )
}