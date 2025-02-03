import { useQuery, useQueryClient } from '@tanstack/react-query'
import { ChatRoomInformation, MessageDto } from '../Chat.types'
import styles from './ChatBox.module.css'
import { useSession } from '../../../hooks/useSession'
import { getMessages, getParticipatingChats, leaveChatRoom } from '../../../endpoints/chatEndpoints'
import WaitForQuery from '../../../components/WaitForQuery'
import RecentChat from './RecentChat'
import ChatArea from './ChatArea'
import { useEffect, useState } from 'react'
import { useChatClient } from '../../../hooks/useChatClient'

type ChatState = 'ready' | 'loading' | 'error'

export default function ChatBox() {
    const {user} = useSession()

    const [selectedChat, setSelectedChat] = useState<ChatRoomInformation | undefined>()
    const [messages, setMessages] = useState<readonly MessageDto[]>([])
    const [chatState, setChatState] = useState<ChatState>('loading')

    const chatQueryKey = ['chats', 'participating', user.id]

    const chatQuery = useQuery({
        queryKey: chatQueryKey,
        queryFn: () => getParticipatingChats(user)
    })
    const queryClient = useQueryClient()

    useEffect(() => {
        if (!selectedChat)
            return

        queryClient.fetchQuery({
            queryKey: ['messages', selectedChat.id],
            queryFn: () => getMessages(selectedChat.id)
        })
        .then(data => {
            setMessages(data)
        })
    }, [selectedChat])

    const {sendMessage, switchChat, leaveChat} = useChatClient({
        onMessageReceived: (message) => {
            queryClient.invalidateQueries({
                queryKey: chatQueryKey
            })
            setMessages(messages => [...messages, message])
        },
        onInitialized: () => setChatState('ready'),
        onJoinError: () => setChatState('error')
    })

    const handleChatSelect = (chat: ChatRoomInformation) => {
        setSelectedChat(chat)
        switchChat(chat.id)
    }

    const handleLeaveChat = async (chat: ChatRoomInformation) => {
        await leaveChatRoom(chat.id, user)
        leaveChat()
        setSelectedChat(undefined)
        queryClient.invalidateQueries({
            queryKey: chatQueryKey
        })
    }

    return (
        <div id={styles['chat-box']}>
            <div id={styles['recent-chats']}>
                <WaitForQuery query={chatQuery}>
                    {chatQuery.data?.map(chat => (
                        <div 
                          key={chat.id} 
                          onClick={() => handleChatSelect(chat)}
                          className={`px-3 py-1 ${styles['recent-chat']}`}>
                            <RecentChat {...chat}/>
                        </div>
                    )
                    )}
                </WaitForQuery>
            </div>
            <div id={styles['chat-area']}>
                {selectedChat 
                && chatState === 'ready' 
                && <ChatArea selectedChat={selectedChat} messages={messages} onSend={sendMessage} onRequestLeave={handleLeaveChat}/>}
            </div>
        </div>
    )
}