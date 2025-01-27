import { FormEvent, useState } from "react"
import InputText from "../../components/Form/InputText"
import { ChatRoomInformation, CreateChatRoomRequest } from "./Chat.types"
import { UserInformation } from "../Users/Users.types"
import { useGetAllUsers } from "../../hooks/queries/useGetAllUsers"
import Select, { MultiValue } from "react-select"
import WaitForQuery from "../../components/WaitForQuery"
import { useMutation } from "@tanstack/react-query"
import { createChatRoom } from "../../endpoints/chatEndpoints"
import { useSession } from "../../hooks/useSession"

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

    const handleSubmit = (e: FormEvent) => {
        e.preventDefault()
        const finalRequest: CreateChatRoomRequest = {
            ...request, 
            initialParticipants: [...request.initialParticipants, user.id]
        }

        mutation.mutate(finalRequest)

        setRequest(initialState)
    }

    const setName = (value: string) => setRequest(req => ({...req, name: value}))
    const setParticipants = (participants: MultiValue<UserInformation>) => {
        setRequest(req => (
            {
                ...req, 
                initialParticipants: participants.map(p => p.id)
            }))
    }

    return (
        <WaitForQuery query={query}>
            <form name="create-chat" 
                className="p-3 border rounded-2 d-flex flex-column gap-2" 
                onSubmit={handleSubmit}>
                <h2>Create Chat Room</h2>

                <label className="form-label">Name</label>
                <InputText value={request.name} onChange={setName}/>

                <label className="form-label">Participants</label>
                <Select 
                    isMulti
                    defaultValue={new Array<UserInformation>()}
                    getOptionLabel={p => p.username}
                    getOptionValue={p => p.id}
                    onChange={setParticipants}
                    options={query.data?.filter(u => u.id !== user.id)}/>

                <button type="submit" className="btn btn-primary align-self-center mt-3">Submit</button>

                <div>{status}</div>
            </form>

        </WaitForQuery>
    )
}