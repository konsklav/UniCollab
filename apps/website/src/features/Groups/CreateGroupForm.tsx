import { useState } from "react";
import { CreateGroupRequest } from "./Group.types";
import { useSession } from "../../hooks/useSession";
import { useGetAllUsers } from "../../hooks/queries/useGetAllUsers";
import WaitForQuery from "../../components/WaitForQuery";
import { UniCollabForm } from "../../components/Form/UniCollabForm";
import InputText from "../../components/Form/InputText";
import UserSelect from "../../components/Users/UserSelect";
import { UserInformation } from "../Users/Users.types";

const initialRequest: CreateGroupRequest = {
    name: '',
    initialMembers: []
}

export default function CreateGroupForm() {
    const [request, setRequest] = useState<CreateGroupRequest>(initialRequest)
    const {user} = useSession()

    const query = useGetAllUsers()

    const handleSubmit = async () => { }

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
                </div>
            </UniCollabForm>
        </WaitForQuery>
    )
}