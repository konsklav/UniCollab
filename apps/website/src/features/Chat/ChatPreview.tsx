import { Button } from "../../components/Button";
import { ChatRoomInformation } from "./Chat.types";

interface ChatPreviewProps {
    info: ChatRoomInformation,
    onJoin: (chatId: string) => Promise<void>
}

export default function ChatPreview({info, onJoin}: ChatPreviewProps) {

    const handleJoin = async () => {
        await onJoin(info.id)
    }

    return (
        <div className="card" style={{width: '20rem'}}>
            <div className="card-body">
                <h5 className="card-title">{info.name}</h5>
                <h6 className="card-subtitle mb-3 text-body-secondary">{info.participantCount} participants</h6>

                <Button 
                    color={'success'} 
                    onClick={handleJoin}
                    loadingText={`Joining ${info.name}...`}>Join</Button>
            </div>
        </div>
    )
}