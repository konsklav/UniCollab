import { useMutation, useQuery } from "@tanstack/react-query";
import { getJoinableChats, joinChatRoom } from "../../endpoints/chatEndpoints";
import WaitForQuery from "../../components/WaitForQuery";
import ChatPreview from "./ChatPreview";

interface ChatBrowseProps {
    onJoinChat?: (chatId: string) => void
}

export default function BrowseAllChats({onJoinChat}: ChatBrowseProps) {
    const query = useQuery({
        queryKey: ['get-joinable-chats'],
        queryFn: getJoinableChats
    })

    const mutation = useMutation({
        mutationFn: joinChatRoom,
        onSuccess: (_data, variables, _context) => {
            onJoinChat?.(variables)    
        }
    })

    const handleJoin = (chatId: string): Promise<void> => {
        return mutation.mutateAsync(chatId)
    }

    return (
        <WaitForQuery query={query}>
            {query.data?.map(chatInfo => (
                <ChatPreview info={chatInfo} onJoin={handleJoin}/>
            ))}
        </WaitForQuery>
    )
}