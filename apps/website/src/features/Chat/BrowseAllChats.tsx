import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { getJoinableChats, joinChatRoom } from "../../endpoints/chatEndpoints";
import WaitForQuery from "../../components/WaitForQuery";
import ChatPreview from "./ChatPreview";
import { useSession } from "../../hooks/useSession";

interface ChatBrowseProps {
    onJoinChat?: (chatId: string) => void
}

export default function BrowseAllChats({onJoinChat}: ChatBrowseProps) {
    const {user} = useSession()
    const queryClient = useQueryClient()
    
    const query = useQuery({
        queryKey: ['get-joinable-chats'],
        queryFn: () => getJoinableChats(user)
    })

    const mutation = useMutation({
        mutationFn: (chatId: string) => joinChatRoom(chatId, user),
        onSuccess: (_data, variables, _context) => {
            onJoinChat?.(variables)    
        }
    })

    const handleJoin = async (chatId: string) => {
        await mutation.mutateAsync(chatId)
        queryClient.invalidateQueries({
            queryKey: ['get-joinable-chats']
        })
    }

    return (
        <>
            <h1>Available Chats</h1>
            <WaitForQuery query={query}>
                <div className="row g-1">
                        {query.data?.map(chatInfo => (
                            <div className="col-auto">
                                <ChatPreview info={chatInfo} onJoin={handleJoin}/>
                            </div>
                        ))}
                </div>
            </WaitForQuery>
        </>
    )
}