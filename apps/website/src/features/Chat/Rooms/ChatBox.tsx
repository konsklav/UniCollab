import { useQuery } from '@tanstack/react-query'
import { ChatRoomInformation } from '../Chat.types'
import styles from './ChatBox.module.css'
import { useSession } from '../../../hooks/useSession'
import { getParticipatingChats } from '../../../endpoints/chatEndpoints'
import WaitForQuery from '../../../components/WaitForQuery'
import RecentChat from './RecentChat'

export default function ChatBox() {
    const {user} = useSession()
    const query = useQuery({
        queryKey: ['chats', 'participating', user.id],
        queryFn: () => getParticipatingChats(user)
    })

    const selectChat = (chat: ChatRoomInformation) => { } 

    return (
        <div id={styles['chat-box']}>
            <div id={styles['recent-chats']}>
                <WaitForQuery query={query}>
                    {query.data?.map(chat => (
                        <div 
                          key={chat.id} 
                          onClick={() => selectChat(chat)}
                          className={`px-3 py-1 ${styles['recent-chat']}`}>
                            <RecentChat {...chat}/>
                        </div>
                    )
                    )}
                </WaitForQuery>
            </div>
            <div id={styles['chat-area']}>
                
            </div>
        </div>
    )
}