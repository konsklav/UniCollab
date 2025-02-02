import { ChatRoomInformation } from "../Chat.types";

export default function RecentChat(info: ChatRoomInformation) {

    const date = info.lastMessage 
        ? new Date(info.lastMessage.sentAt).toLocaleDateString()
        : ''
        
    const time = info.lastMessage 
        ? new Date(info.lastMessage.sentAt).toLocaleTimeString()
        : ''

    return (
        <div className="d-flex">
            <div className="flex-grow-1 text-truncate" style={{width: '10rem'}}>
                <strong>{info.name}</strong>
                <div className="text-black-50">
                    {info.lastMessage 
                        ? <>
                            <strong>{info.lastMessage.sender.username}:</strong> {info.lastMessage.content}
                        </>
                        : 'No Messages!'}
                </div>
            </div>
            <div className="d-flex flex-column align-items-end text-black-50">
                <div>{date}</div>
                <div>{time}</div>
            </div>
        </div>
    )
}