import { useState } from "react"
import InputText from "../../components/Form/InputText"
import { ChatRoomInformation, CreateChatRoomRequest } from "./Chat.types"
import { UserInformation } from "../Users/Users.types"
import { useGetAllUsers } from "../../hooks/queries/useGetAllUsers"
import { MultiValue } from "react-select"
import WaitForQuery from "../../components/WaitForQuery"
import { useMutation } from "@tanstack/react-query"
import { createChatRoom } from "../../endpoints/chatEndpoints"
import { useSession } from "../../hooks/useSession"
import { UniCollabForm } from "../../components/Form/UniCollabForm"
import { SubmitButton } from "../../components/Button"
import UserSelect from "../../components/Users/UserSelect"

const initialState: CreateChatRoomRequest = {
    name: '',
    initialParticipants: []
}

export default function CreateChatForm() {
    const [request, setRequest] = useState<CreateChatRoomRequest>(initialState)
    const [status, setStatus] = useState('')
    const {user} = useSession()

    const query = useGetAllUsers()
    const mutation = useMutation({
        mutationFn: createChatRoom,
        onSuccess: (data, _variables, _context) => {
            const chat = data as ChatRoomInformation
            setStatus(`Successfully created chat room '${chat.name}'`)
        },
        onError: () => {
            setStatus("Couldn't create chat room.")
        }
    })

    const handleSubmit = async () => {
        const finalRequest: CreateChatRoomRequest = {
            ...request, 
            initialParticipants: [...request.initialParticipants, user.id]
        }

        await mutation.mutateAsync(finalRequest)

        setRequest(initialState)
    }

    const setName = (value: string) => setRequest(req => ({...req, name: value}))
    const setParticipants = (participants: MultiValue<UserInformation>) => setRequest(req => (
        {
            ...req, 
            initialParticipants: participants.map(p => p.id)
        }))

    return (
        <WaitForQuery query={query}>
            <UniCollabForm name="create-chat" className="p-3 border rounded-2 d-flex flex-column gap-2" onSubmit={handleSubmit}>
                <h2>Create Chat Room</h2>

                <InputText label="Name" value={request.name} onChange={setName}/>

                <label className="form-label">Participants</label>
                <UserSelect onChange={setParticipants} options={query.data?.filter(u => u.id !== user.id)} />

                <SubmitButton 
                    color={'primary'}
                    className="align-self-center mt-3"
                    loadingText={`Creating ${request.name}...`}>Create</SubmitButton>

                <div>{status}</div>
            </UniCollabForm>
        </WaitForQuery>
    )
}