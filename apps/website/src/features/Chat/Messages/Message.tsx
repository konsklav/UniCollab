import { UserInformation } from "../../Users/Users.types";
import { MessageDto } from "../Chat.types";

import styles from './Message.module.css'

interface MessageProps {
    message: MessageDto,
    sessionUser: UserInformation
}

export default function Message({message, sessionUser}: MessageProps) {
    const getClasses = () => {
        if (message.sender.id === sessionUser.id) {
            return styles['message-own']
        }
    }

    const sentAt = new Date(message.sentAt).toLocaleString();

    return (
        <div className={`${styles['message']} ${getClasses()}`}>
            <div className="p-1 text-black-50">
                <strong>{message.sender.username}</strong> at <strong>{sentAt}</strong>
            </div>
            <div className='p-2 border rounded'>
                {message.content}
            </div>
        </div>
    )
}