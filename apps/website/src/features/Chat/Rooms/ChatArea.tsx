import { useQuery } from "@tanstack/react-query"
import { getMessages } from "../../../endpoints/chatEndpoints"
import { useChatClient } from "../../../hooks/useChatClient"
import { ChatRoomInformation } from "../Chat.types"
import WaitForQuery from "../../../components/WaitForQuery"
import MessageBoard from "../Messages/MessageBoard"

interface ChatAreaProps {
    selectedChat: ChatRoomInformation | undefined
}

export default function ChatArea({selectedChat}: ChatAreaProps) {
    if (!selectedChat)
        return <></>

    const [messages, setMessages]

    const query = useQuery({
        queryKey: ['messages', selectedChat.id],
        queryFn: () => getMessages(selectedChat.id)
    })

    const {sendMessage} = useChatClient(selectedChat.id, {
        onMessageReceived: (message) => {
            
        }
    })

    return (
        <WaitForQuery query={query}>
            <MessageBoard messages={query.data!}/>
        </WaitForQuery>
    )
}