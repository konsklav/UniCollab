import { ChatRoomInformation } from "../Chat.types"

interface ChatAreaProps {
    selectedChat: ChatRoomInformation | undefined
}

export default function ChatArea({selectedChat}: ChatAreaProps) {
    return (
        <>
        {selectedChat 
            ? selectedChat.name
            : 'Nope!'}
        </>
    )
}