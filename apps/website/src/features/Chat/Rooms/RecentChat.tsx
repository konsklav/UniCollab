import { ChatRoomInformation } from "../Chat.types";

export default function RecentChat(info: ChatRoomInformation) {
    return (
        <div>
            <strong>{info.name}</strong>
            <div className="text-black-50">
                {info.lastMessageSent 
                    ? `${info.lastMessageSent.sender.username}: ${info.lastMessageSent.content}`
                    : 'No Messages!'}
            </div>
        </div>
    )
}