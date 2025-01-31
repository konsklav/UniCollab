import { useQuery } from '@tanstack/react-query'
import { ChatRoomInformation } from '../Chat.types'
import styles from './ChatBox.module.css'
import { useSession } from '../../../hooks/useSession'
import { getParticipatingChats } from '../../../endpoints/chatEndpoints'
import WaitForQuery from '../../../components/WaitForQuery'
import RecentChat from './RecentChat'
import ChatArea from './ChatArea'
import { useState } from 'react'

export default function ChatBox() {
    const {user} = useSession()
    const query = useQuery({
        queryKey: ['chats', 'participating', user.id],
        queryFn: () => getParticipatingChats(user)
    })

    const [selectedChat, setSelectedChat] = useState<ChatRoomInformation | undefined>()

    return (
        <div id={styles['chat-box']}>
            <div id={styles['recent-chats']}>
                <WaitForQuery query={query}>
                    {query.data?.map(chat => (
                        <div 
                          key={chat.id} 
                          onClick={() => setSelectedChat(chat)}
                          className={`px-3 py-1 ${styles['recent-chat']}`}>
                            <RecentChat {...chat}/>
                        </div>
                    )
                    )}
                </WaitForQuery>
            </div>
            <div id={styles['chat-area']}>
                <ChatArea selectedChat={selectedChat}/>
            </div>
        </div>
    )
}