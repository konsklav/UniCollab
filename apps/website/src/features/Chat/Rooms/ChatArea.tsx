import { useEffect, useRef } from "react"
import { ChatRoomInformation, MessageDto } from "../Chat.types"
import MessageBoard from "../Messages/MessageBoard"
import ChatInput from "./ChatInput"
import { Button } from "../../../components/Button"

import styles from './ChatArea.module.css'

interface ChatAreaProps {
    selectedChat: ChatRoomInformation,
    messages: readonly MessageDto[],
    onSend: (message: string) => void,
    onRequestLeave: (chat: ChatRoomInformation) => void
}

export default function ChatArea({selectedChat, messages, onSend, onRequestLeave}: ChatAreaProps) {
    const chatDiv = useRef<HTMLDivElement>(null)

    useEffect(() => {
        chatDiv.current?.scrollIntoView({behavior: 'instant', block: 'end'})
    }, [selectedChat, messages])

    return (
        <div className={styles['chat-area']} ref={chatDiv}>
            <div className={styles['chat-title']}>
                <h2 className="fw-bold align-self-center position-sticky">
                    {selectedChat.name}
                </h2>
                <Button color={'danger'} onClick={() => onRequestLeave(selectedChat)}>Leave</Button>
            </div>

            <div className={styles['message-board']}>
                <MessageBoard messages={messages}/>
            </div>
            <div className="p-2 w-100">
                <ChatInput onSend={onSend}/>
            </div>
        </div>
    )
}