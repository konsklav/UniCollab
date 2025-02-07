import { useState } from "react";
import { CreateGroupRequest, GroupDto } from "./Group.types";
import { useSession } from "../../hooks/useSession";
import { useGetAllUsers } from "../../hooks/queries/useGetAllUsers";
import WaitForQuery from "../../components/WaitForQuery";
import { UniCollabForm } from "../../components/Form/UniCollabForm";
import InputText from "../../components/Form/InputText";
import UserSelect from "../../components/Users/UserSelect";
import { UserInformation } from "../Users/Users.types";
import { SubmitButton } from "../../components/Button";
import { createGroup } from "../../endpoints/groupEndpoints";
import { useNavigate } from "react-router-dom";

const initialRequest: CreateGroupRequest = {
    name: '',
    initialMembers: []
}

export default function CreateGroupForm() {
    const [request, setRequest] = useState<CreateGroupRequest>(initialRequest)
    const {user} = useSession()
    const navigate = useNavigate()

    const query = useGetAllUsers()

    const handleSubmit = async () => { 
        const response = await createGroup(user.id, request)
        
        if (response.status === 201 || response.status === 200) {
            const group: GroupDto = response.data
            if (group) {
                navigate(`/groups/${group.id}`)
            }
        }
    }

    const setName = (value: string) => setRequest(req => ({...req, name: value}))
    const setMembers = (members: readonly UserInformation[]) => {
        setRequest(req => (
            {...req, initialMembers: members.map(u => u.id)}
        ))
    }

    return (
        <WaitForQuery query={query}>
            <UniCollabForm name="create-group" onSubmit={handleSubmit}>
                <div className="p-3 border rounded-2 d-flex flex-column gap-2">
                    <h2>Create Group</h2>

                    <InputText label="Name" value={request.name} onChange={setName}/>

                    <label className="form-label fw-bold">Add Members</label>
                    <UserSelect onChange={setMembers} options={query.data?.filter(u => u.id !== user.id)} />

                    <SubmitButton color={'success'} loadingText={`Creating ${request.name}...`}>Create</SubmitButton>
                </div>
            </UniCollabForm>
        </WaitForQuery>
    )
}