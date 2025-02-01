import { useQuery } from "@tanstack/react-query"
import { getMessages } from "../../../endpoints/chatEndpoints"
import { useChatClient } from "../../../hooks/useChatClient"
import { ChatRoomInformation, MessageDto } from "../Chat.types"
import WaitForQuery from "../../../components/WaitForQuery"
import MessageBoard from "../Messages/MessageBoard"
import { useEffect, useState } from "react"
import ChatInput from "./ChatInput"

interface ChatAreaProps {
    selectedChat: ChatRoomInformation | undefined
}

type ChatState = 'loading' | 'ready' | 'error'

export default function ChatArea({selectedChat}: ChatAreaProps) {
    if (!selectedChat)
        return <></>

    const [messages, setMessages] = useState<readonly MessageDto[]>([])
    const [chatState, setChatState] = useState<ChatState>('loading')

    const query = useQuery({
        queryKey: ['messages', selectedChat.id],
        queryFn: () => getMessages(selectedChat.id)
    })

    useEffect(() => {
        if (query.isSuccess && query.data) {
            setMessages(query.data)
        }
    }, [query.data])

    const {sendMessage} = useChatClient(selectedChat.id, {
        onMessageReceived: (message) => {
            setMessages(messages => [...messages, message])
        },
        onInitialized: () => setChatState('ready'),
        onJoinError: () => setChatState('error')
    })

    return (
        <div className="p-2">
            <WaitForQuery query={query}>
                    <MessageBoard messages={messages}/>
            </WaitForQuery>
            {chatState === 'ready' && <ChatInput onSend={msg => sendMessage(msg)}/>}
        </div>
    )
}