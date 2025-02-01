import { useSession } from "../../../hooks/useSession";
import { MessageDto } from "../Chat.types";
import Message from "./Message";

import styles from './MessageBoard.module.css'

interface MessageBoardProps {
    messages: readonly MessageDto[]    
}

export default function MessageBoard({messages}: MessageBoardProps) {
    const {user} = useSession()

    return (
        <div id={styles['message-board']}>
            {messages.map(msg => (
                <Message 
                    key={msg.id} 
                    message={msg}
                    sessionUser={user}/>
            ))}
        </div>
    )
}